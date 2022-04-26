using AssemblyCSharp.Mod.PickMob;
//using AssemblyCSharp.Mod.SaveSetting;
using System;
using System.Threading;

namespace AssemblyCSharp.Mod.Xmap
{
    public class Pk9rXmap
    {
        public static bool IsXmapRunning = false;
        public static bool IsMapTransAsXmap = false;
        public static bool IsShowPanelMapTrans = true;
        public static bool IsUseCapsuleNormal = false;
        public static bool IsUseCapsuleVip = false;
        public static int  IdMapCapsuleReturn = -1;
        public static bool IsMoveToBoss;
        public static bool isGoBack;
        public static bool isGoBackbt;
        public static bool IsgobackComplete;
      
        public static bool Chat(string text)
        {
            if (text == "xmp")
            {
                if (IsXmapRunning)
                {
                    XmapController.FinishXmap();
                    GameScr.info1.addInfo("Đã huỷ Xmap", 0);
                }
                else
                {
                    XmapController.ShowXmapMenu();
                }
            }
            else if (IsGetInfoChat<int>(text, "xmp"))
            {
                if (IsXmapRunning)
                {
                    XmapController.FinishXmap();
                    GameScr.info1.addInfo("Đã huỷ Xmap", 0);
                }
                else
                {
                    int idMap = GetInfoChat<int>(text, "xmp");
                    XmapController.StartRunToMapId(idMap);
                }
            }
            else if (IsGetInfoChat<string>(text, "xmp"))
            {
                if (IsXmapRunning)
                {
                    XmapController.FinishXmap();
                    GameScr.info1.addInfo("Đã huỷ Xmap", 0);
                }
                else
                {
                    int idMap;
                    switch(GetInfoChat<string>(text, "xmp"))
                    {
                        case "vn":
                            idMap = Char.myCharz().nClass.classId + 21;
                            XmapController.StartRunToMapId(idMap);
                            break;
                        case "dh":
                            idMap = 129;
                            XmapController.StartRunToMapId(idMap);
                            break;
                        case "st":
                            idMap = 84;
                            XmapController.StartRunToMapId(idMap);
                            break;
                        case "tl":
                            idMap = 102;
                            XmapController.StartRunToMapId(idMap);
                            break;
                    }
                }
            }
            else if (text == "csb")
            {
                IsUseCapsuleNormal = !IsUseCapsuleNormal;
                GameScr.info1.addInfo("Sử dụng capsule thường Xmap: " + (IsUseCapsuleNormal ? "Bật" : "Tắt"), 0);
            }
            else if (text == "csdb")
            {
                IsUseCapsuleVip = !IsUseCapsuleVip;
                GameScr.info1.addInfo("Sử dụng capsule đặc biệt Xmap: " + (IsUseCapsuleVip ? "Bật" : "Tắt"), 0);
            }
            else if (text == "j")
            {
                if (XmapController.getX(0) > 0 && XmapController.getY(0) > 0 && XmapData.CanNextMap())
                {
                    XmapController.MoveMyChar(XmapController.getX(0), XmapController.getY(0));
                }
            }
            else if (text == "k")
            {
                if (XmapController.getX(1) > 0 && XmapController.getY(1) > 0 && XmapData.CanNextMap())
                {
                    XmapController.MoveMyChar(XmapController.getX(1), XmapController.getY(1));
                    Service.gI().getMapOffline();
                    Service.gI().requestChangeMap();
                    for (int i = 0; i < TileMap.vGo.size(); i++)
                    {
                        Waypoint w = (Waypoint)TileMap.vGo.elementAt(i);
                        if (Res.distance(w.maxX, w.minY, Char.myCharz().cx, Char.myCharz().cy) < 100)
                        {
                            w.popup.doClick(1);
                        }
                    }
                }

            }
            else if (text == "left")
            {
                if (XmapController.getX(2) > 0 && XmapController.getY(2) > 0 && XmapData.CanNextMap())
                {
                    XmapController.MoveMyChar(XmapController.getX(2), XmapController.getY(2));
                    
                }
            }
            else if (text == "i")
            {
                if (XmapController.getX (3) > 0 && XmapController.getY(3) > 0 && XmapData.CanNextMap())
                {
                    XmapController.MoveMyChar(XmapController.getX(3), XmapController.getY(3));

                }
            }
            else if (text == "tl")
            {
                Chat("xmp102");
            }
            else if (text == "goback")
            {
                isGoBack = !isGoBack;
                XmapController.MapID = TileMap.mapID;
                XmapController.ZoneID = TileMap.zoneID;
                XmapController.cx = Char.myCharz().cx;
                XmapController.cy = Char.myCharz().cy;
                GameScr.info1.addInfo("GoBack Toạ Độ " + (isGoBack ? "Bật" : "Tắt"), 0);
            }else if (text == "gb")
            {
                isGoBackbt = !isGoBackbt;
                XmapController.MapID = TileMap.mapID;
                XmapController.ZoneID = TileMap.zoneID;
                
                GameScr.info1.addInfo("GoBack " + (isGoBackbt ? "Bật" : "Tắt"), 0);
            }else if (text == "moveto")
            {
                if (Char.myCharz().charFocus != null)
                {
                    XmapController.MoveMyChar(Char.myCharz().charFocus.cx, Char.myCharz().charFocus.cy);
                }
                if (Char.myCharz().mobFocus != null)
                {
                    XmapController.MoveMyChar(Char.myCharz().mobFocus.x, Char.myCharz().mobFocus.y);
                }
                if (Char.myCharz().npcFocus != null)
                {
                    XmapController.MoveMyChar(Char.myCharz().npcFocus.cx, Char.myCharz().npcFocus.cy);
                }
                if (Char.myCharz().itemFocus != null)
                {
                    XmapController.MoveMyChar(Char.myCharz().itemFocus.x, Char.myCharz().itemFocus.y);
                }
            }          
            else if(text == "ttt")
            {
                GameMidlet.instance.exit();
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool HotKeys()
        {
            switch (GameCanvas.keyAsciiPress)
            {
                case 'x':
                    if (IsXmapRunning)
                    {
                        XmapController.FinishXmap();
                        GameScr.info1.addInfo("Đã huỷ Xmap", 0);
                    }else
                    ShowMenu.showmainmenu();
                    //Chat("xmp");
                    break;
                case 'c':
                    Chat("csb");
                    break;
                case 'j':
                    Chat("j");
                    break;
                case 'k':
                    Chat("k");
                    break;
                case 'l':
                    Chat("left");
                    break;
                case 'i':
                    Chat("i");
                    break;
                case 'g':
                    Chat("moveto");
                    break;
                case 'q':
                    ShowMenu.Instance().ActionPerform(141007, null);
                    break;
                default:
                    return false;
            }
            return true;
        }
        public static void GameScrUpdate()
        {
            GoBack();
            fineNPC38();
            GoBackbt();
        }
        public static void CharDie()
        {
            if (global::Char.myCharz().meDead && GameCanvas.gameTick % (5 * (int)UnityEngine.Time.timeScale) == 0)
            {
                IsgobackComplete = false;
                PickMobController.IsCompleteGetBean = false;
            }
        }
        public static void GoBack()
        {
            if(isGoBack && GameCanvas.gameTick % (20 * (int)UnityEngine.Time.timeScale) == 0)
            {
                if(isGoBackbt == true)
                    isGoBackbt = false;
                XmapController.GoBackToaDo();
            }
        }public static void GoBackbt()
        {
            if(isGoBackbt && GameCanvas.gameTick % (20 * (int)UnityEngine.Time.timeScale) == 0)
            {
                if (isGoBack == true)
                    isGoBack = false;
                XmapController.GoBack();
            }
        }
        public static void Update()
        {
            if (XmapData.Instance().IsLoading)
                XmapData.Instance().Update();
            if (IsXmapRunning)
                XmapController.Update();
        }
        static int oldID;
        public static void fineNPC38()
        {
            if (checkNPC == true && GameCanvas.gameTick % 50 == 0 && Pk9rXmap.IsXmapRunning == true)
            {

                if (TileMap.mapID == 27)
                {
                    oldID = 27;
                    Chat("left");
                    //XmapController.StartRunToMapId(28);
                    GameScr.info1.addInfo((checkNPC ? "Bật" : "Tắt"), 0);
                    return;
                }
                else if (TileMap.mapID == 28)
                {
                    if (oldID == 29)
                    {
                        Chat("j");
                        return;
                    }
                       
                    Chat("left");
                    GameScr.info1.addInfo((checkNPC ? "Bật" : "Tắt"), 0);
                    return;
                    //XmapController.StartRunToMapId(29);

                }
                else if (TileMap.mapID == 29)
                {
                    oldID = 29;
                    Chat("j");
                    GameScr.info1.addInfo((checkNPC ? "Bật" : "Tắt"), 0);
                    return;

                }

                else
                {
                    checkNPC = false;
                }

            }
            
        }
        public static bool checkNPC;
        public static void Info(string text)
        {
            if (text.Equals("Bạn chưa thể đến khu vực này") || text.Contains("Chưa hạ hết đối thủ"))
                XmapController.FinishXmap();
            if (text.ToLower().StartsWith("đã hết"))
            {
                XmapController.StartRunToMapId(23);
            }
            if (text.ToLower().Contains("rương phụ đã đầy"))
            {
                PickMob.PickMob.VongQuay = false;
            }
            if (text.ToLower().Contains("cần 1 trang bị có lỗ"))
            {
                PickMob.PickMob.DapDo1 = false;
            }
        }

        public static bool XoaTauBay(Object obj)
        {
            Teleport teleport = (Teleport)obj;
            if (teleport.isMe)
            {
                Char.myCharz().isTeleport = false;
                if (teleport.type == 0)
                {
                    Controller.isStopReadMessage = false;
                    Char.ischangingMap = true;
                }
                Teleport.vTeleport.removeElement(teleport);
                return true;
            }
            return false;
        }

        public static void SelectMapTrans(int selected)
        {
            if (IsMapTransAsXmap)
            {
                XmapController.HideInfoDlg();
                string mapName = GameCanvas.panel.mapNames[selected];
                int idMap = XmapData.GetIdMapFromPanelXmap(mapName);
                XmapController.StartRunToMapId(idMap);
                return;
            }
            XmapController.SaveIdMapCapsuleReturn();
            Service.gI().requestMapSelect(selected);
        }

        public static void ShowPanelMapTrans()
        {
            IsMapTransAsXmap = false;
            if (IsShowPanelMapTrans)
            {
                GameCanvas.panel.setTypeMapTrans();
                GameCanvas.panel.show();
                return;
            }
            IsShowPanelMapTrans = true;
        }

        public static void FixBlackScreen()
        {
            Controller.gI().loadCurrMap(0);
            Service.gI().finishLoadMap();
            Char.isLoadingMap = false;
        }

        #region Không cần liên kết với game
        public static bool IsGetInfoChat<T>(string text, string s)
        {
            if (!text.StartsWith(s))
            {
                return false;
            }
            try
            {
                Convert.ChangeType(text.Substring(s.Length), typeof(T));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static T GetInfoChat<T>(string text, string s)
        {
            return (T)Convert.ChangeType(text.Substring(s.Length), typeof(T));
        }
        #endregion
    }
}
