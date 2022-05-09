using AssemblyCSharp.Mod.PickMob;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Mod.Xmap
{
    public class XmapController : IActionListener
    {
        private const int TIME_DELAY_NEXTMAP = 200;
        private const int TIME_DELAY_RENEXTMAP = 500;
        private const int ID_ITEM_CAPSULE_VIP = 194;
        private const int ID_ITEM_CAPSULE = 193;
        private const int ID_ICON_ITEM_TDLT = 4387;

        private static readonly XmapController _Instance = new();

        private static int IdMapEnd;
        private static List<int> WayXmap;
        private static int IndexWay;
        private static bool IsNextMapFailed;
        private static bool IsWait;
        private static long TimeStartWait;
        private static long TimeWait;
        private static bool IsWaitNextMap;
        public static string mapnext;
        public static void Update()
        {
            if (IsWaiting())
                return;

            if (XmapData.Instance().IsLoading)
                return;

            if (IsWaitNextMap)
            {
                Wait(TIME_DELAY_NEXTMAP);
                IsWaitNextMap = false;
                return;
            }

            if (IsNextMapFailed)
            {
                XmapData.Instance().MyLinkMaps = null;
                WayXmap = null;
                IsNextMapFailed = false;
                return;
            }

            if (WayXmap == null)
            {
                if (XmapData.Instance().MyLinkMaps == null)
                {
                    XmapData.Instance().LoadLinkMaps();
                    return;
                }
                WayXmap = XmapAlgorithm.FindWay(TileMap.mapID, IdMapEnd);
                IndexWay = 0;
                if (WayXmap == null)
                {
                    GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
                    FinishXmap();
                    return;
                }
            }

            if (TileMap.mapID == WayXmap[WayXmap.Count - 1] && !XmapData.IsMyCharDie())
            {
                GameScr.info1.addInfo("Xmap by Phucprotein", 0);
                FinishXmap();
                return;
            }

            if (TileMap.mapID == WayXmap[IndexWay])
            {
                if (XmapData.IsMyCharDie())
                {
                    Service.gI().returnTownFromDead();
                    IsWaitNextMap = IsNextMapFailed = true;
                }
                else if (XmapData.CanNextMap())
                {
                    NextMap(WayXmap[IndexWay + 1]);
                    IsWaitNextMap = true;
                }
                Wait(TIME_DELAY_RENEXTMAP);
                return;
            }

            if (TileMap.mapID == WayXmap[IndexWay + 1])
            {
                IndexWay++;
                return;
            }

            IsNextMapFailed = true;
        }

        public void perform(int idAction, object p)
        {
            switch (idAction)
            {
                case 1:
                    List<int> idMaps = (List<int>)p;
                    ShowPanelXmap(idMaps);
                    break;
            }
        }

        public static void Wait(int time)
        {
            IsWait = true;
            TimeStartWait = mSystem.currentTimeMillis();
            TimeWait = time;
        }

        public static bool IsWaiting()
        {
            if (IsWait && mSystem.currentTimeMillis() - TimeStartWait >= TimeWait)
                IsWait = false;
            return IsWait;
        }

        #region Thao tác của xmap
        public static void ShowXmapMenu()
        {
            XmapData.Instance().LoadGroupMapsFromFile("Game\\Game\\TextData\\GroupMapsXmap.txt");
            MyVector myVector = new();
            foreach (var groupMap in XmapData.Instance().GroupMaps)
                myVector.addElement(new Command(groupMap.NameGroup, _Instance, 1, groupMap.IdMaps));
            GameCanvas.menu.startAt(myVector, 3);
        }

        public static void ShowPanelXmap(List<int> idMaps)
        {
            Pk9rXmap.IsMapTransAsXmap = true;
            int len = idMaps.Count;
            GameCanvas.panel.mapNames = new string[len];
            GameCanvas.panel.planetNames = new string[len];
            for (int i = 0; i < len; i++)
            {
                string nameMap = TileMap.mapNames[idMaps[i]];
                GameCanvas.panel.mapNames[i] = idMaps[i] + ": " + nameMap;
                GameCanvas.panel.planetNames[i] = "Xmap by Phucprotein";
            }
            GameCanvas.panel.setTypeMapTrans();
            GameCanvas.panel.show();
        }

        public static void StartRunToMapId(int idMap)
        {
            IdMapEnd = idMap;
            Pk9rXmap.IsXmapRunning = true;
        }

        public static void FinishXmap()
        {
            Pk9rXmap.IsXmapRunning = false;
            IsNextMapFailed = false;
            XmapData.Instance().MyLinkMaps = null;
            WayXmap = null;
        }

        public static void SaveIdMapCapsuleReturn()
        {
            Pk9rXmap.IdMapCapsuleReturn = TileMap.mapID;
        }

        private static void NextMap(int idMapNext)
        {
            List<MapNext> mapNexts = XmapData.Instance().GetMapNexts(TileMap.mapID);
            if (mapNexts != null)
            {
                foreach (MapNext mapNext in mapNexts)
                {
                    if (mapNext.MapID == idMapNext)
                    {
                        mapnext = TileMap.mapNames[mapNext.MapID];
                        NextMap(mapNext);
                        return;
                    }
                    
                }
            }
            GameScr.info1.addInfo("Lỗi tại dữ liệu", 0);
        }

        private static void NextMap(MapNext mapNext)
        {
            switch (mapNext.Type)
            {
                
                case TypeMapNext.AutoWaypoint:
                    NextMapAutoWaypoint(mapNext);
                    break;
                case TypeMapNext.NpcMenu:
                    NextMapNpcMenu(mapNext);
                    break;
                case TypeMapNext.NpcPanel:
                    NextMapNpcPanel(mapNext);
                    break;
                case TypeMapNext.Position:
                    NextMapPosition(mapNext);
                    break;
                case TypeMapNext.Capsule:
                    NextMapCapsule(mapNext);
                    break;
            }
        }

        private static void NextMapAutoWaypoint(MapNext mapNext)
        {
            Waypoint waypoint = XmapData.FindWaypoint(mapNext.MapID);
            if (waypoint != null)
            {
                int x = XmapData.GetPosWaypointX(waypoint);
                int y = XmapData.GetPosWaypointY(waypoint);
                MoveMyChar(x, y);
                if (Res.distance(waypoint.maxX, waypoint.minY, Char.myCharz().cx, Char.myCharz().cy) < 100)
                {
                    waypoint.popup.doClick(1);
                    return;
                }
                //RequestChangeMap(waypoint);
            }
            //for (int i = 0; i < TileMap.vGo.size(); i++)
            //{
            //    Waypoint w = (Waypoint)TileMap.vGo.elementAt(i);
            //    if (Res.distance(w.maxX, w.minY, Char.myCharz().cx, Char.myCharz().cy) < 100)
            //    {
            //        w.popup.doClick(1);
            //        return;
            //    }
            //}
        }

        private static void NextMapNpcMenu(MapNext mapNext)
        {
            int idNpc = mapNext.Info[0];
            //if (idNpc == 38)
            //{
            //    Pk9rXmap.checkNPC = true;
            //} 
            Service.gI().openMenu(idNpc);
            for (int i = 1; i < mapNext.Info.Length; i++) 
            {
                int select = mapNext.Info[i];
                
                if (ChangeServer.blue && idNpc == 38)
                {
                    Service.gI().confirmMenu((short)idNpc, (sbyte)0);
                }
                
                else
                {
                    Service.gI().confirmMenu((short)idNpc, (sbyte)select);
                }
                
            }
        }

        private static void NextMapNpcPanel(MapNext mapNext)
        {
            int idNpc = mapNext.Info[0];
            int selectMenu = mapNext.Info[1];
            int selectPanel = mapNext.Info[2];
            Service.gI().openMenu(idNpc);
            Service.gI().confirmMenu((short)idNpc, (sbyte)selectMenu);
            Service.gI().requestMapSelect(selectPanel);
        }

        private static void NextMapPosition(MapNext mapNext)
        {
            int xPos = mapNext.Info[0];
            int yPos = mapNext.Info[1]; 
            MoveMyChar(xPos, yPos);
            Service.gI().requestChangeMap();
            Service.gI().getMapOffline();
        }

        private static void NextMapCapsule(MapNext mapNext)
        {
            SaveIdMapCapsuleReturn();
            int index = mapNext.Info[0];
            Service.gI().requestMapSelect(index);
        }
        #region load map JIKL
        public static int getX(sbyte type)
        {
            for (int i = 0; i < TileMap.vGo.size(); i++)
            {
                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                if (waypoint.maxX < 60 && type == 0)
                {
                    return 15;
                }
                if ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60 && type == 1)
                {
                    return (int)((waypoint.minX + waypoint.maxX) / 2);

                }
                if ((int)waypoint.minX > TileMap.pxw - 60 && type == 2)
                {
                    return TileMap.pxw - 15;
                }
                if(type == 3)
                {
                    if (waypoint.maxX < 60 )
                    {
                        return 15;
                    }
                    if ((int)waypoint.minX > TileMap.pxw - 60)
                    {
                        return TileMap.pxw - 15;
                    }
                }
            }
            return 0;
        }
        public static int getY(sbyte type)
        {
            for (int i = 0; i < TileMap.vGo.size(); i++)
            {
                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                if (waypoint.maxX < 60 && type == 0)
                {
                    return (int)waypoint.maxY;
                }
                if ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60 && type == 1)
                {
                    return (int)waypoint.maxY;

                }
                if ((int)waypoint.minX > TileMap.pxw - 60 && type == 2)
                {
                    return (int)waypoint.maxY;
                }
                if (type == 3 && waypoint.maxY !=Char.myCharz().cy)
                {
                    if (waypoint.maxX < 60 )
                    {
                        return (int)waypoint.maxY;
                    }
                    if ((int)waypoint.minX > TileMap.pxw - 60 )
                    {
                        return (int)waypoint.maxY;
                    }
                }
            }
            return 0;
        }
        #endregion

        #region Thao tác với game
        public static void UseCapsuleNormal()
        {
            Pk9rXmap.IsShowPanelMapTrans = false;
            for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
            {
                if (global::Char.myCharz().arrItemBag[i].template.id == ID_ITEM_CAPSULE)
                {
                    Service.gI().useItem(0, 1, Convert.ToSByte(i), -1);
                    break;
                }
            }
            //Service.gI().useItem(0, 1, -1, ID_ITEM_CAPSULE);
        }

        public static void UseCapsuleVip()
        {
            Pk9rXmap.IsShowPanelMapTrans = false;
            for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
            {
                if (global::Char.myCharz().arrItemBag[i].template.id == ID_ITEM_CAPSULE_VIP)
                {
                    Service.gI().useItem(0, 1, Convert.ToSByte(i), -1);
                    break;
                }
            }
            //Service.gI().useItem(0, 1, -1, ID_ITEM_CAPSULE_VIP);
        }
        
        public static void HideInfoDlg()
        {
            InfoDlg.hide();
        }

        public static void MoveMyChar(int x, int y)
        {
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();

            if (ItemTime.isExistItem(ID_ICON_ITEM_TDLT))
                return;

            Char.myCharz().cx = x;
            Char.myCharz().cy = y + 1;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
        }

        private static void RequestChangeMap(Waypoint waypoint)
        {
            if (waypoint.isOffline)
            {
                Service.gI().getMapOffline();
                return;
            }
            Service.gI().requestChangeMap();
        }
        #endregion
        #endregion
        public static int MapID;
        public static int ZoneID;
        public static int cx;
        public static int cy;
        public static int stepHome;
        public static void GoBackToaDo()
        {
            if (TileMap.mapID == MapID && ZoneID == TileMap.zoneID)
            {
                return;
            }
            if (Char.myCharz().meDead && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
            {
                Service.gI().returnTownFromDead();
                return;
            }
            if (TileMap.mapID == MapID && TileMap.zoneID == ZoneID && GameCanvas.gameTick % (100 * (int)Time.timeScale) == 0)
            {
                MoveMyChar(cx, cy);
                return;
            }
            if (TileMap.mapID == global::Char.myCharz().nClass.classId + 21)
            {
                if (stepHome == 0)
                {
                    Service.gI().pickItem(-1);
                    Service.gI().openMenu(4);
                    stepHome = 1;
                    return;
                }
                if (stepHome == 1)
                {
                    GameCanvas.menu.menuSelectedItem = 0;
                    GameCanvas.menu.performSelect();
                    GameCanvas.menu.doCloseMenu();
                    stepHome = 2;
                    return;
                }
                if (stepHome == 2)
                {
                    Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(0);
                    if (global::Char.myCharz().cx != (int)(waypoint.maxX - 20))
                    {
                        Xmap.XmapController.MoveMyChar((int)(waypoint.maxX - 20), (int)waypoint.maxY);
                        return;
                    }
                    Service.gI().getMapOffline();
                    stepHome = 0;
                    return;
                }
            }
            if (TileMap.mapID != MapID && GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
            {
                if(GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
                {
                    XmapController.StartRunToMapId(MapID);
                }
            }
            if(TileMap.mapID == MapID && TileMap.zoneID != ZoneID && GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
            {
                Service.gI().requestChangeZone(ZoneID, -1);
                Wait(150);
            }
            if (TileMap.mapID ==  MapID && ZoneID == TileMap.zoneID)
            {
                Pk9rXmap.IsgobackComplete = true;
            }
        }
        public static void Getout()
        {
            Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(0);
            if (global::Char.myCharz().cx != (int)(waypoint.maxX - 20))
            {
                MoveMyChar((int)(waypoint.maxX - 20), (int)waypoint.maxY);
                return;
            }
            Service.gI().getMapOffline();
        }
        public static void GoBack()
        {
            if (TileMap.mapID == MapID && ZoneID == TileMap.zoneID)
            {
                return;
            }
            if (Char.myCharz().meDead && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
            {
                Service.gI().returnTownFromDead();
                return;
            }
            if (TileMap.mapID != MapID && GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
            {
                if (GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
                {
                    XmapController.StartRunToMapId(MapID);
                }
            }
            if (TileMap.mapID == MapID && TileMap.zoneID != ZoneID && GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
            {
                Service.gI().requestChangeZone(ZoneID, -1);
                Wait(150);
            }
            if (TileMap.mapID == MapID && ZoneID == TileMap.zoneID)
            {
                Pk9rXmap.IsgobackComplete = true;
            }
        }
		
	}
}
