using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AssemblyCSharp.Mod.PickMob
{
    public class UseItem
    {
        
        public static void useItem(int id)
        {
          

            try
            {
                
                for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                {
                    if (global::Char.myCharz().arrItemBag[i].template.id == id)
                    {
                        Service.gI().useItem(0, 1, (sbyte)i, -1);
                        GameScr.info1.addInfo("Đã sử dụng item ( " + Char.myCharz().arrItemBag[i].template.name + " )", 0);
                        break;
                    }
                }
                

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
            
        }
        public static int findYadrat()
        {
            
                sbyte itemID = 0;

                while (itemID < global::Char.myCharz().arrItemBag.Length)
                {
                    if (global::Char.myCharz().arrItemBag[itemID].template.name.Contains("Yardrat"))
                    {
                        return itemID;
                    }
                    itemID++;
                }
                return -1;

           
        }
        public static void usePorata()
        {
            try
            {
                sbyte itemID = 0;
       
                while (itemID < global::Char.myCharz().arrItemBag.Length)
                {
                    if (global::Char.myCharz().arrItemBag[(int)itemID].template.id == 454)
                    {
                        Service.gI().useItem(0, 1, itemID, -1);
                        Service.gI().petStatus(3);
                        break;
                    }
                    itemID++;
                }
                GameScr.info1.addInfo("Đã sử dụng item Porata", 0);

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static void usePean()
        {
            try
            {

                while (PickMobAuto.IsAutoBuffDauTheoSec)
                {
                    GameScr.gI().doUseHP();
                    Thread.Sleep(TimeSpan.FromSeconds(PickMobAuto.SecondBuffTheoSec));
                }
                GameScr.info1.addInfo("Buff đậu sau " + PickMobAuto.SecondBuffTheoSec + "s", 0);

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static void useCN()
        {
            try
            {
                while (PickMobAuto.IsACN)
                {
                    for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                    {
                        if (global::Char.myCharz().arrItemBag[i].template.id == 381)
                        {
                            Service.gI().useItem(0, 1, (sbyte)i, -1);
                            GameScr.info1.addInfo("Sử dụng cuồng nộ", 0);
                            break;
                        }
                    }
                    Thread.Sleep(600000);
                }

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static void usehopqua()
        {
            try
            {
                sbyte itemID = 0;
                if (PickMobAuto.Ishopqua)
                {
                    while (itemID < global::Char.myCharz().arrItemBag.Length)
                    {
                        if (global::Char.myCharz().arrItemBag[(int)itemID].template.id == 648)
                        {
                            Service.gI().useItem(0, 1, itemID, -1);

                            break;
                        }
                        itemID++;
                    }
                    GameScr.info1.addInfo("Đang sử dụng item hộp quà", 0);
                }
                

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static void useBH()
        {
            try
            {
                while (PickMobAuto.IsABH)
                {
                    for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                    {
                        if (global::Char.myCharz().arrItemBag[i].template.id == 382)
                        {
                            Service.gI().useItem(0, 1, (sbyte)i, -1);
                            GameScr.info1.addInfo("Sử dụng bổ huyết", 0);
                            break;
                        }
                    }
                    Thread.Sleep(600000);
                }

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static void useGX()
        {
            try
            {
                while (PickMobAuto.IsAGX)
                {
                    for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                    {
                        if (global::Char.myCharz().arrItemBag[i].template.id == 384)
                        {
                            Service.gI().useItem(0, 1, (sbyte)i, -1);
                            GameScr.info1.addInfo("Sử dụng giáp xên", 0);
                            break;
                        }
                    }
                    Thread.Sleep(600000);
                }

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static void useBK()
        {
            try
            {
                while (PickMobAuto.IsABK)
                {
                    for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                    {
                        if (global::Char.myCharz().arrItemBag[i].template.id == 383)
                        {
                            Service.gI().useItem(0, 1, (sbyte)i, -1);
                            GameScr.info1.addInfo("Sử dụng bổ khí", 0);
                            break;
                        }
                    }
                    Thread.Sleep(600000);
                }

            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }
        public static int useColdItem()
        {
           
                sbyte itemID = 0;

                while (itemID < global::Char.myCharz().arrItemBag.Length)
                {
                    if (global::Char.myCharz().arrItemBag[itemID].template.name.Contains("Frost")||global::Char.myCharz().arrItemBag[itemID].template.name.Contains("Băng"))
                    {
                        return itemID;
                    }
                    itemID++;
                }

                return -1;

           
        }
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
