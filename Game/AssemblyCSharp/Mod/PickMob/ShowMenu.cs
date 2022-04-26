using System.Collections.Generic;
using System.IO;
using AssemblyCSharp.Mod.SaveSetting;
using AssemblyCSharp.Mod.Xmap;

namespace AssemblyCSharp.Mod.PickMob
{
    public class ShowMenu
    {
        static List<Menu> menus;
        static int count = 1;
        static MyVector myVector = new();
        static string menutag = "\n[menu]";
        static string cansavetag = " [+] ";
        private static ShowMenu _Instance;
        public static List<int> CharIDList = new List<int>();
        public static List<string> CharNameList = new List<string>();
        public static ShowMenu Instance()
        {
            if (_Instance == null)
                _Instance = new();
            return _Instance;
        }
        public void ActionPerform(int idAction,object p)
        {
            Char @char = global::Char.myCharz().charFocus;
            switch (idAction)
            {
                case 555551:
                    foreach (var item in CharIDList)
                    {
                        if (item == @char.charID)
                        {
                            GameScr.info1.addInfo("Đã thêm người này", 0);
                            return;
                        }
                    }
                    CharIDList.Add(@char.charID);
                    CharNameList.Add(@char.cName.Split(' ')[1]);
                    GameScr.info1.addInfo("Đã thêm " + @char.cName,0);
                    break;
                case 555552:
                    CharIDList.RemoveAt((int)p);
                    CharNameList.RemoveAt((int)p);
                    GameScr.info1.addInfo("Đã xoá " + @char.cName,0);
                    break;
                case 555553:
                    MyVector myVector = new MyVector();
                    for(int i = 0; i < CharNameList.Count; i++)
                    {
                        myVector.addElement(new Command("Xoá\n" + CharNameList[i], 555552, i));
                    }
                    GameCanvas.menu.startAt(myVector, 3);
                    break;
                case 555554:
                    PickMob.TeleVip((int)p);
                    break;
                case 121001:
                    Pk9rXmap.Chat("xmp");
                    break;
                case 121002:
                    PickMob.Chat("anhat");
                    break;
                case 121003:
                    PickMob.Chat("ts");
                    break;
                case 121004:
                    PickMob.Chat("atc");
                    break;
                    ///////////// UP DE TU //////////////
                case 121005:
                    showupdetumenu();
                    break;
                case 121006:
                    Pk9rXmap.Chat("gb");
                    break;
                case 121007:
                    Pk9rXmap.Chat("goback");
                    break;
                case 131002:
                    PickMob.Chat("ndt");
                    break;
                case 131003:
                    PickMob.Chat("aflag8");
                    break;
                case 131004:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_ASTTNL_When_HP_MP, 
                        TextFieldChatMod.Instance().strChat_ASTTNL_When_HP_MP, 
                        GameScr.gI());
                    //ChatTextField.gI().tfChat.name = TextFieldChatMod.Instance().Caption_ASTTNL_When_HP_MP;
                    //ChatTextField.gI().strChat = TextFieldChatMod.Instance().strChat_ASTTNL_When_HP_MP;
                    //ChatTextField.gI().to = string.Empty;
                    //ChatTextField.gI().startChat(GameScr.gI(), string.Empty);
                    //PickMob.Chat("asttnl");
                    break;
                case 121008:
                    OnScreen.Chat("gdl");
                    break;
                case 121009:
                    OnScreen.Chat("gdlv");
                    break;
                    /////////////// AUTO MAP /////////////////
                case 141003:
                    PickMob.Chat("aduiga");
                    break;
                case 141004:
                    PickMob.Chat("athudau");
                    break;
                case 141006:
                    Pk9rXmap.Chat("csb");
                    break;
                case 141007:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_Map
                        , TextFieldChatMod.Instance().strChat_Input_ID_Map, GameScr.gI());
                    break;
                case 141001:
                    showautomapmenu();
                    break;
                case 151002:
                    showautoskillmenu();
                    break;
                case 151003:
                    PickMob.Chat("ak");
                    break;
                case 151004:
                    PickMob.IsAttackForPet = !PickMob.IsAttackForPet;
                    break;
                    /////////// PEAN //////////////
                case 161000:
                    showpeanmenu();
                    break;
                case 161001:
                    PickMob.Chat("xindau");
                    break;
                case 161002:
                    PickMob.Chat("chodau");
                    break;
                case 161003:
                    PickMob.Chat("thudau");
                    break;
                case 161004:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Use_Pean_When_HP_MP
                        , TextFieldChatMod.Instance().strChat_Use_Pean_When_HP_MP, GameScr.gI());
                    break;
                case 171000:
                    showtrainmenu();
                    break;
                case 171001:
                    PickMob.Chat("nsq");
                    break;
                case 171002:
                    PickMob.Chat("clrm");
                    break;
                case 171003:
                    PickMob.Chat("vdh");
                    break;
                case 171004:
                    PickMob.Chat("addt");
                    break;
                case 181000:
                    showmoremenu();
                    break;
                case 181001:
                    OnScreen.Chat("sb");
                    break;
                case 181002:
                    OnScreen.Chat("pinfo"); 
                    break;
                case 181003:
                    PickMob.Chat("ukhu"); 
                    break;
                case 181004:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_Auto_Vao_Khu
                        , TextFieldChatMod.Instance().strChat_Input_Auto_Vao_Khu, GameScr.gI());
                    break;
                case 181005:
                    showhaoquangmenu();
                    break;
                case 1810051:
                    Char.myCharz().clevel = 12;
                    break;
                case 1810052:
                    Char.myCharz().clevel = 13;
                    break;
                case 1810053:
                    Char.myCharz().clevel = 14;
                    break;
                case 1810054:
                    Char.myCharz().clevel = 15;
                    break;
                //case 1810055:
                //    Char.myCharz().clevel = 16;
                //    break;
                case 0000001:
                    var AutoMapSettingWriter = new StreamWriter("Data\\AutoMapSetting.ini");
                    AutoMapSettingWriter.Write(Setttings.AnDuiGa +":"+PickMob.IsAnDuiGa+"|");
                    AutoMapSettingWriter.Write(Setttings.UseCapsuleNormal+ ":" + Pk9rXmap.IsUseCapsuleNormal + "|");
                    AutoMapSettingWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Map", 0);
                    break;
                case 00000011:
                    LoadSetting.LOAD_SETTING_AUTO_PET();
                    break;
                case 0000002:
                    var AutoSkillWriter = new StreamWriter("Data\\AutoSkillSetting.ini");
                    AutoSkillWriter.Write(Setttings.AttackForPet + ":" + PickMob.IsAttackForPet + "|");
                    AutoSkillWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Skill", 0);
                    break;
                case 00000012:
                    LoadSetting.LOAD_SETTING_AUTO_MAP();
                    break;
                case 0000003:
                    var AutoPeanWriter = new StreamWriter("Data\\AutoPeanSetting.ini");
                    AutoPeanWriter.Write(Setttings.XinDau + ":" + PickMob.IsXinDau + "|");
                    AutoPeanWriter.Write(Setttings.ChoDau + ":" + PickMob.IsChoDau + "|");
                    AutoPeanWriter.Write(Setttings.ThuDau + ":" + PickMob.IsAutoThuDau + "|");
                    AutoPeanWriter.Write(Setttings.UseHPPotionWhen + ":" + PickMob.HpBuff + "|");
                    AutoPeanWriter.Write(Setttings.UseMPPotionWhen + ":" + PickMob.MpBuff + "|");
                    AutoPeanWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Pean", 0);
                    break;
                case 00000013:
                    LoadSetting.LOAD_SETTING_AUTO_SKILL();
                    break;
                case 0000004:
                    var AutoTrainWriter = new StreamWriter("Data\\AutoTrainSetting.ini");
                    AutoTrainWriter.Write(Setttings.NeSieuQuai + ":" + PickMob.IsNeSieuQuai + "|");
                    var AutoGobackWriter = new StreamWriter("Data\\AutoGobackSetting.ini");
                    AutoGobackWriter.Write(Setttings.Goback + ":" + Pk9rXmap.isGoBackbt + "|");
                    AutoGobackWriter.Close();
                    AutoTrainWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Train", 0);
                    break;
                case 00000014:
                    LoadSetting.LOAD_SETTING_AUTO_PEAN();
                    break;
                case 0000005:
                    var AutoUpPetWriter = new StreamWriter("Data\\AutoUpPetSetting.ini");
                    AutoUpPetWriter.Write(Setttings.PickPetDropItem + ":" + PickMob.IsNhatDoUpDT + "|");
                    AutoUpPetWriter.Write(Setttings.AutoFlag_8 + ":" + PickMob.IsAutoFlag + "|");
                    AutoUpPetWriter.Write(Setttings.UseTTNLWhenHP + ":" + PickMobController.aHP + "|");
                    AutoUpPetWriter.Write(Setttings.UseTTNLWhenMP + ":" + PickMobController.aMP + "|");
                    AutoUpPetWriter.Write(Setttings.XoaDoHoa + ":" + OnScreen.IsXoaMap + "|");
                    AutoUpPetWriter.Write(Setttings.XoaDoHoaGiamCPU + ":" + OnScreen.IsXoaMapV + "|");
                    AutoUpPetWriter.Write(Setttings.XNhat + ":" + PickMobController.XNhat + "|");
                    AutoUpPetWriter.Write(Setttings.YNhat + ":" + PickMobController.YNhat + "|");
                    AutoGobackWriter = new StreamWriter("Data\\AutoGobackSetting.ini");
                    AutoGobackWriter.Write(Setttings.Goback + ":" + Pk9rXmap.isGoBackbt + "|");
                    AutoGobackWriter.Write(Setttings.GobackPosition + ":" + Pk9rXmap.isGoBack + "|");
                    AutoGobackWriter.Write(Setttings.MapID + ":" + XmapController.MapID + "|");
                    AutoGobackWriter.Write(Setttings.ZoneID + ":" + XmapController.ZoneID + "|");
                    AutoGobackWriter.Write(Setttings.cx + ":" + XmapController.cx + "|");
                    AutoGobackWriter.Write(Setttings.cy + ":" + XmapController.cy + "|");
                    AutoGobackWriter.Close();
                    AutoUpPetWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Up Pet", 0);
                    break;
                case 00000015:
                    LoadSetting.LOAD_SETTING_AUTO_TRAIN();
                    break;
                case 190001:
                    PickMob.Chat("add");
                    break;
                case 190002:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_ITEM_TO_PICK_LIST
                        , TextFieldChatMod.Instance().strChat_Input_ID_ITEM_TO_PICK_LIST, GameScr.gI());
                    break;
                case 190003:
                    PickMob.Chat("addt");
                    break;
                case 190004:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_ITEM_TO_PICK_BLOCK_LIST
                        , TextFieldChatMod.Instance().strChat_Input_ID_ITEM_TO_PICK_BLOCK_LIST, GameScr.gI());
                    break;
                case 190005:
                    PickMob.Chat("blocki");
                    break;
                case 190006:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_SLN
                        , TextFieldChatMod.Instance().strChat_Input_SLN, GameScr.gI());
                    break;
                case 190007:
                    PickMob.Chat("clri");
                    break;
                case 190000:
                    showpicksmenu();
                    break;
                case 200000:
                    showchatmenu();
                    break;
                case 200001:
                    PickMob.Chat("atgtg");
                    break;
            }

        }
        public void CreateStartChat(string tfChatName, string strChat, IChatable parentScreen)
        {
            ChatTextField.gI().tfChat.name = tfChatName;
            ChatTextField.gI().strChat = strChat;
            ChatTextField.gI().to = string.Empty;
            ChatTextField.gI().isShow = true;
            ChatTextField.gI().tfChat.isFocus = true;
            ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);

            ChatTextField.gI().startChat(parentScreen, string.Empty);
        }
        public void MenuMain()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Auto Map"+menutag,IdAction = 141001},
                new Menu(){ Name =  "Auto Skill" + menutag,IdAction = 151002},
                new Menu(){ Name =  "Auto Pean" + menutag,IdAction = 161000},
                new Menu(){ Name =  "Auto Pick" + menutag,IdAction = 190000},
                new Menu(){ Name =  "Auto Train"+ menutag,IdAction = 171000},
                new Menu(){ Name =  "Auto Chat"+ menutag,IdAction = 200000},
                new Menu(){ Name =  "Up Đệ"+menutag,IdAction = 121005},               
                new Menu(){ Name =  "More"+menutag,IdAction = 181000},               
            };
            
        }
        public void MenuUpDT()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {               
                new Menu(){ Name =  "Nhặt Đồ\n Đệ Tử"+ "\n["+(PickMob.IsNhatDoUpDT ? "On]" : "Off]")+cansavetag,IdAction = 131002},
                new Menu(){ Name =  "Auto cờ đen"+ "\n["+(PickMob.IsAutoFlag ? "On]" : "Off]")+cansavetag,IdAction = 131003},
                new Menu(){ Name = $"TTNL\n{PickMobController.aHP}% HP\n{PickMobController.aMP}% MP"+cansavetag,IdAction = 131004},
                new Menu(){ Name =  "Goback"+ "\n["+(Pk9rXmap.isGoBackbt ? "On]" : "Off]")+cansavetag,IdAction = 121006},
                new Menu(){ Name =  "Goback\n Toạ Độ"+ "\n["+(Pk9rXmap.isGoBack ? "On]" : "Off]")+cansavetag,IdAction = 121007},
                new Menu(){ Name =  "Xoá Đồ Hoạ"+ "\n["+(OnScreen.IsXoaMap ? "On]" : "Off]")+cansavetag,IdAction = 121008},
                new Menu(){ Name =  "Xoá Đồ Hoạ\n Và Giảm CPU"+ "\n["+(OnScreen.IsXoaMapV ? "On]" : "Off]")+cansavetag,IdAction = 121009},
                new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000005},
                new Menu(){ Name =  "Load Setting",IdAction = 00000011},
            };
            
        }
        public void MenuChat()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Auto Chat"+ "\n["+(PickMob.IsAutoChat ? "On]" : "Off]"),IdAction = 121004},
                new Menu(){ Name =  "Auto Chat\n Thế Giới"+ "\n["+(PickMob.IsAutoChat ? "On]" : "Off]"),IdAction = 200001},
            };
            
        }
        public void MenuUpAutoMap()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {               
                new Menu(){ Name =  "Load Map"+menutag,IdAction = 121001},
                new Menu(){ Name =  "Ăn Đùi Gà"+ "\n["+(PickMob.IsAnDuiGa ? "On]" : "Off]")+cansavetag,IdAction = 141003},
                new Menu(){ Name =  "Về Nhà\n Thu Đậu"+ "\n["+(PickMob.IsAutoThuDauXin ? "On]" : "Off]"),IdAction = 141004},
                new Menu(){ Name =  "Sử Dụng\nCapsule"+ "\n["+(Pk9rXmap.IsUseCapsuleNormal ? "On]" : "Off]")+cansavetag,IdAction = 141006},
                new Menu(){ Name =  "Tới Map\n",IdAction = 141007},
                new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000001},
                new Menu(){ Name =  "Load Setting",IdAction = 00000012},
            };
            
        }
        public void MenuPicks()
        {
            string addItemToDS = "Thêm Item Vào\nDanh Sách\n Vật Phẩm";
            string addBlockItemToDS = "Thêm Item Vào\nDanh Sách\n Không Nhặt";
            if (PickMob.IdItemPicks.Count > 0)
            {
                addItemToDS = "Xoá Item Khỏi\nDanh Sách\n Vật Phẩm";
            }
            if (PickMob.IdItemBlocks.Count > 0)
            {
                addBlockItemToDS = "Xoá Item Khỏi\nDanh Sách\n Không Nhặt";
            }
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Auto Pick" + "\n["+(PickMob.IsAutoPickItems ? "On]" : "Off]"),IdAction = 121002},
                new Menu(){ Name =  addItemToDS ,IdAction = 190001},
                new Menu(){ Name =  "Nhập ID Item\n Vào Danh Sách\n Vật Phẩm" ,IdAction = 190002},
                new Menu(){ Name =  "Thêm Vào\nDanh Sách\n Loại Vật Phẩm",IdAction = 190003},
                new Menu(){ Name =  "Nhập ID Item\n Vào Danh Sách\n Không Nhặt" ,IdAction = 190004},
                new Menu(){ Name =  addBlockItemToDS,IdAction = 190005},
                new Menu(){ Name =  "Số Lần Nhặt",IdAction = 190006},
                new Menu(){ Name =  "Clear Danh Sách Lọc",IdAction = 190007},
            };
            
        }
        public void MenuAutoSkill()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {               
                new Menu(){ Name =  "Tự Đánh"+ "\n["+(PickMob.IsAK ? "On]" : "Off]"),IdAction = 151003},
                new Menu(){ Name =  "Đánh Khi\nĐệ Cần"+ "\n["+(PickMob.IsAttackForPet ? "On]" : "Off]")+cansavetag,IdAction = 151004},
                new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000002},
                new Menu(){ Name =  "Load Setting",IdAction = 00000013},
            };
            
        }
        public void MenuPean()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Xin Đậu"+ "\n["+(PickMob.IsXinDau ? "On]" : "Off]")+cansavetag,IdAction = 161001},
                new Menu(){ Name =  "Cho Đậu"+ "\n["+(PickMob.IsChoDau ? "On]" : "Off]")+cansavetag,IdAction = 161002},
                new Menu(){ Name =  "Thu Đậu"+ "\n["+(PickMob.IsAutoThuDau ? "On]" : "Off]")+cansavetag,IdAction = 161003},
                new Menu(){ Name =  $"Ăn Đậu Khi\n HP: {PickMob.HpBuff}% \nMP: {PickMob.MpBuff}%"+cansavetag,IdAction = 161004},
                new Menu(){ Name =  "Thu Đậu"+ "\n["+(PickMob.IsAutoThuDau ? "On]" : "Off]")+cansavetag,IdAction = 161003},
                new Menu(){ Name =  "Lưu\n Cài Đặt",IdAction = 0000003},
                new Menu(){ Name =  "Load Setting",IdAction = 00000014},
            };

        }
        public void MenuTrain()
        {
            string text = "Tàn Sát\n Tất Cả";
            if (PickMob.IdMobsTanSat.Count > 0)
            {
                text = "Tàn Sát\n Theo Danh Sách";
            }
            
            //menus.Clear();
            menus = new List<Menu>()
            {
                
                new Menu(){ Name =  text+ "\n["+(PickMob.IsTanSat ? "On]" : "Off]"),IdAction = 121003},
                new Menu(){ Name =  "Né Siêu Quái" + "\n["+(PickMob.IsNeSieuQuai ? "On]" : "Off]")+cansavetag,IdAction = 171001},
                new Menu(){ Name =  "Goback"+ "\n["+(Pk9rXmap.isGoBackbt ? "On]" : "Off]")+cansavetag,IdAction = 121006},
                new Menu(){ Name =  "Thêm loại quái",IdAction = 171004},
                new Menu(){ Name =  "Clear\n Danh Sách Quái",IdAction = 171002},
                new Menu(){ Name =  "Vượt Địa Hình"+ "\n["+(PickMob.IsVuotDiaHinh ? "On]" : "Off]"),IdAction = 171003},
                new Menu(){ Name =  "Lưu\n Cài Đặt",IdAction = 0000004},
                new Menu(){ Name =  "Load Setting",IdAction = 00000015},
            };

        }
        public void MenuMore()
        {
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Goback"+ "\n["+(Pk9rXmap.isGoBackbt ? "On]" : "Off]"),IdAction = 121006},
                new Menu(){ Name =  "Goback\n Toạ Độ"+ "\n["+(Pk9rXmap.isGoBack ? "On]" : "Off]"),IdAction = 121007},
                new Menu(){ Name =  "Thông Báo\n Boss"+ "\n["+(OnScreen.viewBoss ? "On]" : "Off]"),IdAction = 181001},
                new Menu(){ Name =  "Danh Sách\n Char",IdAction = 181002},
                new Menu(){ Name =  "Cập Nhật Khu",IdAction = 181003},
                new Menu(){ Name =  "Xoá Đồ Hoạ"+ "\n["+(OnScreen.IsXoaMap ? "On]" : "Off]")+cansavetag,IdAction = 121008},
                new Menu(){ Name =  "Xoá Đồ Hoạ\n Và Giảm CPU"+ "\n["+(OnScreen.IsXoaMapV ? "On]" : "Off]")+cansavetag,IdAction = 121009},
                new Menu(){ Name =  "Auto Vào Khu",IdAction = 181004},
                new Menu(){ Name =  "Hào Quang",IdAction = 181005},
                new Menu(){ Name =  "Săn boss",IdAction = 181006},
            };

        }
        public void MenuHaoQuang()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Hào Quang 1",IdAction = 1810051},
                new Menu(){ Name =  "Hào Quang 2",IdAction = 1810052},
                new Menu(){ Name =  "Hào Quang 3",IdAction = 1810053},
                new Menu(){ Name =  "Hào Quang 4",IdAction = 1810054},
                //new Menu(){ Name =  "Hào Quang 5",IdAction = 1810055},
            };
        }
        public void MenuSanBoss()
        {
            
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Hào Quang 1",IdAction = 1810051},
                new Menu(){ Name =  "Hào Quang 2",IdAction = 1810052},
                new Menu(){ Name =  "Hào Quang 3",IdAction = 1810053},
                new Menu(){ Name =  "Hào Quang 4",IdAction = 1810054},
                
            };
        }
        public static void showmainmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuMain();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
        public static void showupdetumenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuUpDT();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }public static void showautomapmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuUpAutoMap();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        } public static void showautoskillmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuAutoSkill();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        } public static void showpeanmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuPean();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }public static void showtrainmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuTrain();
            //if (PickMob.TypeMobsTanSat.Count > 0)
            //{
            //    for (int i = 0; i < GameScr.vMob.size(); i++)
            //    {
            //        Mob mob = (Mob)GameScr.vMob.elementAt(i);
            //        if (PickMob.TypeMobsTanSat.Contains(mob.templateId))
            //        {
            //            myVector.addElement(new Command(mob.getTemplate().name, 0));
            //        }
            //    }
            //}
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
        public static void showmoremenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuMore();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
        public static void showpicksmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuPicks();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
        public static void showchatmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuChat();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
        public static void showhaoquangmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuHaoQuang();
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
    }
}
