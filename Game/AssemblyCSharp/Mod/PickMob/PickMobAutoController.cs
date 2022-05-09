using AssemblyCSharp.Mod.Xmap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace AssemblyCSharp.Mod.PickMob
{
    public class PickMobAutoController 
    {
        private
        const int TIME_REPICKITEM = 500;
        private
        const int TIME_DELAY_TANSAT = 500;
        private
        const int ID_ICON_ITEM_TDLT = 4387;
        private static readonly sbyte[] IdSkillsMelee = {
      0,
      9,
      2,
      17,
      4
    };
        private static readonly sbyte[] IdSkillsCanNotAttack = {
      10,
      11,
      14,
      23,
      7
    };

        private static readonly PickMobAutoController _Instance = new();

        public static bool IsPickingItems;

        private static bool IsWait;
        private static long TimeStartWait;
        private static long TimeWait;

        public static List<ItemMap> ItemPicks = new();
        public static List<SetDo1> ListSet1 = new List<SetDo1>();
        public static List<SetDo2> ListSet2 = new List<SetDo2>();
        private static int IndexItemPick = 0;
        public static bool findMobComplete;
        public static int XNhat;
        public static int YNhat;
        public static long timeAK;
        public static long timeTuHS;
        public static long timeSkill3;
        public static bool DangGiaoDich;
        public static int TimeGiaoDich;
        public static string NameInFile;
        public static bool Traded;
        public static string[] ListIDItem;
        public static string[] ListTenNVGD;

        private static void Move(int x, int y)
        {
            Char myChar = Char.myCharz();
            if (!PickMobAuto.IsVuotDiaHinh)
            {
                myChar.currentMovePoint = new MovePoint(x, y);
                return;
            }
            int[] vs = GetPointYsdMax(myChar.cx, x);
            if (vs[1] >= y || (vs[1] >= myChar.cy && (myChar.statusMe == 2 || myChar.statusMe == 1)))
            {
                vs[0] = x;
                vs[1] = y;
            }
            myChar.currentMovePoint = new MovePoint(vs[0], vs[1]);

        }

        #region Get data pick item
        private static TpyePickItem GetTpyePickItem(ItemMap itemMap)
        {
            Char myChar = Char.myCharz();
            bool isMyItem = (itemMap.playerId == myChar.charID || itemMap.playerId == -1);
            if (PickMobAuto.IsItemMe && !isMyItem)
                return TpyePickItem.CanNotPickItem;

            if (PickMobAuto.IsLimitTimesPickItem && itemMap.countAutoPick > PickMobAuto.TimesAutoPickItemMax)
                return TpyePickItem.CanNotPickItem;

            if (!FilterItemPick(itemMap))
                return TpyePickItem.CanNotPickItem;

            if (Res.abs(myChar.cx - itemMap.xEnd) < 60 && Res.abs(myChar.cy - itemMap.yEnd) < 60)
                return TpyePickItem.PickItemNormal;

            if (ItemTime.isExistItem(ID_ICON_ITEM_TDLT))
                return TpyePickItem.PickItemTDLT;

            if (PickMobAuto.IsTanSat)
                return TpyePickItem.PickItemTanSat;

            return TpyePickItem.CanNotPickItem;
        }

        private static bool FilterItemPick(ItemMap itemMap)
        {
            if (PickMobAuto.IdItemPicks.Count != 0 && !PickMobAuto.IdItemPicks.Contains(itemMap.template.id))
                return false;

            if (PickMobAuto.IdItemBlocks.Count != 0 && PickMobAuto.IdItemBlocks.Contains(itemMap.template.id))
                return false;

            if (PickMobAuto.TypeItemPicks.Count != 0 && !PickMobAuto.TypeItemPicks.Contains(itemMap.template.type))
                return false;

            if (PickMobAuto.TypeItemBlock.Count != 0 && PickMobAuto.TypeItemBlock.Contains(itemMap.template.type))
                return false;

            return true;
        }

        private enum TpyePickItem
        {
            CanNotPickItem,
            PickItemNormal,
            PickItemTDLT,
            PickItemTanSat
        }
        #endregion

        #region Get data tan sat
        private static Mob GetMobTanSat()
        {
            Mob mobDmin = null;
            int d;
            int dmin = int.MaxValue;
            Char myChar = Char.myCharz();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                d = (mob.xFirst - myChar.cx) * (mob.xFirst - myChar.cx) + (mob.yFirst - myChar.cy) * (mob.yFirst - myChar.cy);
                if (IsMobTanSat(mob) && d < dmin)
                {
                    mobDmin = mob;
                    dmin = d;
                }
            }
            return mobDmin;
        }

        private static Mob GetMobNext()
        {
            Mob mobTmin = null;
            long tmin = mSystem.currentTimeMillis();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                if (IsMobNext(mob) && mob.timeLastDie < tmin)
                {
                    mobTmin = mob;
                    tmin = mob.timeLastDie;
                }
            }
            return mobTmin;
        }

        private static bool IsMobTanSat(Mob mob)
        {
            if (mob.status == 0 || mob.status == 1 || mob.hp <= 0 || mob.isMobMe)
                return false;

            bool checkNeSieuQuai = PickMobAuto.IsNeSieuQuai && !ItemTime.isExistItem(ID_ICON_ITEM_TDLT);
            if (mob.levelBoss != 0 && checkNeSieuQuai)
                return false;

            if (!FilterMobTanSat(mob))
                return false;

            return true;
        }

        private static bool IsMobNext(Mob mob)
        {
            if (mob.isMobMe)
                return false;

            if (!FilterMobTanSat(mob))
                return false;

            if (PickMobAuto.IsNeSieuQuai && !ItemTime.isExistItem(ID_ICON_ITEM_TDLT) && mob.getTemplate().hp >= 3000)
            {
                if (mob.levelBoss != 0)
                {
                    Mob mobNextSieuQuai = null;
                    bool isHaveMob = false;
                    for (int i = 0; i < GameScr.vMob.size(); i++)
                    {
                        mobNextSieuQuai = (Mob)GameScr.vMob.elementAt(i);
                        if (mobNextSieuQuai.countDie == 10 && (mobNextSieuQuai.status == 0 || mobNextSieuQuai.status == 1))
                        {
                            isHaveMob = true;
                            break;
                        }
                    }
                    if (!isHaveMob)
                    {
                        return false;
                    }
                    mob.timeLastDie = mobNextSieuQuai.timeLastDie;
                }
                else if (mob.countDie == 10 && (mob.status == 0 || mob.status == 1))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool FilterMobTanSat(Mob mob)
        {
            if (PickMobAuto.IdMobsTanSat.Count != 0 && !PickMobAuto.IdMobsTanSat.Contains(mob.mobId))
                return false;

            if (PickMobAuto.TypeMobsTanSat.Count != 0 && !PickMobAuto.TypeMobsTanSat.Contains(mob.templateId))
                return false;

            return true;
        }

        private static Skill GetSkillAttack()
        {
            Skill skill = null;
            Skill nextSkill;
            SkillTemplate skillTemplate = new();
            foreach (var id in PickMobAuto.IdSkillsTanSat)
            {
                skillTemplate.id = id;
                nextSkill = Char.myCharz().getSkill(skillTemplate);
                if (IsSkillBetter(nextSkill, skill))
                {
                    skill = nextSkill;
                }
            }
            return skill;
        }

        private static bool IsSkillBetter(Skill SkillBetter, Skill skill)
        {
            if (SkillBetter == null)
                return false;

            if (!CanUseSkill(SkillBetter))
                return false;

            bool isPrioritize = (SkillBetter.template.id == 17 && skill.template.id == 2) ||
              (SkillBetter.template.id == 9 && skill.template.id == 0);
            if (skill != null && skill.coolDown >= SkillBetter.coolDown && !isPrioritize)
                return false;

            return true;
        }

        private static bool CanUseSkill(Skill skill)
        {
            if (mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > skill.coolDown)
                skill.paintCanNotUseSkill = false;

            if (skill.paintCanNotUseSkill && !IdSkillsMelee.Contains(skill.template.id))
                return false;

            if (IdSkillsCanNotAttack.Contains(skill.template.id))
                return false;

            if (Char.myCharz().cMP < GetManaUseSkill(skill))
                return false;

            return true;
        }

        private static int GetManaUseSkill(Skill skill)
        {
            if (skill.template.manaUseType == 2)
                return 1;
            else if (skill.template.manaUseType == 1)
                return (skill.manaUse * Char.myCharz().cMPFull / 100);
            else
                return skill.manaUse;
        }

        private static int GetYsd(int xsd)
        {
            Char myChar = Char.myCharz();
            int dmin = TileMap.pxh;
            int d;
            int ysdBest = -1;
            for (int i = 24; i < TileMap.pxh; i += 24)
            {
                if (TileMap.tileTypeAt(xsd, i, 2))
                {
                    d = Res.abs(i - myChar.cy);
                    if (d < dmin)
                    {
                        dmin = d;
                        ysdBest = i;
                    }
                }
            }
            return ysdBest;
        }

        private static int[] GetPointYsdMax(int xStart, int xEnd)
        {
            int ysdMin = TileMap.pxh;
            int x = -1;

            if (xStart > xEnd)
            {
                for (int i = xEnd; i < xStart; i += 24)
                {
                    int ysd = GetYsd(i);
                    if (ysd < ysdMin)
                    {
                        ysdMin = ysd;
                        x = i;
                    }
                }
            }
            else
            {
                for (int i = xEnd; i > xStart; i -= 24)
                {
                    int ysd = GetYsd(i);
                    if (ysd < ysdMin)
                    {
                        ysdMin = ysd;
                        x = i;
                    }
                }
            }
            int[] vs = {
        x,
        ysdMin
      };
            return vs;
        }
        #endregion

        #region Control update
        public static void Wait(int time)
        {
            IsWait = true;
            TimeStartWait = mSystem.currentTimeMillis();
            TimeWait = time;
        }

        public static bool IsWaiting()
        {
            if (IsWait && (mSystem.currentTimeMillis() - TimeStartWait >= TimeWait))
                IsWait = false;
            return IsWait;
        }
        #endregion
        #region mặc đồ theo set
        public struct SetDo1
        {
            public string info;
            public int type;

            public SetDo1(string info, int type)
            {
                this.info = info;
                this.type = type;
            }
        }
        public struct SetDo2
        {
            public string info2;
            public int type2;

            public SetDo2(string info2, int type2)
            {
                this.info2 = info2;
                this.type2 = type2;
            }
        }
        public static void addSet1(Item item)
        {
            foreach (SetDo1 setDo1 in ListSet1)
            {

                if (setDo1.type == item.template.type)
                {
                    ListSet1.Remove(setDo1);
                    GameScr.info1.addInfo("Đã gỡ " + item.template.name + " khỏi set 1", 0);
                }

            }
            ListSet1.Add(new SetDo1(item.info, item.template.type));
            GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào set 1", 0);
        }
        public static void addSet2(Item item)
        {
            foreach (SetDo2 setDo2 in ListSet2)
            {
                if (setDo2.type2 == item.template.type)
                {
                    ListSet2.Remove(setDo2);
                    GameScr.info1.addInfo("Đã gỡ " + item.template.name + " khỏi set 2", 0);

                }

            }
            //foreach (SetDo1 setDo1 in ListSet1)
            //{
            //    if (setDo1.type == item.template.type)
            //    {
            //        ListSet1.Remove(setDo1);
            //        GameScr.info1.addInfo("Đã gỡ " + item.template.name + " khỏi set 1", 0);
            //    }

            //}
            ListSet2.Add(new SetDo2(item.info, item.template.type));
            GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào set 2", 0);
        }
        public static void MacSet1()
        {
            bool flag = false;
            foreach (SetDo1 setDo1 in ListSet1)
            {
                Item[] arrItemBag = Char.myCharz().arrItemBag;
                try
                {
                    for (int i = 0; i < arrItemBag.Length; i++)
                    {
                        if (flag == false && arrItemBag[i].template.type == setDo1.type && arrItemBag[i].info == setDo1.info)
                        {
                            Service.gI().getItem(4, (sbyte)i);
                            //Thread.Sleep(800);
                            GameScr.info1.addInfo("Đã mặc: " + setDo1.info, 0);
                        }
                    }
                    flag = true;

                }
                catch
                {

                }

            }
            PickMobAuto.MacSet1t = false;
            GameScr.info1.addInfo("Đã mặc set 1", 0);
        }
        public static void MacSet2()
        {
            bool flag = false;
            foreach (SetDo2 setDo2 in ListSet2)
            {
                Item[] arrItemBag2 = Char.myCharz().arrItemBag;
                try
                {
                    for (int i = 0; i < arrItemBag2.Length; i++)
                    {
                        if (flag == false && arrItemBag2[i].template.type == setDo2.type2 && arrItemBag2[i].info == setDo2.info2)
                        {
                            Service.gI().getItem(4, (sbyte)i);
                            //Thread.Sleep(800);
                            GameScr.info1.addInfo("Đã mặc: " + setDo2.info2, 0);
                        }
                    }
                    flag = true;
                }
                catch
                {

                }

            }
            PickMobAuto.MacSet2t = false;
            GameScr.info1.addInfo("Đã mặc set 2", 0);
        }
        public static void actionPerForm(int idAction)
        {
            if (idAction == 25551)
            {
                //new Thread(delegate ()
                //{
                //    MacSet1();
                //})
                //{
                //    IsBackground = true
                //}.Start();
                if (PickMobAuto.MacSet2t)
                    PickMobAuto.MacSet2t = false;
                PickMobAuto.MacSet1t = true;
            }
            if (idAction == 25552)
            {
                //new Thread(delegate ()
                //{
                //    MacSet2();
                //})
                //{
                //    IsBackground = true
                //}.Start();
                if (PickMobAuto.MacSet1t)
                    PickMobAuto.MacSet1t = false;
                PickMobAuto.MacSet2t = true;
            }
            if (idAction == 25553)
            {
                GameCanvas.endDlg();
                PickMobAuto.IsBanMDV = !PickMobAuto.IsBanMDV;
                GameScr.info1.addInfo("Bán mảnh đá vụn: " + OnOrOff(PickMobAuto.IsBanMDV), 0);
            }
            if (idAction == 25555)
            {
                GameCanvas.endDlg();
                PickMobAuto.IsBanNR7s = !PickMobAuto.IsBanNR7s;
                GameScr.info1.addInfo("Bán 7S: " + OnOrOff(PickMobAuto.IsBanMDV), 0);
            }
            if (idAction == 25554)
            {
                GameCanvas.endDlg();
                
            }
        }
        public static string OnOrOff(bool t)
        {
            if (t)
            {
                return "On";
            }
            return "Off";
        }
        public static void vectorUseEqui()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Mặc set 1", 25551));
            myVector.addElement(new Command("Mặc set 2", 25552));
            GameCanvas.menu.startAt(myVector, 3);
        }
        #endregion
        public static void findMobForPet()
        {
            findMobComplete = false;
            MyVector vt = new MyVector();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob md = (Mob)GameScr.vMob.elementAt(i);
                if (global::Math.abs(md.x - Char.myCharz().cx) > 350)
                {
                    findMobComplete = true;
                    vt.addElement(md);
                    GameScr.gI().doUseSkillNotFocus(GameScr.onScreenSkill[0]);
                    Service.gI().sendPlayerAttack(vt, new MyVector(), 1);
                    Service.gI().sendPlayerAttack(vt, new MyVector(), 1);
                    return;
                }

            }
            if (findMobComplete == false)
            {
                MyVector myVector = new MyVector();
                Mob mob = (Mob)GameScr.vMob.elementAt(0);
                myVector.addElement(mob);
                GameScr.gI().doUseSkillNotFocus(GameScr.onScreenSkill[0]);
                Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
            }
        }
        public static void updateZone()
        {
            if (TileMap.mapID != global::Char.myCharz().nClass.classId + 21)
            {
                Service.gI().openUIZone();
            }
            else
            {

            }
        }
        public static void startOKDlg(string info)
        {
            if (info.Contains("Không thể đổi khu vực trong map này"))
                return;
            else if (info.StartsWith("Chưa thể đổi khu vực"))
                return;

        }
        public static void Update()
        {

            if (IsWaiting())
                return;

            Char myChar = Char.myCharz();

            if (myChar.statusMe == 14 || myChar.cHP <= 0)
                return;

            if ((myChar.cHP <= myChar.cHPFull * PickMobAuto.HpBuff / 100 ||
                myChar.cMP <= myChar.cMPFull * PickMobAuto.MpBuff / 100) &&
              (Pk9rXmap.isGoBack == false || Pk9rXmap.isGoBackbt == false))
                GameScr.gI().doUseHP();
            //if (Mathf.Round((float)global::Char.myPetz().cMP / (float)global::Char.myPetz().cMPFull * 100f) < 10 ||
            //  Mathf.Round((float)global::Char.myPetz().cHP / (float)global::Char.myPetz().cHPFull * 100f) < 10)
            //{
            //    GameScr.gI().doUseHP();

            //}
            bool isUseTDLT = ItemTime.isExistItem(ID_ICON_ITEM_TDLT);
            bool isTanSatTDLT = PickMobAuto.IsTanSat && isUseTDLT;
            if (PickMobAuto.IsAutoPickItems && !isTanSatTDLT)
            {
                if (IsPickingItems)
                {
                    if (IndexItemPick >= ItemPicks.Count)
                    {
                        IsPickingItems = false;
                        return;
                    }
                    ItemMap itemMap = ItemPicks[IndexItemPick];
                    switch (GetTpyePickItem(itemMap))
                    {
                        case TpyePickItem.PickItemTDLT:
                            myChar.cx = itemMap.xEnd;
                            myChar.cy = itemMap.yEnd;
                            Service.gI().charMove();
                            if (itemMap.template.id != 726 && itemMap.template.id != 220 && itemMap.template.id != 221 && itemMap.template.id != 222 && itemMap.template.id != 223 && itemMap.template.id != 224 && itemMap.template.id != 225)
                            {
                                Service.gI().pickItem(itemMap.itemMapID);
                            }
                            itemMap.countAutoPick++;
                            IndexItemPick++;
                            Wait(TIME_REPICKITEM);
                            return;
                        case TpyePickItem.PickItemTanSat:
                            Move(itemMap.xEnd, itemMap.yEnd);
                            myChar.mobFocus = null;
                            Wait(TIME_REPICKITEM);
                            return;
                        case TpyePickItem.PickItemNormal:
                            Service.gI().charMove();
                            if (itemMap.template.id != 726 && itemMap.template.id != 220 && itemMap.template.id != 221 && itemMap.template.id != 222 && itemMap.template.id != 223 && itemMap.template.id != 224 && itemMap.template.id != 225)
                            {
                                Service.gI().pickItem(itemMap.itemMapID);
                            }
                            itemMap.countAutoPick++;
                            IndexItemPick++;
                            Wait(TIME_REPICKITEM);
                            return;
                        case TpyePickItem.CanNotPickItem:
                            IndexItemPick++;
                            return;
                    }
                }
                ItemPicks.Clear();
                IndexItemPick = 0;
                for (int i = 0; i < GameScr.vItemMap.size(); i++)
                {
                    ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
                    if (GetTpyePickItem(itemMap) != TpyePickItem.CanNotPickItem)
                    {
                        ItemPicks.Add(itemMap);
                    }
                }
                if (ItemPicks.Count > 0)
                {
                    IsPickingItems = true;
                    return;
                }
            }

            if (PickMobAuto.IsTanSat)
            {
                //PickMob.IsAK = false;
                if (myChar.isCharge)
                {
                    Wait(TIME_DELAY_TANSAT);
                    return;
                }
                myChar.clearFocus(0);
                if (myChar.mobFocus != null && !IsMobTanSat(myChar.mobFocus))
                    myChar.mobFocus = null;
                if (myChar.mobFocus == null)
                {
                    myChar.mobFocus = GetMobTanSat();
                    if (isUseTDLT && myChar.mobFocus != null)
                    {
                        myChar.cx = myChar.mobFocus.xFirst - 24;
                        myChar.cy = myChar.mobFocus.yFirst;
                        Service.gI().charMove();
                        XmapController.MoveMyChar(myChar.cx, myChar.cy);
                    }
                }
                if (myChar.mobFocus != null)
                {
                    if (myChar.skillInfoPaint() == null)
                    {
                        Skill skill = GetSkillAttack();
                        if (skill != null && !skill.paintCanNotUseSkill)
                        {
                            Mob mobFocus = myChar.mobFocus;
                            mobFocus.x = mobFocus.xFirst;
                            mobFocus.y = mobFocus.yFirst;
                            GameScr.gI().doSelectSkill(skill, true);
                            if (Res.distance(mobFocus.xFirst, mobFocus.yFirst, myChar.cx, myChar.cy) <= 48)
                            {
                                GameScr.gI().MyDoDoubleClickToObj(mobFocus);
                            }
                            else
                            {
                                XmapController.MoveMyChar(mobFocus.xFirst, mobFocus.yFirst);
                                Move(mobFocus.xFirst, mobFocus.yFirst);
                            }

                        }
                    }
                }
                else if (!isUseTDLT)
                {
                    Mob mob = GetMobNext();
                    if (mob != null)
                    {
                        Move(mob.xFirst - 24, mob.yFirst);
                    }
                }
                Wait(TIME_DELAY_TANSAT);
            }
        }
        // auto danh

        //
        static int autoLoginTick = 300;
        static int i = autoLoginTick;
        
        public static void autoLogin()
        {

            if (PickMobAuto.IsAutoLogin == true)
            {

                if (ServerListScreen.testConnect == 0)
                {
                    GameCanvas.serverScreen.switchToMe();
                    i = autoLoginTick;
                }
                if (ServerListScreen.testConnect == 2)
                {
                    //if (i > 1301)
                    //{
                    //    i = 0;
                    //}
                    GameCanvas.startOKDlg($"Đăng Nhập Lại Sau\n {i * 15 / 1000}s");
                    i--;
                    //i++;

                    if (i == 0)
                    {
                        try
                        {
                            if (GameCanvas.loginScr == null)
                            {
                                GameCanvas.loginScr = new LoginScr();
                            }

                            GameScr.xskill = true;
                            GameCanvas.loginScr.switchToMe();
                            GameCanvas.loginScr.doLogin();
                            PickMobAuto.IsAutoLogin = false;
                        }
                        finally
                        {
                            i = autoLoginTick;
                        }

                        //break;
                    }
                }
            }
        }
        //nhat do de tu roi
        public static void NhatDoUpDT()
        {
            if (Char.myCharz().meDead)
            {
                return;
            }
            for (int i = 0; i < GameScr.vItemMap.size(); i++)
            {
                ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
                Char.myCharz().itemFocus = itemMap;
                if (itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1 && Math.abs(itemMap.x - Char.myCharz().cx) < 200 && Math.abs(itemMap.y - Char.myCharz().cy) < 48)
                {
                    //Char.myCharz().currentMovePoint = new MovePoint(itemMap.x, itemMap.y);
                    //Char.myCharz().itemFocus = itemMap;
                    //Thread.Sleep(1000);
                    //Char.myCharz().currentMovePoint = new MovePoint(XNhat, YNhat);
                    if (itemMap.template.id != 533 && itemMap.template.id != 225 && itemMap.template.id != 748 && itemMap.template.id != 949 && itemMap.template.id != 950)
                    {
                        //Xmap.XmapController.MoveMyChar(itemMap.x, itemMap.y);
                        //Char.myCharz().itemFocus = itemMap;
                        //Service.gI().pickItem(itemMap.itemMapID);
                        //Thread.Sleep(2000);
                        //Xmap.XmapController.MoveMyChar(XNhat, YNhat);
                        //Char.myCharz().cx = itemMap.x;
                        //Char.myCharz().cy = itemMap.y;
                        //Service.gI().charMove();
                        Xmap.XmapController.MoveMyChar(itemMap.xEnd, itemMap.yEnd);
                        Service.gI().pickItem(itemMap.itemMapID);
                        PickMobAuto.Wait(2000);
                        Xmap.XmapController.MoveMyChar(XNhat, YNhat);
                    }

                }

            }
        }
        //public static void NhatDoUpDT2()
        //{
        //    if (Char.myCharz().meDead)
        //    {
        //        return;
        //    }
        //    for (int i = 0; i < GameScr.vItemMap.size(); i++)
        //    {
        //        ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
        //        Char.myCharz().itemFocus = itemMap;
        //        if (itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1 && Math.abs(itemMap.x - Char.myCharz().cx) < 200 && Math.abs(itemMap.y - Char.myCharz().cy) < 48)
        //        {
        //            //Char.myCharz().currentMovePoint = new MovePoint(itemMap.x, itemMap.y);
        //            //Char.myCharz().itemFocus = itemMap;
        //            //Thread.Sleep(1000);
        //            //Char.myCharz().currentMovePoint = new MovePoint(XNhat, YNhat);
        //            if (itemMap.template.id != 533 && itemMap.template.id != 225 && itemMap.template.id != 748 && itemMap.template.id != 949 && itemMap.template.id != 950)
        //            {
        //                Xmap.XmapController.MoveMyChar(itemMap.xEnd, itemMap.yEnd);
        //                Service.gI().pickItem(itemMap.itemMapID);
        //            }

        //        }

        //    }
        //}
        #region autokok
        public static void MoveLeft()
        {
            //GameScr.gI().checkClickMoveTo(global::Char.myCharz().cx + 11, global::Char.myCharz().cy);
            global::Char.myCharz().cy -= 3;
            Service.gI().charMove();
        }
        public static void MoveRight()
        {
            //GameScr.gI().checkClickMoveTo(global::Char.myCharz().cx - 10, global::Char.myCharz().cy);
            global::Char.myCharz().cy += 3;
            Service.gI().charMove();
        }
        public static void AutoKOK()
        {
            //Char myChar = Char.myCharz();
            while (PickMobAuto.IsAKOK)
            {
                if (PickMobAuto.IsKOKMove)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
                Thread.Sleep(500);
                PickMobAuto.IsKOKMove = !PickMobAuto.IsKOKMove;
            }
        }
        #endregion
        //auto skill 3
        public static void AutoSkill3()
        {
            if (PickMobAuto.IsSkill3)
            {
                if (mSystem.currentTimeMillis() - timeSkill3 > Char.myCharz().myskill.coolDown)
                {
                    //sbyte idSkill = Char.myCharz().myskill.template.id;
                    SkillTemplate template = new SkillTemplate();
                    template.id = 8;
                    Skill skill = global::Char.myCharz().getSkill(template);
                    Service.gI().selectSkill((int)template.id);
                    GameScr.gI().doUseSkillNotFocus(skill);
                    GameScr.gI().doUseSkillNotFocus(skill);
                    skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                    timeSkill3 = mSystem.currentTimeMillis();
                }
            }

        }
        // xin dau

        public static void xinDau()
        {

            while (PickMobAuto.IsXinDau)
            {
                Service.gI().clanMessage(1, "", -1);
                Thread.Sleep(302000);

            }

        }

        public static void choDau()
        {
            while (PickMobAuto.IsChoDau)
            {
                for (int i = 0; i < ClanMessage.vMessage.size(); i++)
                {
                    ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
                    if (clanMessage.maxCap != 0 && clanMessage.playerName != global::Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Service.gI().clanDonate(clanMessage.id);
                            Thread.Sleep(150);
                        }
                    }
                }
                Thread.Sleep(150);
            }
        }
        public static bool IsCompleteGetBean;
        public static void thuDau()
        {
            if (TileMap.mapID == global::Char.myCharz().nClass.classId + 21)
            {
                int soLuong = 0;
                for (int i = 0; i < Char.myCharz().arrItemBox.Length; i++)
                {
                    Item item = Char.myCharz().arrItemBox[i];
                    if (item != null && item.template.type == 6)
                    {
                        soLuong += item.quantity;

                    }
                }
                if (soLuong <= 20 && (GameCanvas.gameTick % (200 * (int)Time.timeScale)) == 0)
                {
                    for (int i2 = 0; i2 < Char.myCharz().arrItemBox.Length; i2++)
                    {
                        Item item2 = Char.myCharz().arrItemBox[i2];
                        if (item2 != null && (item2.template.id == 13 || item2.template.id == 60 ||
                            item2.template.id == 61 || item2.template.id == 62 || item2.template.id == 63 ||
                            item2.template.id == 64 || item2.template.id == 65 || item2.template.id == 352 ||
                            item2.template.id == 523 || item2.template.id == 595))
                        {
                            Service.gI().getItem(1, (sbyte)i2);
                        }
                    }
                    IsCompleteGetBean = true;
                }
                if (GameScr.gI().magicTree.currPeas > 0 && GameScr.hpPotion < 10 || soLuong < 10 &&
                  (GameCanvas.gameTick % (100 * (int)Time.timeScale)) == 0)
                {
                    Service.gI().openMenu(4);
                    Service.gI().confirmMenu(4, 0);
                    IsCompleteGetBean = true;
                }
            }
        }
        public static void thuDauXin()
        {
            if (TileMap.mapID == global::Char.myCharz().nClass.classId + 21)
            {
                int soLuong = 0;
                for (int i = 0; i < Char.myCharz().arrItemBox.Length; i++)
                {
                    Item item = Char.myCharz().arrItemBox[i];
                    if (item != null && item.template.type == 6)
                    {
                        soLuong += item.quantity;

                    }
                }
                if (soLuong <= 20 && (GameCanvas.gameTick % (200 * (int)Time.timeScale)) == 0)
                {
                    for (int i2 = 0; i2 < Char.myCharz().arrItemBox.Length; i2++)
                    {
                        Item item2 = Char.myCharz().arrItemBox[i2];
                        if (item2 != null && (item2.template.id == 13 || item2.template.id == 60 ||
                            item2.template.id == 61 || item2.template.id == 62 || item2.template.id == 63 ||
                            item2.template.id == 64 || item2.template.id == 65 || item2.template.id == 352 ||
                            item2.template.id == 523 || item2.template.id == 595))
                        {
                            Service.gI().getItem(1, (sbyte)i2);
                        }
                    }
                }
                if (GameScr.gI().magicTree.currPeas > 0 && GameScr.hpPotion < 10 || soLuong < 10 &&
                  (GameCanvas.gameTick % (100 * (int)Time.timeScale)) == 0)
                {
                    Service.gI().openMenu(4);
                    Service.gI().confirmMenu(4, 0);
                    IsCompleteGetBean = true;
                }
            }
            else
            {
                Xmap.XmapController.StartRunToMapId(global::Char.myCharz().nClass.classId + 21);
            }
        }
        public static void autodt()
        {
            if (TileMap.mapID != 27)
            {
                PickMobAuto.IsDT = false;
                return;
            }
            if ((GameCanvas.gameTick % (20 * (int)Time.timeScale)) == 0)
            {
                Service.gI().openMenu(25);
                Service.gI().confirmMenu(25, 0);
                GameScr.info1.addInfo("Đang auto doanh trại", 0);
            }

        }
        public static void Revive()
        {
            if (PickMobAuto.IsRevive && global::Char.myCharz().meDead)
            {
                Service.gI().wakeUpFromDead();
            }
        }
        public static void AutoCSKB()
        {
            while (PickMobAuto.IsAutoCSKB)
            {
                Service.gI().useItem(0, 1, -1, 380);
                Thread.Sleep(700);
            }
        }
        //Auto focus
        public static void AutoFocus()
        {
            //if (PickMob.CharAutoFocus == null)
            //{
            //    if (Char.myCharz().charFocus != null)
            //    {
            //        PickMob.CharAutoFocus = Char.myCharz().charFocus;
            //        GameScr.info1.addInfo("Auto focus: " + PickMob.CharAutoFocus.cName, 0);
            //    }
            //}
            //else
            //{
            //    PickMob.CharAutoFocus = null;
            //    GameScr.info1.addInfo("Tắt auto focus", 0);
            //}

            if (PickMobAuto.CharAutoFocus == null)
            {
                return;
            }
            global::Char.myCharz().charFocus = PickMobAuto.CharAutoFocus;
            bool flag = true;
            if (GameCanvas.gameTick % 30 != 0)
            {
                return;
            }

            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                if (GameScr.vCharInMap.elementAt(i) == PickMobAuto.CharAutoFocus)
                {
                    flag = true;
                }
            }
            if (!flag || PickMobAuto.mapNameAutoFocus != TileMap.mapName)
            {
                PickMobAuto.CharAutoFocus = null;
                PickMobAuto.mapNameAutoFocus = null;
                PickMobAuto.isCharAutoFocus = false;
            }

        }
        public static void sellThoiVang()
        {
            if (PickMobAuto.isBanVang)
            {
                if (!ChangeServer.nroS)
                {
                    Service.gI().openMenu(39);
                }

                for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                {
                    if (global::Char.myCharz().arrItemBag[i].template.id == 457)
                    {
                        bool flag = false;
                        PickMobAuto.isSell = true;
                        int num = 0;
                        if (ChangeServer.nroS)
                        {
                            int j = global::Char.myCharz().arrItemBag[i].quantity;

                            while (j > 3)
                            {
                                Service.gI().useItem(0, 1, Convert.ToSByte(i), -1);
                                Thread.Sleep(500);
                                num++;
                                j--;
                                flag = true;
                                if (num == 3 || j <= 0)
                                {
                                    break;
                                }
                            }

                        }
                        if (ChangeServer.blue)
                        {
                            int j = global::Char.myCharz().arrItemBag[i].quantity;
                            while (j > 3)
                            {
                                Service.gI().saleItem(0, 1, (short)((sbyte)i));
                                Thread.Sleep(500);
                                num++;
                                j--;
                                flag = true;
                                if (num == 3)
                                {
                                    break;
                                }
                            }
                        }
                        if (flag)
                        {
                            PickMobAuto.DapDo2 = true;
                        }
                        PickMobAuto.isSell = false;
                        PickMobAuto.isBanVang = false;
                        return;
                    }
                }
            }

        }
        public static void AutoFocusBoss()
        {
            //if (PickMob.IsAutoFocusBoss)
            //{
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                char name = char.Parse(@char.cName.Substring(0, 1));
                if (name >= 'A' && name < 'Z' && !@char.cName.StartsWith("Đệ tử"))
                {
                    global::Char.myCharz().npcFocus = null;
                    global::Char.myCharz().mobFocus = null;
                    global::Char.myCharz().charFocus = null;
                    global::Char.myCharz().itemFocus = null;
                    global::Char.myCharz().charFocus = @char;
                }
            }
            //}
        }
        public static void AutoNeBoss()
        {
            while (PickMobAuto.IsAutoNeBoss)
            {
                for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                {
                    Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                    char name = char.Parse(@char.cName.Substring(0, 1));
                    if (name >= 'A' && name < 'Z' && !@char.cName.StartsWith("Đệ tử") && !@char.cName.StartsWith("Ăn trộm"))
                    {
                        Service.gI().requestChangeZone(-1, -1);
                    }
                }

                Thread.Sleep(1000);
            }
        }
        public static bool FlagInMap()
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (@char.cFlag != 0 && @char.charID > 0)
                {
                    return true;
                }

            }
            return false;
        }
        // auto flag 8
        public static void AutoFlag()
        {
            if (PickMobAuto.IsAutoFlag && (GameCanvas.gameTick % (20 * (int)Time.timeScale)) == 0)
            {
                if (!FlagInMap() && Char.myCharz().cFlag == 0)
                {
                    Service.gI().getFlag(1, 8);
                    return;
                }
                if (FlagInMap() && Char.myCharz().cFlag == 8)
                {
                    ////sbyte idSkill = Char.myCharz().myskill.template.id;
                    //SkillTemplate skillTemplate = new SkillTemplate();
                    //skillTemplate.id = 19;
                    //Skill skill = global::Char.myCharz().getSkill(skillTemplate);
                    ////Service.gI().selectSkill((int)skillTemplate.id);
                    //GameScr.gI().doUseSkillNotFocus(skill);
                    Service.gI().getFlag(1, 0);
                }
            }

        }
        public static void autoAnDuiGa()
        {
            if (PickMobAuto.IsAnDuiGa)
            {
                if (TileMap.mapID == global::Char.myCharz().nClass.classId + 21 && (GameCanvas.gameTick % (10 * (int)Time.timeScale)) == 0)
                {
                    ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(0);
                    if (itemMap != null)
                    {
                        XmapController.MoveMyChar(itemMap.x, itemMap.y);
                        Service.gI().pickItem(itemMap.itemMapID);
                    }
                }
            }
        }
        public static void autonhathopqua()
        {
            if (PickMobAuto.IsAnDuiGa)
            {

                if ((GameCanvas.gameTick % (20 * (int)Time.timeScale)) == 0)
                {
                    for (int i = 0; i < GameScr.vItemMap.size(); i++)
                    {
                        ItemMap @item = (ItemMap)GameScr.vItemMap.elementAt(i);

                        if (@item.template.id == 648)
                        {
                            Xmap.XmapController.MoveMyChar(@item.x, @item.y);
                            Char.myCharz().itemFocus = @item;
                            Service.gI().pickItem(@item.itemMapID);
                        }
                    }
                }
            }

        }
        // auto enter
        public static void AutoEnter()
        {
            if (PickMobAuto.IsAEnter)
            {
                while (PickMobAuto.IsAEnter)
                {

                    //KAutoHelper.AutoControl.SendKeyPress(KAutoHelper.KeyCode.ENTER);
                    Thread.Sleep(1000);
                }
            }
        }
        // AK
        public static void MOB(Mob mob)
        {
            try
            {
                MyVector myVector = new MyVector();
                myVector.addElement(mob);

                if (myVector.size() != 0)
                {
                    Service.gI().sendPlayerAttack(myVector, new MyVector(), -1);
                    timeAK = mSystem.currentTimeMillis();
                }
            }
            catch
            {

            }
        }
        public static void CHARs(global::Char @char)
        {
            try
            {
                MyVector myVector = new MyVector();
                myVector.addElement(@char);

                if (myVector.size() != 0)
                {
                    Service.gI().sendPlayerAttack(new MyVector(), myVector, -1);
                    timeAK = mSystem.currentTimeMillis();
                }
            }
            catch
            {

            }
        }
        public static void AK()
        {

            //if (mSystem.currentTimeMillis() - timeAK > Char.myCharz().myskill.coolDown)
            //    {

            if (global::Char.myCharz().charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus))
            {
                CHARs(global::Char.myCharz().charFocus);
                return;
            }

            if (global::Char.myCharz().mobFocus != null && GameScr.gI().isMeCanAttackMob(Char.myCharz().mobFocus))
            {
                MOB(global::Char.myCharz().mobFocus);
            }
            //}
            //if (Char.myCharz().mobFocus != null && GameScr.gI().isMeCanAttackMob(Char.myCharz().mobFocus) && Math.abs(Char.myCharz().mobFocus.x
            //    - Char.myCharz().cx) < Char.myCharz().myskill.dx * 1.5)
            //{
            //    MOB();
            //    timeAK = mSystem.currentTimeMillis();
            //    return;
            //}
            //if (Char.myCharz().charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus)
            //    && Math.abs(Char.myCharz().charFocus.xSd
            //    - Char.myCharz().cx) < Char.myCharz().myskill.dx * 1.5)
            //{
            //    CHARs();
            //    timeAK = mSystem.currentTimeMillis();
            //}

        }
        ////////////
        //auto ban do kho bau
        public static void AutoBDKB()
        {
            if (TileMap.mapID != 5)
            {
                PickMobAuto.IsABDKB = false;
                return;
            }
            if (!GameScr.isPaint)
            {
                PickMobAuto.IsABDKB = false;
                return;
            }
            if ((GameCanvas.gameTick % (20 * (int)Time.timeScale)) == 0)
            {
                Service.gI().confirmMenu((short)PickMobAuto.idNPC.template.npcTemplateId, 0);
            }
        }
        // tu sat
        public static void TuSat()
        {

            while (Char.myCharz().cHP > 0)
            {
                Service.gI().getFlag(1, 8);
                MyVector tuSat = new MyVector();
                tuSat.addElement(Char.myCharz());
                GameScr.gI().doUseSkillNotFocus(GameScr.onScreenSkill[0]);
                Service.gI().sendPlayerAttack(new MyVector(), tuSat, -1);
                Thread.Sleep(1000);
            }
        }
        public static void TuHS()
        {
            if (PickMobAuto.IsTuHS && Char.myCharz().cHP > 0)
            {
                if (mSystem.currentTimeMillis() - timeTuHS > Char.myCharz().myskill.coolDown)
                {
                    sbyte idSkill = Char.myCharz().myskill.template.id;
                    SkillTemplate skillTemplate = new SkillTemplate();
                    skillTemplate.id = 7;
                    Skill skill = global::Char.myCharz().getSkill(skillTemplate);
                    Service.gI().selectSkill((int)skillTemplate.id);
                    MyVector VMe = new MyVector();
                    VMe.addElement(Char.myCharz());
                    GameScr.gI().doUseSkillNotFocus(skill);
                    Service.gI().sendPlayerAttack(new MyVector(), VMe, -1);
                    skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                    Service.gI().selectSkill((int)idSkill);
                    timeTuHS = mSystem.currentTimeMillis();
                }

            }

        }
        public static void GoiDeTu()
        {
            Char.myCharz().cy--;
            Service.gI().charMove();
            Char.myCharz().cy++;
            Service.gI().charMove();
        }
        public static void KhoaViTri()
        {
            if (PickMobAuto.kvt)
            {
                Char.myCharz().isLockMove = true;
            }
            else
            {
                Char.myCharz().isLockMove = false;
            }
        }
        // tai tao nang luong khi hp va mp
        public static int aHP;
        public static int aMP;
        public static void ASkill3HPMP()
        {
            if (Char.myCharz().meDead || Char.myCharz().cHP == 0)
            {
                return;
            }
            Char myChar = Char.myCharz();
            if (myChar.cHP <= myChar.cHPFull * aHP / 100 || myChar.cMP <= myChar.cMPFull * aMP / 100)
            {
                //sbyte idSkill = Char.myCharz().myskill.template.id;
                SkillTemplate template = new SkillTemplate();
                template.id = 8;
                Skill skill = global::Char.myCharz().getSkill(template);
                Service.gI().selectSkill((int)template.id);
                GameScr.gI().doUseSkillNotFocus(skill);
                GameScr.gI().doUseSkillNotFocus(skill);
            }

        }
        public static void xindauv()
        {
            try
            {
                Service.gI().clanMessage(1, "", -1);
            }
            catch
            {

            }
            finally
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                    //Wait(3000);
                    Thread.Sleep(3000);

                    PickMobAuto.Chat("out");

                }));
            }
        }
        public static int num4;
        public static int y;
        public static int num5;
        public static int h2;
        public static void ChenKhu(int num, int oldMap)
        {

            if (TileMap.zoneID != num && oldMap == TileMap.mapID)
            {
                if (GameScr.gI().numPlayer[num] < GameScr.gI().maxPlayer[num])
                {
                    Service.gI().requestChangeZone(num, -1);
                    PickMobAuto.Wait(150);
                }
            }
            if (TileMap.zoneID == num)
            {
                PickMobAuto.IsChenKhu = false;
            }

        }
        public static bool HaveBossInMap()
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);

                if (@char.cName.ToLower().Contains("ăn trộm") || @char.cName.ToLower().Contains("ở dơ") || @char.cName.ToLower().Contains("super broly"))
                {
                    global::Char.myCharz().npcFocus = null;
                    global::Char.myCharz().mobFocus = null;
                    global::Char.myCharz().charFocus = null;
                    global::Char.myCharz().itemFocus = null;
                    global::Char.myCharz().charFocus = @char;
                    PickMobAuto.IsAK = true;
                    if (@char.cx != Char.myCharz().cx || @char.cy != Char.myCharz().cy)
                        Xmap.XmapController.MoveMyChar(@char.cx, @char.cy);
                    return true;

                }
            }
            return false;
        }
        public static void SanTrom()
        {
            if (PickMobAuto.IsSanAnTrom && (GameCanvas.gameTick % (20 * (int)Time.timeScale)) == 0)
            {
                if (!HaveBossInMap())
                {
                    Service.gI().requestChangeZone(TileMap.zoneID + 1, -1);
                }
            }

        }
        public static void DoKhu()
        {
            while (PickMobAuto.IsDoKhu == true)
            {
                Service.gI().requestChangeZone(TileMap.zoneID + 1, -1);
                Thread.Sleep(1000);
            }
        }
        public static void TeleVip(int charid)
        {
            Item[] arrItem = global::Char.myCharz().arrItemBody;
            if (arrItem[5] == null)
            {
                Service.gI().getItem(4, (sbyte)UseItem.findYadrat());
                Service.gI().gotoPlayer(charid);
                Service.gI().getItem(5, 5);
                return;
            }
            if (arrItem[5].template.name.Contains("Yardrat"))
            {
                Service.gI().gotoPlayer(charid);
                return;
            }
            if (!arrItem[5].template.name.ToLower().StartsWith("yardrat"))
            {
                Service.gI().getItem(4, (sbyte)UseItem.findYadrat());
                Service.gI().gotoPlayer(charid);
                Service.gI().getItem(4, (sbyte)UseItem.findYadrat());
            }
        }
        public static void VuotDiaHinh()
        {
            if (global::Char.myCharz().statusMe == 2 || global::Char.myCharz().statusMe == 3 || global::Char.myCharz().statusMe == 4 || global::Char.myCharz().statusMe == 10)
            {
                int num = global::Char.myCharz().cx + global::Char.myCharz().cdir * 24;
                if (TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy - 24) == 0 && TileMap.tileTypeAtPixel(num, global::Char.myCharz().cy - 24) > 0 && TileMap.tileTypeAtPixel(num, global::Char.myCharz().cy - 24) < 14)
                {
                    int num2 = 0;
                    for (int i = 6; i < TileMap.tmh; i++)
                    {
                        if (TileMap.tileTypeAt(num / (int)TileMap.size, i) != 0)
                        {
                            num2 = i;
                            break;
                        }
                    }
                    global::Char.myCharz().cx = num;
                    global::Char.myCharz().cy = num2 * (int)TileMap.size;
                    Service.gI().charMove();
                }
            }
        }
        public static bool IsChenKhuFull;
        public static int AutoZone;
        public static int OldMap;
        public static int AutoZoneCount = 1;
        public static void ChenKhuFull(int khu, int oldmap)
        {
            if (IsChenKhuFull)
            {
                if (GameScr.gI().numPlayer[khu] < 15 && TileMap.mapID == oldmap && TileMap.zoneID != khu)
                {
                    Service.gI().requestChangeZone(khu, -1);
                }
                if (TileMap.mapID != oldmap)
                {
                    AutoZoneCount = 1;
                    IsChenKhuFull = false;
                    return;
                }
                if (TileMap.zoneID == khu)
                {
                    GameScr.info1.addInfo("Đã vào khu: " + khu, 0);
                    IsChenKhuFull = false;
                    AutoZoneCount = 1;
                    return;
                }
                else
                {
                    IsChenKhuFull = true;
                    GameScr.info1.addInfo("Đang cố gắng vào khu " + khu + " lần " + AutoZoneCount, 0);
                    AutoZoneCount++;
                }
                PickMobAuto.Wait(100);
            }

        }
        public static void MobFollow(MyVector vMob)
        {
            if ( /*GameCanvas.gameTick % 70 == 0 && */ PickMobAuto.isMobFollow)
            {
                bool flag = false;
                for (int i = 0; i < vMob.size(); i++)
                {
                    Mob mob = (Mob)vMob.elementAt(i);
                    if (mob.status != 0 && mob.status != 1)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return;
                }
                if (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.isMobMe))
                {
                    for (int k = 0; k < vMob.size(); k++)
                    {
                        Mob mob2 = (Mob)vMob.elementAt(k);
                        if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && mob2.levelBoss <= 0 && !mob2.isMobMe /* && (Math.abs(mob2.x - Char.myCharz().cx) < 250 && Math.abs(mob2.y - Char.myCharz().cy) < 350)*/ )
                        {
                            Char.myCharz().mobFocus = mob2;
                            XmapController.MoveMyChar(Char.myCharz().mobFocus.xFirst, Char.myCharz().mobFocus.yFirst - 80);
                            break;
                        }
                    }
                }
                else if (Char.myCharz().mobFocus.hp <= 0 || Char.myCharz().mobFocus.status == 1 || Char.myCharz().mobFocus.status == 0)
                {
                    Char.myCharz().mobFocus = null;
                }
            }

        }
        public static int SKillFocus()
        {
            for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
            {
                if (GameScr.onScreenSkill[i] == Char.myCharz().myskill)
                {
                    return i;
                }
            }
            return 0;
        }
        static bool adauStep1 = false;
        static bool adauStep2 = false;
        static bool adauStep3 = false;
        public static void ADAU()
        {
            return;
                if (TileMap.mapID == global::Char.myCharz().nClass.classId + 21)
                {
                if (GameScr.gI().magicTree.currPeas > 0 && GameScr.hpPotion < 1000  && adauStep1 == false)
                {
                    Service.gI().openMenu(4);
                    Service.gI().confirmMenu(4, 0);
                    adauStep1 = true;
                    Wait(200);
                    return;
                }
                if (adauStep1 == true&& adauStep2 == false )
                    {
                        for (int i2 = 0; i2 < Char.myCharz().arrItemBox.Length; i2++)
                        {
                            Item item2 = Char.myCharz().arrItemBox[i2];
                            if (item2 != null && (item2.template.id == 13 || item2.template.id == 60 ||
                                item2.template.id == 61 || item2.template.id == 62 || item2.template.id == 63 ||
                                item2.template.id == 64 || item2.template.id == 65 || item2.template.id == 352 ||
                                item2.template.id == 523 || item2.template.id == 595))
                            {
                                Service.gI().getItem(0, (sbyte)i2);
                            }
                        }
                        adauStep2 = true;
                    return;
                    }
                    
                }
                if(adauStep1 == true && adauStep2 == true && adauStep3 == false)
            {
                if (GameScr.gI().magicTree.currPeas > 0 && GameScr.hpPotion < 1000 )
                {
                    Service.gI().openMenu(4);
                    Service.gI().confirmMenu(4, 2);
                    adauStep3 = true;
                    adauStep1 = false;
                    adauStep2 = false;
                    Wait(200);
                    return;
                }
            }
            
        }


    }
}