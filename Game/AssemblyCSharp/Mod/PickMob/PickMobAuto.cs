using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.IO;
using System.Collections;
using AssemblyCSharp.Mod.SaveSetting;
using AssemblyCSharp.Mod.Xmap;

namespace AssemblyCSharp.Mod.PickMob
{
    public class PickMobAuto
    {
        private
        const int ID_ITEM_GEM = 77;
        private
        const int ID_ITEM_GEM_LOCK = 861;
        private
        const int DEFAULT_HP_BUFF = 20;
        private
        const int DEFAULT_MP_BUFF = 20;
        private static readonly sbyte[] IdSkillsBase = {
      4,
      0,
      2,
      17
    };
        private static readonly short[] IdItemBlockBase = {
      225,
      353,
      354,
      355,
      356,
      357,
      358,
      359,
      360,
      362
    };

        public static bool IsTanSat = false;
        public static bool IsNeSieuQuai = true;
        public static bool IsVuotDiaHinh;
        public static List<int> IdMobsTanSat = new();
        public static List<int> TypeMobsTanSat = new();
        public static List<sbyte> IdSkillsTanSat = new(IdSkillsBase);

        public static bool IsAutoPickItems = false;
        public static bool IsItemMe = true;
        public static bool IsLimitTimesPickItem = true;
        public static int TimesAutoPickItemMax = 7;
        public static List<short> IdItemPicks = new();
        public static List<short> IdItemBlocks = new(IdItemBlockBase);
        public static List<sbyte> TypeItemPicks = new();
        public static List<sbyte> TypeItemBlock = new();

        public static int HpBuff = 0;
        public static int MpBuff = -1;

        public static bool IsCSDT;
        public static List<string> listBoss = new List<string>();

        public static bool IsAutoLogin = false;
        public static bool IshideNShowCSDT = true;
        public static bool IshideNShowKhuNMap = true;

        public static bool IsminiMap = true;
        public static int tdc = 3;
        public static float speed = 3f;

        public static bool IsXinDau;
        public static bool IsChoDau;
        public static bool lineBoss = true;

        public static bool ukhu = true;

