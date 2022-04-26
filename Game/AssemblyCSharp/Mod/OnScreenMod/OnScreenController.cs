using AssemblyCSharp.Mod.PickMob;
using AssemblyCSharp.Mod.Xmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace AssemblyCSharp.Mod.OnScreenMod
{

    public class OnScreenController
    {

        #region dctt on screen
        public static Char[] saveChar = new Char[30];
        public static void UpdateKeyTouchControl()
        {
            if (OnScreen.nhanVat)
            {
                int number = 150;
                for (int a = 0; a < saveChar.Length; a++)
                {
                    if (saveChar[a] != null)
                    {
                        if (GameCanvas.isPointerHoldIn(360, number, 150, 12))
                        {
                            XmapController.MoveMyChar(saveChar[a].cx, saveChar[a].cy);
                            global::Char.myCharz().charFocus = saveChar[a];
                            SoundMn.gI().buttonClick();
                            global::Char.myCharz().currentMovePoint = null;
                            GameCanvas.clearAllPointerEvent();
                            GameCanvas.clearKeyHold();
                            GameCanvas.clearKeyPressed();
                        }
                        number += 13;
                    }

                }
            }

        }
        static void determineColor(Char @char, int x, int numY, mGraphics g)
        {
            if (@char.cFlag == 1)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 2003199, 100);
                return;
            }
            if (@char.cFlag == 2)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 16711680, 100);
                return;
            }
            if (@char.cFlag == 3)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 8388736, 100);
                return;
            }
            if (@char.cFlag == 4)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 16776960, 100);
                return;
            }
            if (@char.cFlag == 5)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 8190976, 100);
                return;
            }
            if (@char.cFlag == 6)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 16716947, 100);
                return;
            }
            if (@char.cFlag == 7)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 16753920, 100);
                return;
            }
            if (@char.cFlag == 8)
            {
                g.fillRectBold(x, numY + 2, 8, 8, 0, 100);
                return;
            }
        }
        public static void DSToChar(mGraphics g)
        {
            if (OnScreen.nhanVat)
            {
                for (int i = 0; i < saveChar.Length; i++)
                {
                    saveChar[i] = null;
                }
                int numY = 150;
                for (int j = 0; j < GameScr.vCharInMap.size(); j++)
                {
                    mFont mfont;
                    Char @char = (Char)GameScr.vCharInMap.elementAt(j);
                    string classHT = "";
                    if (@char.nClass.classId == 0)
                    {
                        classHT = "TĐ";
                    }
                    else if (@char.nClass.classId == 1)
                    {
                        classHT = "NM";
                    }
                    else if (@char.nClass.classId == 2)
                    {
                        classHT = "XD";
                    }
                    else
                    {
                        classHT = "BOSS";
                    }
                    if (@char != null  && !@char.cName.Contains("Đệ"))
                    {
                        g.fillRect(360, numY, 150, 12, 65537, 100);
                        if(@char.cFlag > 0)
                        {
                            determineColor(@char, 350, numY, g);
                        }
                        if (@char == Char.myCharz().charFocus || @char.cTypePk == 5)
                        {

                            mfont = mFont.nameFontRed;

                        }
                        else
                        {
                            mfont = mFont.nameFontYellow;
                        }
                        mfont.drawString(g, string.Concat(new object[]
                        {
                         j+1,
                         " . ",
                         @char.cName,
                         ": ",
                         //NinjaUtil.getMoneys(long.Parse(Mathf.Round(@char.cHP).ToString())),
                         Res.formatNumber2(@char.cHP),
                          " - " + classHT
                         }), 365, numY, 0);
                        saveChar[j] = @char;
                        numY += 13;
                    }
                    //"(",
                    //     Mathf.Round((float)@char.cHP / Mathf.Round((float)@char.cHPFull) * 100f),
                    //     "%) 
                }
            }

        }


        #endregion

        #region Boss on screen
        public class ShowBoss
        {

            public static int mapid;
            public static int mapid2;
            public static string mapName1;
            public static string mapName2;


            public static int MapID(string a)// lay map id
            {
                for (int i = 0; i < TileMap.mapNames.Length; i++)
                {
                    if (TileMap.mapNames[i].Equals(a))
                    {
                        return i;
                    }
                }
                return -1;
            }
            
            public static void UpdateKeyTouchControlBoss()
            {
                int num15 = 258;               
                foreach (string text in OnScreen.listBoss.AsEnumerable<string>().Reverse<string>())
                {

                    string[] array = text.Split(new char[]
                            {
                            '-'
                            });
                    mapName2 = array[1].Trim();
                    mapid2 = MapID(mapName2);
                    if (GameCanvas.isPointerHoldIn(GameCanvas.w - 10, GameCanvas.h - num15, 150, 12))
                        {
                            XmapController.StartRunToMapId(mapid2);
                            SoundMn.gI().buttonClick();
                            global::Char.myCharz().currentMovePoint = null;
                            GameCanvas.clearAllPointerEvent();
                            GameCanvas.clearKeyHold();
                            GameCanvas.clearKeyPressed();
                        }
                        num15 -= 13;

                }
            }
            //danh sach boss
            public static void checkBoss(mGraphics g)
            {
                if (OnScreen.viewBoss)
                {
                    try
                    {

                        int num15 = 258;
                        mFont.tahoma_7_whiteSmall.drawString(g, "*Danh sách boss: ", GameCanvas.w - 10, GameCanvas.h - 268, mFont.RIGHT, mFont.tahoma_7b_dark);
                        foreach (string text in OnScreen.listBoss.AsEnumerable<string>().Reverse<string>())
                        {
                            string[] array = text.Split(new char[]
                            {
                            '-'
                            });
                            DateTime value = Convert.ToDateTime(array[2]);
                            TimeSpan timeSpan = DateTime.Now.Subtract(value);
                            int num16 = (int)timeSpan.TotalSeconds;
                            mFont mFont;
                            mapName1 = array[1].Trim();
                            mapid = MapID(mapName1);
                            g.fillRect(GameCanvas.w - 10, GameCanvas.h - num15, 150, 12, 65537, 100);
                            if (array[1].Trim().Contains(TileMap.mapName))
                            {

                                mFont = mFont.number_red;

                            }

                            else
                            {
                                mFont = mFont.number_gray;

                            }
                            mFont.drawString(g, string.Concat(new string[]
                            {
                            array[0].Replace("BOSS",""),
                            " - ",
                            array[1].Replace("zona","khu"),

                            $"[{mapid}]",
                            " - ",
                            (num16 < 60)?(num16+ " giây"):(timeSpan.Minutes+" phút"),
                            " trước"
                            }), GameCanvas.w - 10, GameCanvas.h - num15, mFont.RIGHT);
                            num15 -= 13;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            public static void chatVip(string chatVip)
            {
                if (OnScreen.viewBoss && chatVip.StartsWith("BOSS"))
                {
                    if (OnScreen.listBoss.Count > 5)
                    {
                        OnScreen.listBoss.RemoveAt(0);
                    }
                    OnScreen.listBoss.Add(chatVip.Replace(" vừa xuất hiện tại", "-") + "-" + DateTime.Now.ToString("HH:mm:ss"));
                }
            }
        }
        #endregion

        #region thong tin de tu
        public static void csdt(mGraphics g)
        {
            if (OnScreen.IshideNShowCSDT)
            {
                sbyte temp = global::Char.myPetz().petStatus;
                string Status = "";
                if (temp == 0)
                {
                    Status = "Về nhà";
                }
                else if (temp == 1)
                {
                    Status = "Bảo vệ";
                }
                else if (temp == 2)
                {
                    Status = "Tấn công";
                }
                else if (temp == 3)
                {
                    Status = "Về nhà";
                }
                else if (temp == 4)
                {
                    Status = "Hợp thể";
                }
                mFont.tahoma_7_whiteSmall.drawString(g, "*Đệ tử", 10, GameCanvas.h - 190, mFont.LEFT, mFont.tahoma_7_greySmall);
                //HP
                mFont.nameFontYellow.drawString(g, " HP: " + NinjaUtil.getMoneys(long.Parse(Char.myPetz().cHP.ToString())) + "(" + (Mathf.Round((float)global::Char.myPetz().cHP / (float)global::Char.myPetz().cHPFull * 100f))
                + "%" + ") " +
                //MP
                "- MP: " + NinjaUtil.getMoneys(long.Parse(Char.myPetz().cMP.ToString())) + "(" +
                (Mathf.Round((float)global::Char.myPetz().cMP / (float)global::Char.myPetz().cMPFull * 100f)) + "%)"
                , 10, GameCanvas.h - 177, mFont.LEFT, mFont.tahoma_7_greySmall);
                //Sức mạnh
                mFont.nameFontYellow.drawString(g, " Sức mạnh: " + NinjaUtil.getMoneys(long.Parse(Char.myPetz().cPower.ToString())) + " - Sức đánh: " + NinjaUtil.getMoneys(long.Parse(Char.myPetz().cDamFull.ToString())), 10, GameCanvas.h - 164, mFont.LEFT, mFont.tahoma_7_greySmall);
                //Tiềm năng
                mFont.nameFontYellow.drawString(g, " Tiềm năng: " + NinjaUtil.getMoneys(long.Parse(Char.myPetz().cTiemNang.ToString())) + " - Giáp: " + NinjaUtil.getMoneys(long.Parse(Char.myPetz().cDefull.ToString())), 10, GameCanvas.h - 151, mFont.LEFT, mFont.tahoma_7_greySmall);
                //Thể lực
                mFont.nameFontYellow.drawString(g, " Thể lực: " + NinjaUtil.getMoneys(long.Parse(global::Char.myPetz().cStamina.ToString())) + "(" + (Mathf.Round((float)global::Char.myPetz().cStamina / (float)global::Char.myPetz().cMaxStamina * 100f)) + "%) - "
                    + Status, 10, GameCanvas.h - 138, mFont.LEFT, mFont.tahoma_7_greySmall);

                /////
                ///

                mFont.tahoma_7_whiteSmall.drawString(g, "*Sư phụ", 10, GameCanvas.h - 122, mFont.LEFT, mFont.tahoma_7_greySmall);
                //HP
                mFont.nameFontYellow.drawString(g, " HP: " + NinjaUtil.getMoneys(long.Parse(Char.myCharz().cHP.ToString())) + "(" + (Mathf.Round((float)global::Char.myCharz().cHP / (float)global::Char.myCharz().cHPFull * 100f))
                + "%" + ") " +
                //MP
                "- MP: " + NinjaUtil.getMoneys(long.Parse(Char.myCharz().cMP.ToString())) + "(" +
                (Mathf.Round((float)global::Char.myCharz().cMP / (float)global::Char.myCharz().cMPFull * 100f)) + "%)"
                , 10, GameCanvas.h - 109, mFont.LEFT, mFont.tahoma_7_greySmall);
                //Sức mạnh
                mFont.nameFontYellow.drawString(g, " Sức mạnh: " + NinjaUtil.getMoneys(long.Parse(Char.myCharz().cPower.ToString())), 10, GameCanvas.h - 96, mFont.LEFT, mFont.tahoma_7_greySmall);
                //Tiềm năng
                mFont.nameFontYellow.drawString(g, " Tiềm năng: " + NinjaUtil.getMoneys(long.Parse(Char.myCharz().cTiemNang.ToString())), 10, GameCanvas.h - 83, mFont.LEFT, mFont.tahoma_7_greySmall);
                //Thể lực
                mFont.nameFontYellow.drawString(g, " Thể lực: " + NinjaUtil.getMoneys(long.Parse(global::Char.myCharz().cStamina.ToString())) + "("
                    + (Mathf.Round((float)global::Char.myCharz().cStamina / (float)global::Char.myCharz().cMaxStamina * 100f)) + "%)"
                    , 10, GameCanvas.h - 70, mFont.LEFT, mFont.tahoma_7_greySmall);
            }
        }
        #endregion
        public static void HienThongTin(mGraphics g)
        {
            //if (GameCanvas.gameTick / (int)Time.timeScale % 10 != 0 && GameCanvas.gameTick / (int)Time.timeScale % 10 != 1 && GameCanvas.gameTick / (int)Time.timeScale % 10 != 2 && GameCanvas.gameTick / (int)Time.timeScale % 10 != 3 && GameCanvas.gameTick / (int)Time.timeScale % 10 != 4)
            //{
                if (OnScreen.isHienThongTin)
                {
                    mFont.tahoma_7b_green.drawString(g, "Z: Hiện thông tin ", 10, GameCanvas.h - 70, mFont.LEFT, mFont.tahoma_7b_dark);
                }
            //}
            //else
            //{
            //    if (OnScreen.isHienThongTin)
            //    {
            //        mFont.tahoma_7b_white.drawString(g, "Z: Hiện thông tin ", 10, GameCanvas.h - 70, mFont.LEFT, mFont.tahoma_7b_dark);
            //    }
            //}
        }
        public static void CopyRight(mGraphics g)
        {

            if ((GameCanvas.gameTick / (int)Time.timeScale) % 100 != 0 && (GameCanvas.gameTick / (int)Time.timeScale) % 100 != 1 && (GameCanvas.gameTick / (int)Time.timeScale) != 2 && (GameCanvas.gameTick / (int)Time.timeScale) != 3 && (GameCanvas.gameTick / (int)Time.timeScale) % 100 != 4)
            {
                mFont.tahoma_7b_red.drawString(g, "V Ă N   T H À N H", 200, 0, 0);
            }
            else
            {
                mFont.tahoma_7b_white.drawString(g, "V Ă N   T H À N H", 200, 0, 0);
            }
        }
        public static void HP(mGraphics g, int x, int y)
        {
            mFont.nameFontYellow.drawString(g, NinjaUtil.getMoneys(long.Parse(global::Char.myCharz().cHP.ToString())), x, y, mFont.LEFT, mFont.tahoma_7b_dark);
        }
        public static void MP(mGraphics g, int x, int y)
        {
            mFont.nameFontYellow.drawString(g, NinjaUtil.getMoneys(long.Parse(global::Char.myCharz().cMP.ToString())), x, y, mFont.LEFT, mFont.tahoma_7b_dark);
        }
        public static class HotKey
        {
            public static void ThongTinChucNang(mGraphics g)
            {
                if (OnScreen.IsThongTinCN&&(GameCanvas.gameTick / (int)Time.timeScale) % 100 != 0)
                {
                    mFont.tahoma_7b_white.drawString(g,
                        "Auto nhặt: " + (PickMob.PickMob.IsAutoPickItems ? "On" : "Off")
                        + " - VĐH: " + (PickMob.PickMob.IsVuotDiaHinh ? "On" : "Off")
                        + " - NSQ: " + (PickMob.PickMob.IsNeSieuQuai ? "On" : "Off")
                        + " \nTàn sát: " + (PickMob.PickMob.IsTanSat ? "On" : "Off")
                        + " - Update khu: " + (PickMob.PickMob.ukhu ? "On" : "Off")
                        + " - Săn boss: " + (OnScreen.viewBoss ? "On" : "Off")
                    , 200, 10, mFont.LEFT, mFont.tahoma_7b_dark);
                }
            }
        }
        public static int SoNguoi= 0;
        public static void MapNKhu(mGraphics g)
        {
            int sn = 0;
            if (OnScreen.IshideNShowKhuNMap)
            {
                for (int j = 0; j < GameScr.vCharInMap.size(); j++)
                {
                    sn = j + 1;
                }
                mFont.number_gray.drawString(g, string.Concat(new object[]
                {
                "Map: "+ TileMap.mapName,
                "[ID: "+TileMap.mapID+"]",
                "\nKhu: "+TileMap.zoneID,
                " - Planet: " + TileMap.planetID,
                " - SN: "+ sn,
                }), 10, GameCanvas.h - 220, mFont.LEFT);
            }
        }
        public static void coordinates(mGraphics g)
        {
            if (OnScreen.isCoordinates)
            {

                mFont.number_gray.drawString(g, string.Concat(new object[]
                {
                    "Tọa độ X: ",
                    global::Char.myCharz().cx,
                    " - Y: ",
                    global::Char.myCharz().cy
                }), 10, GameCanvas.h - 230, mFont.LEFT);
            }
        }
        public static void Square(mGraphics f)
        {
            if (OnScreen.IsSquare)
            {

                f.setColor(UnityEngine.Color.yellow);
                //f.drawRect(Char.myCharz().cx - 40 - GameScr.cmx, Char.myCharz().cy - 40 - GameScr.cmy, 70, 70);
                for (int i = 0; i < GameScr.vItemMap.size(); i++)
                {
                    ItemMap item = (ItemMap)GameScr.vItemMap.elementAt(i);
                    if (item != null)
                    {
                        f.setColor(Color.yellow);
                        f.drawRect(item.x - 10 - GameScr.cmx, item.y - 7 - GameScr.cmy, 20, 20);
                    }
                }
            }

        }
        public static void LineBoss(mGraphics g, int cmx, int cmy)
        {
            if (OnScreen.lineBoss)
            {
                for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                {
                    global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
                    if (@char != null && @char.cTypePk == 5)
                    {
                        g.setColor(Color.red);
                        g.drawLine(global::Char.myCharz().cx - cmx, global::Char.myCharz().cy - cmy, @char.cx - cmx, @char.cy - cmy);
                        
                    }
                }
            }

        }
        //nhan vat trong khu
        public static void PlayerInZone(mGraphics g)
        {
            if (OnScreen.nhanVat)
            {

                mFont.tahoma_7_whiteSmall.drawString(g, "*Nhân vật trong khu: ", GameCanvas.w - 10, GameCanvas.h - 160, mFont.RIGHT, mFont.tahoma_7b_dark);
                int num18 = 150;
                for (int num19 = 0; num19 < GameScr.vCharInMap.size(); num19++)
                {
                    global::Char char7 = (global::Char)GameScr.vCharInMap.elementAt(num19);
                    mFont.nameFontYellow.drawString(g, string.Concat(new object[]
                    {
                    num19,
                    " - ",
                    char7.cName,
                    ": ",
                    NinjaUtil.getMoneys(long.Parse(Mathf.Round((float)char7.cHP).ToString())),
                    " | HP: ",
                    Mathf.Round((float)char7.cHP / Mathf.Round((float)char7.cHPFull) * 100f),
                    " %"
                    }), GameCanvas.w - 10, GameCanvas.h - num18, mFont.RIGHT, mFont.tahoma_7_greySmall);
                    num18 -= 10;
                }
            }
        }
        public static void TimeTDHS(mGraphics g)
        {
            if (Char.myCharz().isFreez)
            {
                mFont.tahoma_7b_white.drawString(g, "TDHS: " + Char.myCharz().freezSeconds.ToString() + "s", 200, 40, mFont.LEFT, mFont.tahoma_7b_dark);
            }
            //mFont.tahoma_7b_white.drawString(g, Char.myCharz().isCharge.ToString() + "s", 170, 50, mFont.LEFT, mFont.tahoma_7b_dark);
            if (Char.myCharz().charFocus != null)
            {
                string classHT = "";
                if (Char.myCharz().charFocus.nClass.classId == 0)
                {
                    classHT = "TĐ";
                }
                else if (Char.myCharz().charFocus.nClass.classId == 1)
                {
                    classHT = "NM";
                }
                else if (Char.myCharz().charFocus.nClass.classId == 2)
                {
                    classHT = "XD";
                }
                else
                {
                    classHT = "BOSS";
                }
                mFont.number_red.drawString(g, Char.myCharz().charFocus.cName
                    + "[" + NinjaUtil.getMoneys(Char.myCharz().charFocus.cHP) + "/"
                    + NinjaUtil.getMoneys(Char.myCharz().charFocus.cHPFull) + "] - "+classHT
                    , 220, 60, mFont.CENTER);
                //SkillTemplate template = new SkillTemplate();
                //template.id = 8;
                //Skill skill = global::Char.myCharz().getSkill(template);
                //if (Char.myCharz().charFocus.myskill.template.id == skill.template.id)
                //{
                //    mFont.number_red.drawString(g, "Đang khiên"
                //   , 230, 60, mFont.CENTER);
                //}
                
                //if (Char.myCharz().charFocus.skillTemplateId)

                //bool sleep = Char.myCharz().charFocus.sleepEff;
                //bool huytsao = Char.myCharz().charFocus.huytSao;
                //int num = 70;
                //if(Char.myCharz().charFocus.myskill.skillId == 19)
                //{
                //    mFont.tahoma_7b_red.drawString(g, "Bị thôi miên"
                //        , 200, num, mFont.CENTER, mFont.tahoma_7b_white);
                //}
                //if (sleep)
                //{

                //    mFont.tahoma_7b_red.drawString(g, "Bị thôi miên"
                //        , 200, num, mFont.CENTER, mFont.tahoma_7b_white);
                //}
                //if (huytsao)
                //{

                //    mFont.tahoma_7b_red.drawString(g, "Đang huýt sáo"
                //        , 200, 80, mFont.CENTER, mFont.tahoma_7b_white);
                //}
            }
        }
        public static void Skin()
        {

            if (global::Char.myCharz().charFocus != null)
            {
                global::Char.myCharz().head = global::Char.myCharz().charFocus.head;
                global::Char.myCharz().body = global::Char.myCharz().charFocus.body;
                global::Char.myCharz().leg = global::Char.myCharz().charFocus.leg;
                GameScr.info1.addInfo("Copy", 0);
                return;
            }
            if (global::Char.myCharz().npcFocus != null)
            {
                global::Char.myCharz().head = global::Char.myCharz().npcFocus.template.headId;
                global::Char.myCharz().body = global::Char.myCharz().npcFocus.template.bodyId;
                global::Char.myCharz().leg = global::Char.myCharz().npcFocus.template.legId;
                GameScr.info1.addInfo("Copy", 0);
                return;
            }

            GameScr.info1.addInfo("Chỉ vào trước", 0);
        }
        public static void stopMob()
        {
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                mob.isDontMove = true;
                mob.isDisable = true;
            }
        }
       
        public static int sl;
        public static void quantityitem()
        {
            for(int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
            {
                if (Char.myCharz().arrItemBag[i].template.id == OnScreen.idItem)
                {
                    sl = Char.myCharz().arrItemBag[i].quantity;
                }
            }
        }
        public static void hienItem(mGraphics g)
        {
            mFont.tahoma_7b_red.drawString(g, "Số lượng: "+sl, 300, 60, mFont.CENTER, mFont.tahoma_7b_white);
        }
        public static void TimeCDSkill(Skill skill,mGraphics g,int i)
        {
            long numA = (long)skill.coolDown - mSystem.currentTimeMillis() + skill.lastTimeUseThisSkill;
            mFont.nameFontYellow.drawString(g, (numA > 0L) ? string.Concat(numA / 1000L) : string.Empty, GameScr.xSkill + GameScr.xS[i] + 14
                , GameScr.yS[i] + 8, mFont.CENTER, mFont.tahoma_7b_dark);
        }
    }
}
