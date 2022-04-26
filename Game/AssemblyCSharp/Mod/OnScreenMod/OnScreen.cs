using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.IO;
using System.Diagnostics;
using AssemblyCSharp.Mod.OnScreenMod;

namespace AssemblyCSharp.Mod.PickMob
{
    public class OnScreen
    {
     
        public static bool viewBoss = true;
        public static List<string> listBoss = new List<string>();

        public static bool IsAutoLogin = true;
        public static bool IshideNShowCSDT ;
        public static bool IshideNShowKhuNMap ;

        
        public static int tdc = 10;

        public static bool IsXinDau;
        public static bool IsChoDau;
        public static bool lineBoss = true;

        public static bool ukhu;

        public static bool isCharAutoFocus;
        public static global::Char CharAutoFocus;
        public static MyVector vCharInMap = new MyVector();
        public static int cmx;
        public static int cmy;
        public static bool nhanVat;

        public static bool isCoordinates;
        public static bool IsXoaMap = false;
        public static bool IsXoaMapV;
        public static MyVector showBoss = new MyVector();
        public static int cpChatVip = 1;
        public static bool isHienThongTin = true;
        public static bool IsSquare = true;
        public static bool IsThongTinCN;
        public static int idItem;
        public static bool IsHienItem;
        public static bool isStopMob;
        public static bool ISCopySkin;
        public static bool IsHideMob;
        public static bool isHideP;
        #region Chat
        public static bool Chat(string text)
        {
            //mini map
            
            //chi so pet
            if (text == "ttcs")
            {
                IshideNShowCSDT = !IshideNShowCSDT;
                //GameScr.info1.addInfo("Chỉ số đệ tử: " + (IshideNShowCSDT ? "Bật" : "Tắt"), 0);
            }
           
            //san boss
            else if (text == "sb")
            {
                viewBoss = !viewBoss;
                GameScr.info1.addInfo($"Hiển thị boss: " + (viewBoss ? "Bật" : "Tắt"), 0);
            }
         
            else if (text == "minfo")
            {
                IshideNShowKhuNMap = !IshideNShowKhuNMap;
                //GameScr.info1.addInfo($"Hiện thị thông tin map" + (IshideNShowKhuNMap ? " Bật" : " Tắt"), 0);
            }
            else if (text == "lineb")
            {
                lineBoss = !lineBoss;
                GameScr.info1.addInfo($"Đường kẻ đến boss: " + (lineBoss ? " Bật" : " Tắt"), 0);
            }
            else if (text == "pinfo")
            {
                nhanVat = !nhanVat;
                //GameScr.info1.addInfo($"Các nhân vật trong khu: " + (nhanVat ? " Bật" : " Tắt"), 0);
            }
            else if (text == "toado")
            {
                isCoordinates = !isCoordinates;
                //GameScr.info1.addInfo($"Toạ độ: " + (isCoordinates ? " Bật" : " Tắt"), 0);
            }
            else if (text == "gdl")
            {
                IsXoaMap = !IsXoaMap;
                GameScr.info1.addInfo($"Xoá map: " + (IsXoaMap ? " Bật" : " Tắt"), 0);
            }
            else if (text == "gdlv")
            {
                IsXoaMapV = !IsXoaMapV;
                Application.targetFrameRate = 60;
                GameScr.info1.addInfo($"Xoá map vip: " + (IsXoaMapV ? " Bật" : " Tắt"), 0);
            }
            else if (text == "hienthongtin")
            {
                isHienThongTin = !isHienThongTin;
                
            }
            else if (text == "hinhvuong")
            {
                IsSquare = !IsSquare;

            }
            else if (text == "chucnang")
            {
                IsThongTinCN = !IsThongTinCN;

            }
            else if (text == "cps")
            {
                CopySkins();
            }
            else if (text == "hienitem")
            {
                IsHienItem = !IsHienItem;

            }
            else if (IsGetInfoChat<int>(text, "hitem"))
            {
                idItem = GetInfoChat<int>(text, "hitem");

            }
            else if (text == "fz")
            {
                isStopMob = !isStopMob;
            }
            else if (text == "hidemob")
            {
                IsHideMob = !IsHideMob;
                GameScr.info1.addInfo($"Ẩn quái: " + (IsHideMob ? " Bật" : " Tắt"), 0);
            }
            else if (text == "hidep")
            {
                isHideP = !isHideP;
                GameScr.info1.addInfo(Val.HidePlayer + (isHideP ? " On" : " Off"), 0);
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion
        public static bool HotKeys()
        {
            switch (GameCanvas.keyAsciiPress)
            {
                case 'z':
                    Chat("pinfo");
                    //Chat("ttdt");
                    Chat("toado");
                    Chat("minfo");
                    Chat("hienthongtin");
                    Chat("chucnang");
                    break;
                case 'p':
                    Chat("cps");
                    break;
                case 'u':
                    Chat("gdlv");
                    break;
                default:
                    return false;
            }
            return true;
        }
        public static void OnscreenUpdate(mGraphics g,int cmx,int cmy)
        {
            Square(g);
            LineBoss(g, cmx, cmy);
            OnScreen.HienThongTin(g);
            OnScreen.HienThiMapNKhu(g);
            OnScreen.Coordinates(g);
            OnScreen.checkBoss(g);
            OnScreen.copyRight(g);
            OnScreen.csdt(g);
            //OnScreen.playerInZone(g);
            OnScreen.Hotkey(g);
            //OnScreen.HPnMP(g);
            OnScreen.TimeTDHS(g);
            OnScreen.DSToChar(g);
            //hienItem(g);
            OnScreen.stopMob();
        }
       
        public static void TimeCDSkill(Skill skill, mGraphics g, int i)
        {
            OnScreenController.TimeCDSkill(skill, g, i);
        }
        public static void stopMob()
        {
            if (isStopMob)
            {
                OnScreenController.stopMob();
            }
            
        }
        public static void CopySkins()
        {
            
                OnScreenController.Skin();
            
            
        }
        
        #region Onscreen
        public static void UpdateKeyTouchControl()
        {
            OnScreenController.UpdateKeyTouchControl();
            OnScreenController.ShowBoss.UpdateKeyTouchControlBoss();
        }       
        public static void Hotkey(mGraphics g)
        {
            OnScreenController.HotKey.ThongTinChucNang(g);
        }
        public static void DSToChar(mGraphics g)
        {
            OnScreenController.DSToChar(g);
        }
        public static void TimeTDHS(mGraphics g)
        {
            OnScreenController.TimeTDHS(g);
        }
        public static void HienThongTin(mGraphics g)
        {
            OnScreenController.HienThongTin(g);
        }
        public static void chatVip(string chatVip)
        {
            OnScreenController.ShowBoss.chatVip(chatVip);
        }
        public static void copyRight(mGraphics g)
        {
            OnScreenController.CopyRight(g);
        }
        public static void checkBoss(mGraphics g)
        {
            OnScreenController.ShowBoss.checkBoss(g);
           
        }
        public static void HP(mGraphics g,int x, int y)
        {
            OnScreenController.HP(g,x,y);
        }
        public static void MP(mGraphics g, int x, int y)
        {
            OnScreenController.MP(g, x, y);
        }
        public static void csdt(mGraphics g)
        {
            OnScreenController.csdt(g);
        }
        public static void HienThiMapNKhu(mGraphics g)
        {
            OnScreenController.MapNKhu(g);
        }
        public static void Square(mGraphics g)
        {
            //copy vào gamescr
            OnScreenController.Square(g);
        }
        public static void LineBoss(mGraphics g,int cmx, int cmy)
        {
            OnScreenController.LineBoss(g,cmx,cmy);
        }
        public static void playerInZone(mGraphics g)
        {
            OnScreenController.PlayerInZone(g);
        }
        public static void Coordinates(mGraphics g)
        {
            OnScreenController.coordinates(g);
        }

        #endregion
        #region Không cần liên kết với game
        private static bool IsGetInfoChat<T>(string text, string s)
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

        private static T GetInfoChat<T>(string text, string s)
        {
            return (T)Convert.ChangeType(text.Substring(s.Length), typeof(T));
        }

        private static bool IsGetInfoChat<T>(string text, string s, int n)
        {
            if (!text.StartsWith(s))
            {
                return false;
            }
            try
            {
                string[] vs = text.Substring(s.Length).Split(' ');
                for (int i = 0; i < n; i++)
                {
                    Convert.ChangeType(vs[i], typeof(T));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static T[] GetInfoChat<T>(string text, string s, int n)
        {
            T[] ts = new T[n];
            string[] vs = text.Substring(s.Length).Split(' ');
            for (int i = 0; i < n; i++)
            {
                ts[i] = (T)Convert.ChangeType(vs[i], typeof(T));
            }
            return ts;
        }
        #endregion

    }
}