        public static bool isCharAutoFocus;
        public static global::Char CharAutoFocus;
        public static string mapNameAutoFocus;
        public static bool IsStart;
        public static bool IsAutoChat;
        public static bool IsAutoChatTG;
        public static bool IsAutoFocusBoss;
        public static bool IsAutoNeBoss;
        public static bool IsAutoFlag;
        public static bool IsAutoThuDau;
        public static bool IsAutoThuDauXin;
        public static bool IsAutoBuffDauTheoSec;
        public static int SecondBuffTheoSec = 300;
        public static bool IsAnDuiGa = true;
        public static bool IsAK;
        public static bool IsDT;
        public static bool IsNhatDoUpDT;
        public static bool IsRevive = true;
        public static bool IsAutoCSKB;
        public static bool IsACN, IsABH, IsABK, IsAGX, Ishopqua;
        public static bool IsAKOK;
        public static bool IsKOKMove;
        public static bool IsSkill3;
        public static bool IsPaint;
        public static bool IsAcGiaoDich;
        public static bool IsKhoaMap;
        public static bool IsKhoaKhu;
        public static bool IsAEnter;
        public static bool IsABDKB;
        public static Npc idNPC;
        public static bool IsTuHS;
        public static int TuHSTime = 44000;
        public static int cx;
        public static int cy;
        public static int ChenNum;
        public static int DoNum = 0;
        public static int Framerate = 30;
        public static bool kvt = false;
        public static bool isEnter = false;
        public static bool isXinDauV;
        public static string idC = null;
        public static bool IsAutoSkill3HPMP;
        public static bool IsChenKhu;
        public static bool IsDoKhu;
        public static bool IsSanAnTrom;
        public static bool IsAttackForPet = true;
        public static bool IsWallPaper = true;
        public static string fileName;
        public static Image wallPaper;
        static string[] files = Directory.GetFiles(@"WPP");
        private static PickMobAuto instance;
        public static double BuysDelay = 0.4;
        public static int Quantity = 1;
        public static string MapBuysItem;
        public static string MapSellItem;
        public static int Quantity_USE = 1;
        public static int Up;
        public static int Down;
        public static int Left;
        public static int Right;
        public static double UseDelay = 0.8;
        public static bool isMobFollow;
        public static bool MacSet1t = false;
        public static bool MacSet2t = false;
        public static bool isSell = false;
        public static bool isBanVang = false;
        public static bool isLoadSetting = false;
        public static int uItem ;
        #region Chat
        public static bool Chat(string text)
        {
            if (text == "add")
            {
                Mob mob = Char.myCharz().mobFocus;
                ItemMap itemMap = Char.myCharz().itemFocus;
                if (mob != null)
                {
                    if (IdMobsTanSat.Contains(mob.mobId))
                    {
                        IdMobsTanSat.Remove(mob.mobId);
                        GameScr.info1.addInfo("Đã xoá mob: " + mob.mobId, 0);
                    }
                    else
                    {
                        IdMobsTanSat.Add(mob.mobId);
                        GameScr.info1.addInfo("Đã thêm mob: " + mob.mobId, 0);
                    }
                }
                else if (itemMap != null)
                {
                    if (IdItemPicks.Contains(itemMap.template.id))
                    {
                        IdItemPicks.Remove(itemMap.template.id);
                        GameScr.info1.addInfo($"Đã xoá khỏi danh sách chỉ tự động nhặt item: {itemMap.template.name}[{itemMap.template.id}]", 0);
                    }
                    else
                    {
                        IdItemPicks.Add(itemMap.template.id);
                        GameScr.info1.addInfo($"Đã thêm vào danh sách chỉ tự động nhặt item: {itemMap.template.name}[{itemMap.template.id}]", 0);
                    }
                }
                else
                {
                    GameScr.info1.addInfo("Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách", 0);
                }
            }//
            else if (text == "addt")
            {
                Mob mob = Char.myCharz().mobFocus;
                ItemMap itemMap = Char.myCharz().itemFocus;
                if (mob != null)
                {
                    if (TypeMobsTanSat.Contains(mob.templateId))
                    {
                        TypeMobsTanSat.Remove(mob.templateId);
                        GameScr.info1.addInfo($"Đã xoá loại mob: {mob.getTemplate().name}[{mob.templateId}]", 0);
                    }
                    else
                    {
                        TypeMobsTanSat.Add(mob.templateId);
                        GameScr.info1.addInfo($"Đã thêm loại mob: {mob.getTemplate().name}[{mob.templateId}]", 0);
                    }
                }
                else if (itemMap != null)
                {
                    if (TypeItemPicks.Contains(itemMap.template.type))
                    {
                        TypeItemPicks.Remove(itemMap.template.type);
                        GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item:" + itemMap.template.type, 0);
                    }
                    else
                    {
                        TypeItemPicks.Add(itemMap.template.type);
                        GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item:" + itemMap.template.type, 0);
                    }
                }
                else
                {
                    GameScr.info1.addInfo("Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách", 0);
                }
            }//
            else if (text == "anhat")
            {
                IsAutoPickItems = !IsAutoPickItems;
                GameScr.info1.addInfo(Val.AutoPickItems + (IsAutoPickItems ? "On" : "Off"), 0);
            }//
            else if (text == "itm")
            {
                IsItemMe = !IsItemMe;
                GameScr.info1.addInfo(Val.LocVatPham + (IsItemMe ? "On" : "Off"), 0);
            }//
            else if (text == "sln")
            {
                IsLimitTimesPickItem = !IsLimitTimesPickItem;
                StringBuilder builder = new();
                builder.Append($"Giới hạn số lần nhặt là ");
                builder.Append(TimesAutoPickItemMax);
                builder.Append(IsLimitTimesPickItem ? ": On" : ": Off");
                GameScr.info1.addInfo(builder.ToString(), 0);
            }//
            else if (IsGetInfoChat<int>(text, "sln"))
            {
                TimesAutoPickItemMax = GetInfoChat<int>(text, "sln");
                GameScr.info1.addInfo("Số lần nhặt giới hạn là: " + TimesAutoPickItemMax, 0);
            }//
            else if (IsGetInfoChat<short>(text, "addi"))
            {
                short id = GetInfoChat<short>(text, "addi");
                if (IdItemPicks.Contains(id))
                {
                    IdItemPicks.Remove(id);
                    GameScr.info1.addInfo($"Đã xoá khỏi danh sách chỉ tự động nhặt item: {ItemTemplates.get(id).name}[{id}]", 0);
                }
                else
                {
                    IdItemPicks.Add(id);
                    GameScr.info1.addInfo($"Đã thêm vào danh sách chỉ tự động nhặt item: {ItemTemplates.get(id).name}[{id}]", 0);
                }
            }//
            else if (text == "blocki")
            {
                ItemMap itemMap = Char.myCharz().itemFocus;
                if (itemMap != null)
                {
                    if (IdItemBlocks.Contains(itemMap.template.id))
                    {
                        IdItemBlocks.Remove(itemMap.template.id);
                        GameScr.info1.addInfo($"Đã xoá khỏi danh sách không tự động nhặt item: {itemMap.template.name}[{itemMap.template.id}]", 0);
                    }
                    else
                    {
                        IdItemBlocks.Add(itemMap.template.id);
                        GameScr.info1.addInfo($"Đã thêm vào danh sách không tự động nhặt item: {itemMap.template.name}[{itemMap.template.id}]", 0);
                    }
                }
                else
                {
                    GameScr.info1.addInfo("Cần trỏ vào vật phẩm cần chặn khi auto nhặt", 0);
                }
            }//
            else if (IsGetInfoChat<short>(text, "blocki"))
            {
                short id = GetInfoChat<short>(text, "blocki");
                if (IdItemBlocks.Contains(id))
                {
                    IdItemBlocks.Remove(id);
                    GameScr.info1.addInfo($"Đã thêm vào danh sách không tự động nhặt item: {ItemTemplates.get(id).name}[{id}]", 0);
                }
                else
                {
                    IdItemBlocks.Add(id);
                    GameScr.info1.addInfo($"Đã xoá khỏi danh sách không tự động nhặt item: {ItemTemplates.get(id).name}[{id}]", 0);
                }
            }//
            else if (IsGetInfoChat<sbyte>(text, "addti"))
            {
                sbyte type = GetInfoChat<sbyte>(text, "addti");
                if (TypeItemPicks.Contains(type))
                {
                    TypeItemPicks.Remove(type);
                    GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item: " + type, 0);
                }
                else
                {
                    TypeItemPicks.Add(type);
                    GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item: " + type, 0);
                }
            }//
            else if (IsGetInfoChat<sbyte>(text, "blockti"))
            {
                sbyte type = GetInfoChat<sbyte>(text, "blockti");
                if (TypeItemBlock.Contains(type))
                {
                    TypeItemBlock.Remove(type);
                    GameScr.info1.addInfo("Đã xoá khỏi danh sách không tự động nhặt loại item: " + type, 0);
                }
                else
                {
                    TypeItemBlock.Add(type);
                    GameScr.info1.addInfo("Đã thêm vào danh sách không tự động nhặt loại item: " + type, 0);
                }
            }//
            else if (text == "clri")
            {
                IdItemPicks.Clear();
                TypeItemPicks.Clear();
                TypeItemBlock.Clear();
                IdItemBlocks.Clear();
                IdItemBlocks.AddRange(IdItemBlockBase);
                GameScr.info1.addInfo("Danh sách lọc item đã được đặt lại mặc định", 0);
            }//
            else if (text == "cnn")
            {
                IdItemPicks.Clear();
                TypeItemPicks.Clear();
                TypeItemBlock.Clear();
                IdItemBlocks.Clear();
                IdItemBlocks.AddRange(IdItemBlockBase);
                IdItemPicks.Add(ID_ITEM_GEM);
                IdItemPicks.Add(ID_ITEM_GEM_LOCK);
                GameScr.info1.addInfo("Đã cài đặt chỉ nhặt ngọc", 0);
            }
            else if (text == "ts")
            {
                IsTanSat = !IsTanSat;
                GameScr.info1.addInfo(Val.AutoAtackMob + (IsTanSat ? "On" : "Off"), 0);
            }//
            else if (text == "nsq")
            {
                IsNeSieuQuai = !IsNeSieuQuai;
                GameScr.info1.addInfo(Val.TanSatNeSieuQuai + (IsNeSieuQuai ? "On" : "Off"), 0);
            }//
            else if (IsGetInfoChat<int>(text, "addm"))
            {
                int id = GetInfoChat<int>(text, "addm");
                if (IdMobsTanSat.Contains(id))
                {
                    IdMobsTanSat.Remove(id);
                    GameScr.info1.addInfo("Đã xoá mob: " + id, 0);
                }
                else
                {
                    IdMobsTanSat.Add(id);
                    GameScr.info1.addInfo("Đã thêm mob: " + id, 0);
                }
            }//
            else if (IsGetInfoChat<int>(text, "addtm"))
            {
                int id = GetInfoChat<int>(text, "addtm");
                if (TypeMobsTanSat.Contains(id))
                {
                    TypeMobsTanSat.Remove(id);
                    GameScr.info1.addInfo($"Đã xoá loại mob: {Mob.arrMobTemplate[id].name}[{id}]", 0);
                }
                else
                {
                    TypeMobsTanSat.Add(id);
                    GameScr.info1.addInfo($"Đã thêm loại mob: {Mob.arrMobTemplate[id].name}[{id}]", 0);
                }
            }//
            else if (text == "clrm")
            {
                IdMobsTanSat.Clear();
                TypeMobsTanSat.Clear();
                GameScr.info1.addInfo("Đã xoá danh sách đánh quái", 0);
            }//
            else if (text == "skill")
            {
                SkillTemplate template = Char.myCharz().myskill.template;
                if (IdSkillsTanSat.Contains(template.id))
                {
                    IdSkillsTanSat.Remove(template.id);
                    GameScr.info1.addInfo($"Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {template.name}[{template.id}]", 0);
                }
                else
                {
                    IdSkillsTanSat.Add(template.id);
                    GameScr.info1.addInfo($"Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {template.name}[{template.id}]", 0);
                }
            }
            else if (IsGetInfoChat<int>(text, "skill"))
            {
                int index = GetInfoChat<int>(text, "skill") - 1;
                SkillTemplate template = Char.myCharz().nClass.skillTemplates[index];
                if (IdSkillsTanSat.Contains(template.id))
                {
                    IdSkillsTanSat.Remove(template.id);
                    GameScr.info1.addInfo($"Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {template.name}[{template.id}]", 0);
                }
                else
                {
                    IdSkillsTanSat.Add(template.id);
                    GameScr.info1.addInfo($"Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {template.name}[{template.id}]", 0);
                }
            }
            else if (IsGetInfoChat<sbyte>(text, "skillid"))
            {
                sbyte id = GetInfoChat<sbyte>(text, "skillid");
                if (IdSkillsTanSat.Contains(id))
                {
                    IdSkillsTanSat.Remove(id);
                    GameScr.info1.addInfo("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: " + id, 0);
                }
                else
                {
                    IdSkillsTanSat.Add(id);
                    GameScr.info1.addInfo("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: " + id, 0);
                }
            }
            else if (text == "clrs")
            {
                IdSkillsTanSat.Clear();
                IdSkillsTanSat.AddRange(IdSkillsBase);
                GameScr.info1.addInfo("Đã đặt danh sách skill sử dụng tự động đánh quái về mặc định", 0);
            }
            else if (text == "abf")
            {
                if (HpBuff == 0 && MpBuff == 0)
                {
                    GameScr.info1.addInfo(Val.UseHPPotion + "Off", 0);
                }
                else
                {
                    HpBuff = DEFAULT_HP_BUFF;
                    MpBuff = DEFAULT_MP_BUFF;
                    GameScr.info1.addInfo($"Tự động sử dụng đậu thần khi HP dưới {HpBuff}%, MP dưới {MpBuff}%", 0);
                }
            }
            else if (IsGetInfoChat<int>(text, "abf"))
            {
                HpBuff = GetInfoChat<int>(text, "abf");
                MpBuff = 0;
                GameScr.info1.addInfo($"Tự động sử dụng đậu thần khi HP dưới {HpBuff}%", 0);
            }
            else if (IsGetInfoChat<int>(text, "abf", 2))
            {
                int[] vs = GetInfoChat<int>(text, "abf", 2);
                HpBuff = vs[0];
                MpBuff = vs[1];
                GameScr.info1.addInfo($"Tự động sử dụng đậu thần khi HP dưới {HpBuff}%, MP dưới {MpBuff}%", 0);
            }
            else if (text == "vdh")
            {
                IsVuotDiaHinh = !IsVuotDiaHinh;
                GameScr.info1.addInfo("Tự động đánh quái vượt địa hình: " + (IsVuotDiaHinh ? "On" : "Off"), 0);
            }
            else if (IsGetInfoChat<int>(text, "k"))
            {
                // Lấy số nguyên sau lệnh chat kz (khu cần chuyển)
                int zone = GetInfoChat<int>(text, "k");
                if (zone != TileMap.zoneID)
                {
                    Service.gI().requestChangeZone(zone, -1);
                }
            }
            else if (text.Equals("odt"))
            {
                Service.gI().openMenu(25);
                Service.gI().confirmMenu(4, 0);
            }
            else if (IsGetInfoChat<int>(text, "npc"))
            {
                int idNpc = GetInfoChat<int>(text, "npc");
                Service.gI().openMenu(idNpc);

            }
            // kiểm tra số thực sau lệnh chat spd
            else if (IsGetInfoChat<float>(text, "spd"))
            {
                speed = GetInfoChat<float>(text, "spd");
            }
            //giao dich
            else if (text == "trade")
            {
                int id = global::Char.myCharz().charFocus.charID;
                string name = global::Char.myCharz().charFocus.cName;
                Service.gI().giaodich(0, id, -1, -1);
                GameScr.info1.addInfo($"Đã mời {name} [Id:{id}] giao dịch", 0);
            }
            //hop the
            else if (text == "ht")
            {
                UseItem.usePorata();

            }
            // su dung cuong no
            else if (text == "acn")
            {
                IsACN = !IsACN;
                Thread th = new Thread(new ThreadStart(UseItem.useCN));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoUseCuongNo + (IsACN ? "On" : "Off"), 0);
            }
            else if (text == "ahqua")
            {
                Ishopqua = !Ishopqua;
                Thread th = new Thread(new ThreadStart(UseItem.usehopqua));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo((Ishopqua ? "On" : "Off"), 0);
            }
            // su dung giap xen
            else if (text == "agx")
            {
                IsAGX = !IsAGX;
                Thread th = new Thread(new ThreadStart(UseItem.useGX));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoUseGiapXen + (IsAGX ? "On" : "Off"), 0);
            }
            // su dung bo khi
            else if (text == "abk")
            {
                IsABK = !IsABK;
                Thread th = new Thread(UseItem.useBK);
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoUseBoKhi + (IsABK ? "On" : "Off"), 0);
            }
            else if (text == "abh")
            {
                IsABH = !IsABH;
                Thread th = new Thread(new ThreadStart(UseItem.useBH));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoUseBoHuyet + (IsABH ? "On" : "Off"), 0);
            }
            //auto login
            else if (text == "alogin")
            {
                IsAutoLogin = !IsAutoLogin;
                GameScr.info1.addInfo(Val.LoginAfterSeconds + (!IsAutoLogin ? "On" : "Off"), 0);
            }
            //mini map
            else if (text == "mmap")
            {
                IsminiMap = !IsminiMap;
                GameScr.info1.addInfo("Mini Map: " + (IsminiMap ? "On" : "Off"), 0);
            }
            else if (IsGetInfoChat<int>(text, "s"))
            {
                tdc = GetInfoChat<int>(text, "s");
                GameScr.info1.addInfo("Tốc độ chạy: " + tdc, 0);
            }
            //xindau
            else if (text == "xindau")
            {
                IsXinDau = !IsXinDau;
                new Thread(delegate () {
                    PickMobAutoController.xinDau();
                })
                {
                    IsBackground = true
                }.Start();

                GameScr.info1.addInfo(Val.AutoXinDau + (IsXinDau ? " On" : " Off"), 0);
            }
            //chodau
            else if (text == "chodau")
            {
                IsChoDau = !IsChoDau;
                new Thread(delegate () {
                    PickMobAutoController.choDau();
                })
                {
                    IsBackground = true
                }.Start();

                GameScr.info1.addInfo(Val.AutoChoDau + (IsChoDau ? " On" : " Off"), 0);
            }
            else if (text == "minfo")
            {
                IshideNShowKhuNMap = !IshideNShowKhuNMap;
                GameScr.info1.addInfo(Val.ShowMapInfo + (IshideNShowKhuNMap ? " On" : " Off"), 0);
            }
            // mở khu nhanh
            else if (text == "zone")
            {
                if (TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23)
                {
                    Service.gI().openUIZone();
                    GameCanvas.panel.setTypeZone();
                    GameCanvas.panel.show();
                }

            }
            else if (text == "ukhu")
            {
                ukhu = !ukhu;

                GameScr.info1.addInfo(Val.UpdateZone + (ukhu ? " On" : " Off"), 0);
            }
            else if (text == "focus")
            {
                isCharAutoFocus = !isCharAutoFocus;
                PickMobAuto.CharAutoFocus = global::Char.myCharz().charFocus;
                GameScr.info1.addInfo("Auto focus: " + PickMobAuto.CharAutoFocus.cName, 0);
            }
            else if (text == "cpuf")
            {
                IsStart = !IsStart;
                if (IsStart)
                    Application.runInBackground = !Application.runInBackground;
            }
            else if (text == "atc")
            {
                IsAutoChat = !IsAutoChat;
                Thread th = new Thread(new ThreadStart(AutoChat));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoChat + (IsAutoChat ? " On" : " Off"), 0);
            }
            else if (IsGetInfoChat<string>(text, "atchat "))
            {
                string textc = GetInfoChat<string>(text, "atchat ");
                var AutoChat = new StreamWriter("Data\\chat.ini");
                AutoChat.Write(textc);
                AutoChat.Close();
                GameScr.info1.addInfo("Đã Lưu Chat", 0);
            }
            else if (text == "wc")
            {
                IsAutoChatTG = !IsAutoChatTG;
                Thread th = new Thread(new ThreadStart(AutoChatTG));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoChat + (IsAutoChatTG ? " On" : " Off"), 0);
            }
            else if (IsGetInfoChat<string>(text, "wchat "))
            {
                string textc = GetInfoChat<string>(text, "wchat ");
                var AutoChat = new StreamWriter("Data\\chattg.ini");
                AutoChat.Write(textc);
                AutoChat.Close();
                GameScr.info1.addInfo("Đã Lưu Chat", 0);
            }
            else if (text == "focusboss")
            {
                //IsAutoFocusBoss = !IsAutoFocusBoss;
                AutoFocusBoss();
            }
            else if (text == "friend")
            {
                Service.gI().friend(0, 1);
            }
            else if (text == "autoneboss")
            {
                IsAutoNeBoss = !IsAutoNeBoss;
                Thread th = new Thread(new ThreadStart(AutoNeBoss));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoNeBoss + (IsAutoNeBoss ? " On" : " Off"), 0);
            }
            else if (text == "af8")
            {
                IsAutoFlag = !IsAutoFlag;
                //Thread th = new Thread(new ThreadStart(AutoFlag));
                //th.IsBackground = true;
                //th.Start();
                GameScr.info1.addInfo(Val.AutoCoDen + (IsAutoFlag ? " On" : " Off"), 0);
            }
            else if (IsGetInfoChat<int>(text, "f"))
            {
                int flag = GetInfoChat<int>(text, "f");
                Service.gI().getFlag(1, (sbyte)flag);
            }
            else if (text == "set1")
            {
                //Thread th = new Thread(new ThreadStart(PickMobController. MacSet1));
                //th.IsBackground = true;
                //th.Start();
                //GameScr.info1.addInfo($"Đã mặc set 1", 0);
                MacSet1t = true;
            }
            //else if (text == "xoaset1")
            //{

            //    PickMobController.XoaSetDo1();
            //    GameScr.info1.addInfo($"Đã xoá set đồ 1", 0);
            //}
            else if (text == "set2")
            {

                new Thread(delegate () {
                    PickMobAutoController.MacSet2();
                })
                {
                    IsBackground = true
                }.Start();
                GameScr.info1.addInfo($"Đã mặc set 2", 0);
            }
            //else if (text == "xoaset2")
            //{

            //    PickMobController.XoaSetDo2();
            //    GameScr.info1.addInfo($"Đã xoá set đồ 2", 0);
            //}
            else if (text == "thudau")
            {

                IsAutoThuDau = !IsAutoThuDau;
                GameScr.info1.addInfo(Val.AutoThuDau + (IsAutoThuDau ? " On" : " Off"), 0);
            }
            else if (text == "athudau")
            {
                IsAutoThuDauXin = !IsAutoThuDauXin;
                GameScr.info1.addInfo(Val.AutoThuDau + (IsAutoThuDauXin ? " On" : " Off"), 0);
            }
            else if (text == "aduiga")
            {
                IsAnDuiGa = !IsAnDuiGa;
                GameScr.info1.addInfo(Val.AutoAnDuiGa + (IsAnDuiGa ? " On" : " Off"), 0);
            }
            else if (text == "adt")
            {
                IsDT = !IsDT;
                Thread th = new Thread(new ThreadStart(autodt));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoDoanhTrai + (IsDT ? " On" : " Off"), 0);
            }
            else if (text == "ndt")
            {
                IsNhatDoUpDT = !IsNhatDoUpDT;
                if (IsNhatDoUpDT)
                {
                    PickMobAutoController.XNhat = Char.myCharz().cx;
                    PickMobAutoController.YNhat = Char.myCharz().cy;
                }

                GameScr.info1.addInfo(Val.AutoNhatDoUpDe + (IsNhatDoUpDT ? " On" : " Off"), 0);
            }
            else if (text == "out")
            {
                Controller.isDisconnected = true;
            }
            else if (text == "ahs")
            {
                IsRevive = !IsRevive;
                GameScr.info1.addInfo(Val.AutoRevive + (IsRevive ? " On" : " Off"), 0);
            }
            else if (text == "acskb")
            {
                IsAutoCSKB = !IsAutoCSKB;

                new Thread(delegate () {
                    PickMobAutoController.AutoCSKB();
                })
                {
                    IsBackground = true
                }.Start();

                GameScr.info1.addInfo(Val.AutoOpenCSKB + (IsAutoCSKB ? " On" : " Off"), 0);
            }
            else if (text == "akok")
            {
                IsAKOK = !IsAKOK;
                Thread th = new Thread(new ThreadStart(PickMobAutoController.AutoKOK));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo(Val.AutoUpKaioKen + (IsAKOK ? " On" : " Off"), 0);
            }
            else if (text == "xoapaint")
            {
                IsPaint = !IsPaint;
            }
            else if (text == "kmap")
            {
                IsKhoaMap = !IsKhoaMap;
                GameScr.info1.addInfo(Val.LockMap + (IsKhoaMap ? " On" : " Off"), 0);
            }
            else if (text == "kkhu")
            {
                IsKhoaKhu = !IsKhoaKhu;
                GameScr.info1.addInfo(Val.LockZone + (IsKhoaKhu ? " On" : " Off"), 0);
            }
            //else if (text == "aenter")
            //{
            //    IsAEnter = !IsAEnter;
            //    new Thread(delegate ()
            //    {
            //        PickMobController.AutoEnter();
            //    })
            //    {
            //        IsBackground = true
            //    }.Start();

            //    GameScr.info1.addInfo($"Auto enter: " + (IsAEnter ? " On" : " Off"), 0);
            //}
            else if (text == "abdkb")
            {
                IsABDKB = !IsABDKB;
                GameScr.info1.addInfo(Val.AutoOpenBDKB + (IsABDKB ? " On" : " Off"), 0);
            }
            else if (text == "tusat")
            {
                Thread th = new Thread(new ThreadStart(PickMobAutoController.TuSat));
                th.IsBackground = true;
                th.Start();
                GameScr.info1.addInfo($"Đang chết ...", 0);
            }
            //else if (IsGetInfoChat<int>(text, "tuhs"))
            //{
            //    IsTuHS = !IsTuHS;
            //    PickMob.TuHSTime = GetInfoChat<int>(text, "tuhs");
            //    new Thread(new ThreadStart(PickMobController.TuHS)).Start();
            //    GameScr.info1.addInfo($"Auto tự hs trong {TuHSTime}: " + (IsTuHS ? " On" : " Off"), 0);
            //}
            else if (text == "tuhs")
            {
                IsTuHS = !IsTuHS;
                //new Thread(new ThreadStart(PickMobController.TuHS)).Start();
                GameScr.info1.addInfo($"Đang tự hs", 0);
            }
            else if (text == "movestop")
            {
                kvt = !kvt;
                GameScr.info1.addInfo(Val.LockPosition + (kvt ? " On" : " Off"), 0);
            }
            else if (text == "load8s")
            {
                GameScr.xskill = true;
                GameScr.info1.addInfo($"Load lại 8 skill", 0);
            }
            else if (text == "xindault")
            {
                isXinDauV = !isXinDauV;
                GameScr.info1.addInfo(Val.AutoXinDauLienTuc + (isXinDauV ? " On" : " Off"), 0);
                PickMobAutoController.xindauv();
            }
            else if (text == "attnl")
            {
                IsSkill3 = !IsSkill3;
                GameScr.info1.addInfo(Val.AutoTTNL + (IsSkill3 ? " On" : " Off"), 0);
            }
            else if (text == "ak")
            {
                IsAK = !IsAK;

                if (IsTanSat == true)
                {
                    IsTanSat = false;
                    Noti.ValStatus(Val.AutoAtackMob, IsTanSat);
                }

                Noti.ValStatus(Val.AK, IsAK);
            }
            else if (text == "asttnl")
            {
                if (PickMobAutoController.aHP == 0 && PickMobAutoController.aMP == 0)
                {
                    GameScr.info1.addInfo(Val.AutoUseTTNL + "Off", 0);
                }
                else
                {
                    PickMobAutoController.aHP = DEFAULT_HP_BUFF;
                    PickMobAutoController.aMP = DEFAULT_MP_BUFF;
                    GameScr.info1.addInfo($"Tự động sử dụng tái tạo năng lượng khi HP dưới {PickMobAutoController.aHP}%, MP dưới {PickMobAutoController.aMP}%", 0);
                }
            }
            else if (IsGetInfoChat<int>(text, "asttnl"))
            {
                PickMobAutoController.aHP = GetInfoChat<int>(text, "asttnl");
                PickMobAutoController.aMP = 0;
                GameScr.info1.addInfo($"Tự động sử dụng tái tạo năng lượng khi HP dưới {PickMobAutoController.aHP}%", 0);
            }
            else if (IsGetInfoChat<int>(text, "asttnl", 2))
            {
                int[] vs = GetInfoChat<int>(text, "asttnl", 2);
                PickMobAutoController.aHP = vs[0];
                PickMobAutoController.aMP = vs[1];
                GameScr.info1.addInfo($"Tự động sử dụng tái tạo năng lượng khi HP dưới {PickMobAutoController.aHP}%, MP dưới {PickMobAutoController.aMP}%", 0);
            }
            else if (text == "akhu")
            {
                PickMobAutoController.OldMap = TileMap.mapID;
                IsChenKhu = !IsChenKhu;
                GameScr.info1.addInfo(Val.AutoChenKhu + (IsChenKhu ? " On" : " Off"), 0);
            }
            else if (IsGetInfoChat<int>(text, "akhu"))
            {
                PickMobAutoController.OldMap = TileMap.mapID;
                IsChenKhu = !IsChenKhu;
                ChenNum = GetInfoChat<int>(text, "akhu");
                GameScr.info1.addInfo(Val.AutoChenKhu + ChenNum + "|" + (IsChenKhu ? " On" : " Off"), 0);
            }
            else if (text == "dokhu")
            {
                IsDoKhu = !IsDoKhu;
                new Thread(delegate () {
                    PickMobAutoController.DoKhu();
                })
                {
                    IsBackground = true
                }.Start();

                GameScr.info1.addInfo(Val.AutoDoKhu + DoNum + (IsDoKhu ? " On" : " Off"), 0);
            }
            //else if (IsGetInfoChat<int>(text, "dokhu"))
            //{
            //    IsDoKhu = !IsDoKhu;
            //    DoNum = GetInfoChat<int>(text, "dokhu");
            //    new Thread(delegate ()
            //    {
            //        PickMobController.DoKhu();
            //    })
            //    {
            //        IsBackground = true
            //    }.Start();
            //    GameScr.info1.addInfo($"Auto dò khu từ khu: " + DoNum + (IsDoKhu ? " On" : " Off"), 0);
            //}
            else if (text == "strom")
            {
                IsSanAnTrom = !IsSanAnTrom;
                GameScr.info1.addInfo(Val.AutoSanTrom + (IsSanAnTrom ? " On" : " Off"), 0);
            }
            else if (text == "bang")
            {
                Item[] arrItem = global::Char.myCharz().arrItemBody;
                if (arrItem[5] == null || arrItem[5].template.id != 630 || arrItem[5].template.id != 631 || arrItem[5].template.id != 632 || arrItem[5].template.id != 450)
                {
                    Service.gI().getItem(4, (sbyte)UseItem.useColdItem());
                }
                else
                {
                    Service.gI().getItem(5, (sbyte)UseItem.useColdItem());
                }

            }
            else if (text == "bangdt")
            {
                Item[] arrItem = global::Char.myCharz().arrItemBody;
                if (arrItem[5] == null || arrItem[5].template.id != 630 || arrItem[5].template.id != 631 || arrItem[5].template.id != 632 || arrItem[5].template.id != 450)
                {
                    Service.gI().getItem(6, (sbyte)UseItem.useColdItem());

                }
            }
            else if (IsGetInfoChat<int>(text, "tgf"))
            {
                Framerate = GetInfoChat<int>(text, "tgf");
                Application.targetFrameRate = Framerate;
                QualitySettings.vSyncCount = 0;
                GameScr.info1.addInfo("Framerate" + Framerate, 0);
            }
            else if (IsGetInfoChat<int>(text, "tgfdefault"))
            {
                Application.targetFrameRate = 60;
                QualitySettings.vSyncCount = 2;
                GameScr.info1.addInfo("Framerate" + Framerate, 0);
            }
            else if (text == "quit")
            {
                Application.Quit();
            }
            else if (text == "lsmap")
            {
                LoadSetting.IsCompleteAutoMap = false;
                LoadSetting.LOAD_SETTING_AUTO_MAP();
            }
            else if (text == "lspean")
            {
                LoadSetting.IsCompleteAutoPean = false;
                LoadSetting.LOAD_SETTING_AUTO_PEAN();
            }
            else if (text == "lsgb")
            {
                LoadSetting.IsCompleteAutoGoback = false;
                LoadSetting.LOAD_SETTING_AUTO_GOBACK();
            }
            else if (text == "lstrain")
            {
                LoadSetting.IsCompleteAutoTrain = false;
                LoadSetting.LOAD_SETTING_AUTO_TRAIN();
            }
            else if (text == "lspet")
            {
                LoadSetting.IsCompleteAutoPet = false;
                LoadSetting.LOAD_SETTING_AUTO_PET();
            }
            else if (text == "lsskill")
            {
                LoadSetting.IsCompleteAutoSkill = false;
                LoadSetting.LOAD_SETTING_AUTO_SKILL();
            }
            else if (text == "wpp")
            {
                IsWallPaper = !IsWallPaper;
            }
            else if (text == "d")
            {
                Char.myCharz().cy += 50;
                Service.gI().charMove();
            }
            else if (text == "u")
            {
                Char.myCharz().cy -= 50;
                Service.gI().charMove();
            }
            else if (text == "l")
            {
                Char.myCharz().cx -= 50;
                Service.gI().charMove();
            }
            else if (text == "r")
            {
                Char.myCharz().cx += 50;
                Service.gI().charMove();
            }
            else if (IsGetInfoChat<int>(text, "buysitem"))
            {
                PickMobAuto.Quantity = GetInfoChat<int>(text, "buysitem");
                MapBuysItem = TileMap.mapName;
                Panel.Instance().perform(200002, p);
            }
            else if (IsGetInfoChat<int>(text, "sellitem"))
            {
                PickMobAuto.Quantity = GetInfoChat<int>(text, "sellitem");
                MapSellItem = TileMap.mapName;
                Panel.Instance().perform(200003, p);
            }
            else if (IsGetInfoChat<int>(text, "l"))
            {
                PickMobAuto.Left = GetInfoChat<int>(text, "l");
                Char.myCharz().cx -= Left;
                Service.gI().charMove();
            }
            else if (IsGetInfoChat<int>(text, "r"))
            {
                PickMobAuto.Right = GetInfoChat<int>(text, "r");
                Char.myCharz().cx += Right;
                Service.gI().charMove();
            }
            else if (IsGetInfoChat<int>(text, "u"))
            {
                PickMobAuto.Up = GetInfoChat<int>(text, "u");
                Char.myCharz().cy -= Up;
                Service.gI().charMove();
            }
            else if (IsGetInfoChat<int>(text, "d"))
            {
                PickMobAuto.Down = GetInfoChat<int>(text, "d");
                Char.myCharz().cx += Down;
                Service.gI().charMove();
            }
            else if (IsGetInfoChat<int>(text, "uitem"))
            {
                uItem = GetInfoChat<int>(text, "uitem");
                UseItem.useItem(uItem);
            }
            else if (text == "mf")
            {
                isMobFollow = !isMobFollow;
                OnScreen.isStopMob = !OnScreen.isStopMob;
                GameScr.info1.addInfo("Mob follow: " + (isMobFollow ? "Bật" : "Tắt"), 0);
            }
            else if (text == "fzsk")
            {
                int num = PickMobAutoController.SKillFocus();
                GameScr.onScreenSkill[num].manaUse = 0;
                GameScr.onScreenSkill[num].coolDown = 0;
                GameScr.info1.addInfo("Đóng băng skill " + Char.myCharz().myskill.template.name, 0);
            }
            else if (text == "getgold")
            {
                Service.gI().openMenu(1);
                Service.gI().confirmMenu((short)1, (sbyte)0);
            }
            else if (IsGetInfoChat<int>(text, "adau"))
            {
                PickMobAuto.SecondBuffTheoSec = GetInfoChat<int>(text, "adau");
                IsAutoBuffDauTheoSec = !IsAutoBuffDauTheoSec;
                new Thread(delegate () {
                    UseItem.usePean();
                })
                {
                    IsBackground = true
                }.Start();
                GameScr.info1.addInfo((PickMobAuto.IsAutoBuffDauTheoSec ? "Bật" : "Tắt") + " buff đậu theo thời gian", 0);
            }
            else if (text == "chp")
            {
                cHP = !cHP;
                cMP = false;
                cSD = false;
                GameScr.info1.addInfo("Cộng tiềm năng liên tục vào hp: " + (PickMobAuto.cHP ? "Bật" : "Tắt"), 0);
            }
            else if (text == "cmp")
            {
                cMP = !cMP;
                cHP = false;
                cSD = false;
                GameScr.info1.addInfo("Cộng tiềm năng liên tục vào mp: " + (PickMobAuto.cMP ? "Bật" : "Tắt"), 0);
            }
            else if (text == "csd")
            {
                cSD = !cSD;
                cHP = false;
                cMP = false;
                GameScr.info1.addInfo("Cộng tiềm năng liên tục vào sd: " + (PickMobAuto.cSD ? "Bật" : "Tắt"), 0);
            }
            else if (text == "cgiap")
            {
                cSD = false;
                cHP = false;
                cMP = false;
                cGiap = !cGiap;
                GameScr.info1.addInfo("Cộng tiềm năng liên tục vào giáp: " + (PickMobAuto.cGiap ? "Bật" : "Tắt"), 0);
            }
            else if (text == "dd1")
            {
                DapDo1 = !DapDo1;
                GameScr.info1.addInfo("Đập đồ: " + (DapDo1 ? "Bật" : "Tắt"), 0);
            }
            else if (text == "dd2")
            {
                DapDo2 = !DapDo2;
                GameScr.info1.addInfo("Đập đồ: " + (DapDo2 ? "Bật" : "Tắt"), 0);
            }
            else if (text == "ruong")
            {
                Service.gI().openMenu(3);
            }
            else if (text == "vq2")
            {
                VongQuay2 = !VongQuay2;
                if (VongQuay2 == true)
                {
                    new Thread(delegate () {
                        VongQuaytd();
                    })
                    {
                        IsBackground = true
                    }.Start();
                }

            }
            else if (text == "vq1")
            {
                VongQuay1 = !VongQuay1;
                if (VongQuay1 == true)
                {
                    new Thread(delegate () {
                        VongQuaytd();
                    })
                    {
                        IsBackground = true
                    }.Start();
                }

            }
            else if (text == "vutrac")
            {
                IsRemoveItem = !IsRemoveItem;
            }
            else if (text == "epnr")
            {
                IsEpNR = !IsEpNR;
                GameScr.info1.addInfo("Ép ngọc rồng: " + (IsEpNR ? "Bật" : "Tắt"), 0);
            }
            else if (text == "adaut")
            {
                ADAU = !ADAU;
            }
            else if (text == "testt")
            {
                
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion
        public static object p;
        public static PickMobAuto gI()
        {
            return (instance != null) ? instance : (instance = new PickMobAuto());
        }

        public static void FindFilesName()
        {
            foreach (var file in files)
            {
                fileName = file;
            }
            wallPaper = Image.createImage(File.ReadAllBytes("WPP/" + fileName));
        }
        public static bool DapDo2 = false;
        public static bool IsEpNR = false;
        public static bool IsDapDa = false;
        public static bool VongQuay2 = false;
        public static bool VongQuay1 = false;
        public static bool isBanRac = false;
        public static bool DapDo1 = false;
        public static bool IsRemoveItem = false;
        public static bool IsBanMDV = false;
        public static bool IsBanNR7s = false;
        // cập nhật khu
        public static bool HotKeys()
        {
            switch (GameCanvas.keyAsciiPress)
            {
                case 't':
                    Chat("ts");
                    break;
                case 'n':
                    Chat("anhat");
                    break;
                case 'a':
                    Chat("add");
                    break;
                case 'b':
                    Chat("abf");
                    break;
                case 'v':
                    Chat("trade");
                    break;
                case 'f':
                    Chat("ht");
                    break;
                case 'm':
                    Chat("zone");
                    break;
                case 'w':
                    Chat("focus");
                    break;
                case 'e':
                    Chat("focusboss");
                    break;
                case 'h':
                    vectorUseEqui();
                    break;
                case '9':
                    Chat("tusat");
                    break;
                case 's':
                    Chat("ak");
                    break;
                case 'o':
                    if (VongQuay2 == true || DapDo2 == true || DapDo1 == true || VongQuay1 == true || IsBanMDV == true || IsBanNR7s == true)
                    {
                        VongQuay2 = false;
                        VongQuay1 = false;
                        DapDo2 = false;
                        DapDo1 = false;
                        IsBanMDV = false;
                        IsBanNR7s = false;
                        stepdv1 = false;
                        stepdv2 = false;
                        stepdv1 = false;
                        stepdv2 = false;
                        isSell = false;
                        GameScr.info1.addInfo("Tắt auto", 0);
                        break;
                    }
                    ADAU = false;
                    break;
                case 'd':
                    MyVector myVector = new MyVector();
                    for (int i = 0; i < ShowMenu.CharNameList.Count; i++)
                    {
                        myVector.addElement(new Command("Đến\n" + ShowMenu.CharNameList[i], 555554, ShowMenu.CharIDList[i]));
                    }
                    myVector.addElement(new Command("Thêm\n", 555551));
                    myVector.addElement(new Command("Xoá\n", 555553));
                    GameCanvas.menu.startAt(myVector, 3);
                    break;
                default:
                    return false;
            }
            return true;
        }
        public static ChatTextField chatTField;
        static bool step1, step2, ADAU;
        static bool stepdv1 = false, stepdv2 = false;
        static bool step7s1 = false, step7s2 = false;
        public static void BanMDV()
        {
            if (IsBanMDV || IsBanNR7s)
            {
                if (stepdv1 == false)
                {
                    Panel.Instance().doFirePet();
                    GameCanvas.panel.tabName[21] = mResources.petMainTab;
                    GameCanvas.panel.setTypePetMain();
                    GameCanvas.panel.show();
                    isSell = true;
                    stepdv1 = true;
                }
                SoundMn.gI().buttonClose();
                if (Input.GetKey("o"))
                {
                    GameScr.info1.addInfo("Auto đã tắt", 0);
                    IsBanMDV = false;
                    stepdv1 = false;
                    stepdv2 = false;
                }
                isSell = true;
                int idItem = 225;
                if (IsBanNR7s)
                {
                    idItem = 20;
                }
                if (stepdv1 == true && stepdv2 == false)
                {
                    for (int i = global::Char.myCharz().arrItemBag.Length - 2; i >= 0; i--)
                    {
                        Item item = global::Char.myCharz().arrItemBag[i];
                        Item item2 = global::Char.myCharz().arrItemBag[i + 1];
                        
                        if (item != null && !itemKHNitemStar(item) && item.template.id == idItem &&
                            item2 != null && !itemKHNitemStar(item2))
                        {

                            Service.gI().saleItem(1, 1, (short)((sbyte)i));
                            Wait(500);
                            break;
                        }
                    }
                }
                    
            }
            
        }
        public static bool itemKHNitemStar(Item item)
        {
            bool result = false;
            for (int i = 0; i < item.itemOption.Length; i++)
            {
                if (item.itemOption[i].optionTemplate.name.StartsWith("$"))
                {
                    return true;
                }
                if (item.itemOption[i].optionTemplate.id == 107)
                {
                    return true;
                }
                if (item.itemOption[i].optionTemplate.name.StartsWith("#"))
                {
                    return true;
                }
                //if (item.itemOption[i].optionTemplate.type < 4)
                //{
                //    return true;
                //}
            }
            return result;
        }
        public static void GameScrUpdate(MyVector vmob)
        {
            if (IsWaiting())
            {
                return;
            }
            PickMobAuto.mobfollow(vmob);
            PickMobAuto.UpdateNhatDoDT();
            PickMobAuto.AnDuiGa();
            PickMobAuto.UpdateAutoThudau();
            PickMobAuto.UpdateAutoThudauXin();
            PickMobAuto.updateZone();
            PickMobAuto.UpdateAutoBDKB();
            PickMobAuto.updateDoanhTrai();
            PickMobAuto.updateSkill3();
            PickMobAuto.updateKVT();
            //updateAutoEnter();
            PickMobAuto.AutoFlag();
            PickMobAuto.AK();
            PickMobAuto.AS3();
            PickMobAuto.TuHS();
            PickMobAuto.autoChenKhu();
            PickMobAutoController.SanTrom();
            PickMobAutoController.autonhathopqua();
            PickMobAutoController.ChenKhuFull(PickMobAutoController.AutoZone, PickMobAutoController.OldMap);
            PickMobAuto.VuotDiaHinh();
            MacSet1();
            MacSet2();
            PickMobAutoController.Revive();
            dapDo();
            charAutoFocus();
            PickMobAutoController.sellThoiVang();
            if (ADAU)
            {
                PickMobAutoController.ADAU();
            }
            VKKRAuto();
            BanMDV();
            if (IsRemoveItem)
            {
                for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                {
                    if (global::Char.myCharz().arrItemBag[i].template.id != 381 &&
                      global::Char.myCharz().arrItemBag[i].template.id != 382 &&
                      global::Char.myCharz().arrItemBag[i].template.id != 383 &&
                      global::Char.myCharz().arrItemBag[i].template.id != 384)
                    {
                        Service.gI().useItem(2, 1, (sbyte)i, -1);
                        GameScr.info1.addInfo("Vứt: " + Char.myCharz().arrItemBag[i].template.name, 0);
                    }
                }
            }

            if (cHP)
            {
                Service.gI().upPotential(0, 1);
                Service.gI().upPotential(0, 10);
                Service.gI().upPotential(0, 100);
                PickMobAutoController.Wait(50);
            }
            if (cMP)
            {
                Service.gI().upPotential(1, 1);
                Service.gI().upPotential(1, 10);
                Service.gI().upPotential(1, 100);
                PickMobAutoController.Wait(50);
            }
            if (cSD)
            {
                Service.gI().upPotential(2, 1);
                Service.gI().upPotential(2, 10);
                Service.gI().upPotential(2, 100);
                PickMobAutoController.Wait(50);
            }
            if (cGiap)
            {
                Service.gI().upPotential(3, 1);
                Service.gI().upPotential(3, 10);
                Service.gI().upPotential(3, 100);
                PickMobAutoController.Wait(50);
            }
        }
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
        public static bool cHP, cMP, cSD, cGiap;
        static bool step12 = false, step22 = false, step3 = false, step4 = false;
        private static bool IsWait;
        private static long TimeStartWait;
        private static long TimeWait;

        public static void VongQuaytd()
        {
            if (TileMap.mapID != 45)
            {
                XmapController.StartRunToMapId(45);
            }
            while (TileMap.mapID != 45)
            {
                if (TileMap.mapID == 45)
                {
                    break;
                }
            };
            step12 = false;
            step22 = false;
            step3 = false;
            step4 = false;
            Service.gI().openMenu(19);
            Service.gI().confirmMenu(19, 4);
            if (VongQuay1)
            {
                Service.gI().confirmMenu(19, 1);
            }
            else
            {
                Service.gI().confirmMenu(19, 2);
            }
            Thread.Sleep(2000);
            while (VongQuay2 || VongQuay1)
            {
                if (Input.GetKey("o"))
                {
                    GameScr.info1.addInfo("Auto đã tắt", 0);
                    CrackBallScr.gI().doClickSkill(1);
                    CrackBallScr.gI().doClickSkill(1);
                    VongQuay2 = false;
                    VongQuay1 = false;
                    break;
                }
                for (int i = 0; i < 7; i++)
                {
                    CrackBallScr.gI().doClickBall(i);
                }
                Thread.Sleep(1000);
                CrackBallScr.gI().doClickSkill(0);
                CrackBallScr.gI().doClickSkill(0);
                Service.gI().openMenu(19);
                Service.gI().confirmMenu(19, 4);
                Service.gI().confirmMenu(19, 3);
                Thread.Sleep(1000);
                if (global::Char.myCharz().arrItemShop[0].Length >= 43)
                {
                    for (int i = global::Char.myCharz().arrItemShop[0].Length - 1; i >= 0; i--)
                    {
                        Service.gI().buyItem(2, i, 0);
                        //Thread.Sleep(100);
                    }
                }
            }

        }
        public static void dapDo()
        {
            if (DapDo2 == true || DapDo1 == true)
            {
                int menuSize = 4;
                if (TileMap.mapID != 5)
                {
                    XmapController.StartRunToMapId(5);
                    return;
                }
                //bool isNhanVang = false;
                bool flag = !GameCanvas.menu.showMenu && !GameCanvas.panel.isShow;
                if (flag)
                {
                    Service.gI().openMenu(21);
                    step1 = true;
                    return;
                }
                if (DapDo1 == true)
                {
                    if (step1 && GameCanvas.menu.menuItems.size() == menuSize)
                    {
                        Service.gI().confirmMenu(21, 0);
                        step1 = false;
                        Thread.Sleep(100);
                    }
                }
                else if (DapDo2 == true)
                {
                    if (step1 && GameCanvas.menu.menuItems.size() == menuSize)
                    {
                        Service.gI().confirmMenu(21, 1);
                        step1 = false;
                    }
                }
                SoundMn.gI().buttonClose();
                if (DapDo1 == true)
                {
                    if (GameCanvas.panel.vItemCombine.size() != 2)
                        return;
                }
                for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
                {
                    Item item = (Item)GameCanvas.panel.vItemCombine.elementAt(i);
                    if (item != null)
                    {

                        MyVector myVector = GameCanvas.panel.vItemCombine;
                        Service.gI().combine(1, myVector);
                        GameCanvas.gI().keyPressedz(-5);
                        if (Input.GetKey("o"))
                        {
                            GameScr.info1.addInfo("Auto đã tắt", 0);
                            DapDo1 = false;
                            DapDo2 = false;
                            break;
                        }
                    }
                }
                if (ChangeServer.nroS)
                {
                    if (Char.myCharz().xu < 200000000 && DapDo2)
                    {
                        PickMobAuto.isBanVang = true;
                        DapDo2 = false;
                        //isNhanVang = true;
                    }
                    //if (isNhanVang)
                    //{

                    //    step1 = true;
                    //    if (XmapController.getX(0) > 0 && XmapController.getY(0) > 0 && XmapData.CanNextMap())
                    //    {
                    //        XmapController.MoveMyChar(XmapController.getX(0), XmapController.getY(0));
                    //    }
                    //}
                }
                else if (ChangeServer.blue)
                {
                    if (Char.myCharz().xu < 200000000 && DapDo2)
                    {
                        PickMobAuto.isBanVang = true;
                    }
                }
            }
        }
        public static void VKKRAuto()
        {
            if (IsEpNR || IsDapDa)
            {
                bool flag = !GameCanvas.menu.showMenu && !GameCanvas.panel.isShow;
                if (flag)
                {
                    Service.gI().openMenu(21);
                    step1 = true;
                    return;
                }
                if(step1 == true)
                {
                    if (IsEpNR)
                    {
                        Service.gI().confirmMenu(21, 3);
                        Service.gI().confirmMenu(21, 1);
                    }
                        
                    if (IsDapDa)
                        Service.gI().confirmMenu(21, 2);

                }
                SoundMn.gI().buttonClose();
                if (Input.GetKey("o"))
                {
                    GameScr.info1.addInfo("Auto đã tắt", 0);
                    IsEpNR = false;
                    IsDapDa = false;
                }
                if (IsDapDa == true)
                {
                    if (GameCanvas.panel.vItemCombine.size() != 2)
                        return;
                }
                
                for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
                {
                    Item item = (Item)GameCanvas.panel.vItemCombine.elementAt(i);
                    if (item != null)
                    {

                        MyVector myVector = GameCanvas.panel.vItemCombine;
                        Service.gI().combine(1, myVector);
                        GameCanvas.gI().keyPressedz(-5);
                        if (Input.GetKey("o"))
                        {
                            GameScr.info1.addInfo("Auto đã tắt", 0);
                            IsEpNR = false;
                            IsDapDa = false;
                            break;
                        }
                    }
                }
            }
        }

        public static void MacSet1()
        {
            if (MacSet1t)
            {
                PickMobAutoController.MacSet1();
            }
        }
        public static void MacSet2()
        {
            if (MacSet2t)
            {
                PickMobAutoController.MacSet2();
            }
        }
        public static void mobfollow(MyVector vMob)
        {
            PickMobAutoController.MobFollow(vMob);
        }
        public static void buyItem(Item item)
        {
            if (PickMobAuto.Quantity > 0 && PickMobAuto.BuysDelay > 0)
            {
                for (int i = 1; i <= PickMobAuto.Quantity; i++)
                {

                    Service.gI().buyItem(1, item.template.id, 0);

                    Thread.Sleep(TimeSpan.FromSeconds(PickMobAuto.BuysDelay));
                    if (TileMap.mapName != MapBuysItem) break;
                }
            }
        }
        public static void useItem(int selected)
        {
            if (PickMobAuto.Quantity_USE > 0 && PickMobAuto.UseDelay > 0)
            {
                for (int i = 1; i <= PickMobAuto.Quantity_USE; i++)
                {
                    Item item9 = (Item)p;
                    bool flag = selected < Char.myCharz().arrItemBody.Length;
                    sbyte b = 0;
                    if (!flag)
                    {
                        b = (sbyte)(selected - Char.myCharz().arrItemBody.Length);
                    }
                    Service.gI().useItem(0, (sbyte)((!flag) ? 1 : 0), (sbyte)((!flag) ? b : selected), -1);
                    if (item9.template.id == 193 || item9.template.id == 194)
                    {
                        GameCanvas.panel.hide();
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(PickMobAuto.BuysDelay));
                }

            }
        }
        public static void salesItem(Item item, int selected)
        {
            return;
            if (PickMobAuto.Quantity > item.quantity)
            {
                GameScr.info1.addInfo("Nhập số lượng lớn hơn số lượng có trong hành trang có thể bị bán sai đồ", 0);
                return;
            }
            if (PickMobAuto.Quantity > 0 && PickMobAuto.BuysDelay > 0)
            {
                for (int i = 1; i <= PickMobAuto.Quantity; i++)
                {
                    Item item9 = (Item)PickMobAuto.p;
                    for (int j = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
                    {
                        if (global::Char.myCharz().arrItemBag[j].template.id == item9.template.id)
                        {
                            Service.gI().saleItem(0, 1, (short)((sbyte)i));
                            break;
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(PickMobAuto.BuysDelay));
                }
            }
        }
        public static void autoChenKhu()
        {
            if (PickMobAuto.IsChenKhu && (GameCanvas.gameTick % (10 * (int)Time.timeScale)) == 0)
            {
                PickMobAutoController.ChenKhu(ChenNum, PickMobAutoController.OldMap);
            }

        }
        public static void autodt()
        {
            PickMobAutoController.autodt();
        }

        // auto Off cờ
        public static void AutoFlag()
        {
            PickMobAutoController.AutoFlag();
            Wait(100);
        }
        // focuss boss
        public static void AutoFocusBoss()
        {
            PickMobAutoController.AutoFocusBoss();
        }
        // auto chat
        public static void AutoChat()
        {
            int i = 0;
            while (IsAutoChat)
            {

                string text = File.ReadAllText("Data\\chat.ini");
                if (text == null)
                {
                    GameScr.info1.addInfo("Không có nội dụng rồi chat kiểu gì", 0);
                    return;
                }
                Service.gI().chat(i + "vt: " + text);
                Thread.Sleep(2000);
                i++;
            }
        }
        // auto chat tg
        public static void AutoChatTG()
        {
            int dem = 0;
            while (IsAutoChatTG)
            {
                string text = File.ReadAllText("Data\\chattg.ini");
                if (text == null)
                {
                    return;
                }
                Service.gI().chatGlobal(dem + "vt: " + text);
                Thread.Sleep(2000);
                dem++;
            }
        }
        //focus char
        public static void charAutoFocus()
        {
            if (isCharAutoFocus)
                PickMobAutoController.AutoFocus();
        }
        // tốc độ chạy
        public static void startOkDlg(string info)
        {
            PickMobAutoController.startOKDlg(info);
        }
        public static void autoLogin()
        {
            if (PickMobAuto.IsAutoLogin)
            {

                //new Thread(delegate ()
                //{
                PickMobAutoController.autoLogin();
                //})
                //{
                //    IsBackground = true
                //}.Start();

            }

        }
        public static void Update()
        {
            PickMobAutoController.Update();
        }
        // auto né boss
        public static void AutoNeBoss()
        {
            PickMobAutoController.AutoNeBoss();
        }
        // sao sư phụ không đánh
        public static void findMobForPet(string info)
        {
            if (info.ToLower().Contains("sao sư phụ không đánh") && IsAttackForPet == true)
            {
                PickMobAutoController.findMobForPet();
            }
            if (info.ToLower().Contains("sư phụ"))
            {
                GameScr.gI().doUseHP();
            }
        }
        public static void Info(string text)
        {
            if (text.Contains("không thể nhặt"))
            {
                IsNhatDoUpDT = false;
            }
            //if (text.ToLower().Contains("bán thành công"))
            //{
            //    var tsw = new StreamWriter(@"DaBan.txt", true);
            //    tsw.WriteLine("("+DateTime.Now.ToString("HH:mm:ss tt")+")"+" "+text);
            //    tsw.Close();
            //}
            //if (text.ToLower().Contains("nhặt"))
            //{
            //    var tsw = new StreamWriter(@"Nhat.txt", true);
            //    tsw.WriteLine("("+DateTime.Now.ToString("HH:mm:ss tt")+")"+" "+text);
            //    tsw.Close();
            //}

            if (text.ToLower().Contains("cần 1 trang bị có lỗ"))
            {
                DapDo1 = false;
            }
        }
        //vector mặc đồ
        public static void vectorUseEqui()
        {
            PickMobAutoController.vectorUseEqui();
        }

        public static void perform(int idAction)
        {
            PickMobAutoController.actionPerForm(idAction);
        }

        #region gamescr update
        //public static void updateAutoEnter()
        //{
        //    if(isEnter && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
        //    {
        //        PickMobController.AutoEnter();
        //    }

        //}
        public static void AS3()
        {
            if (GameCanvas.gameTick % (10 * (int)Time.timeScale) == 0)
            {
                PickMobAutoController.ASkill3HPMP();
            }

        }
        public static void AK()
        {
            if (IsAK && GameCanvas.gameTick % 4 == 0)
            {
                PickMobAutoController.AK();
            }

        }
        public static void updateDoanhTrai()
        {
            if (IsDT && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
            {
                autodt();
                Wait(20);
            }
        }
        //tu hs
        public static void TuHS()
        {
            PickMobAutoController.TuHS();
        }
        //skill 3
        public static void updateSkill3()
        {
            PickMobAutoController.AutoSkill3();
        }
        //kvt
        public static void updateKVT()
        {
            PickMobAutoController.KhoaViTri();
        }
        //xindau
        public static void updateXinDau()
        {
            PickMobAutoController.xinDau();
        }
        // update khu
        public static void updateZone()
        {
            if (ukhu && GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0)
            {
                PickMobAutoController.updateZone();
                Wait(100);
            }
        }
        // update nhat do de tu
        public static void UpdateNhatDoDT()
        {
            if (IsNhatDoUpDT && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
            {
                PickMobAutoController.NhatDoUpDT();
            }

        }
        public static void VuotDiaHinh()
        {
            if (IsVuotDiaHinh)
            {
                PickMobAutoController.VuotDiaHinh();
            }
        }
        // update thu dau
        public static void UpdateAutoThudau()
        {
            if (IsAutoThuDau || PickMobAutoController.IsCompleteGetBean && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
            {
                PickMobAutoController.thuDau();
            }
        }
        public static void UpdateAutoThudauXin()
        {
            if (IsAutoThuDauXin && GameCanvas.gameTick % (20 * (int)Time.timeScale) == 0)
            {
                PickMobAutoController.thuDauXin();
            }
        }
        public static void TeleVip(int id)
        {
            PickMobAutoController.TeleVip(id);
        }
        // auto ăn đùi gà
        public static void AnDuiGa()
        {
            PickMobAutoController.autoAnDuiGa();
        }
        #endregion
        public static void MobStartDie(object obj)
        {
            Mob mob = (Mob)obj;
            if (mob.status != 1 && mob.status != 0)
            {
                mob.timeLastDie = mSystem.currentTimeMillis();
                mob.countDie++;
                if (mob.countDie > 10)
                    mob.countDie = 0;
            }
        }

        public static void UpdateCountDieMob(Mob mob)
        {
            if (mob.levelBoss != 0)
                mob.countDie = 0;
        }
        public static void UpdateAutoBDKB()
        {
            if (PickMobAuto.IsABDKB)
            {
                PickMobAutoController.AutoBDKB();
                Wait(100);
            }
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