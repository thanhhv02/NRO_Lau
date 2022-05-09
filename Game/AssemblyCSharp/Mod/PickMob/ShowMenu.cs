using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
                    PickMobAuto.TeleVip((int)p);
                    break;
                case 121001:
                    Pk9rXmap.Chat("xmp");
                    break;
                case 121002:
                    PickMobAuto.Chat("anhat");
                    break;
                case 121003:
                    PickMobAuto.Chat("ts");
                    break;
                case 121004:
                    PickMobAuto.Chat("atc");
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
                    PickMobAuto.Chat("ndt");
                    break;
                case 131003:
                    PickMobAuto.Chat("aflag8");
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
                    PickMobAuto.Chat("aduiga");
                    break;
                case 141004:
                    PickMobAuto.Chat("athudau");
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
                    PickMobAuto.Chat("ak");
                    break;
                case 151004:
                    PickMobAuto.IsAttackForPet = !PickMobAuto.IsAttackForPet;
                    break;
                case 151005:
                    show8skillmenu();
                    break;
                case 151006:
                    PickMobAuto.Chat("skill1");
                    break;
                case 151007:
                    PickMobAuto.Chat("skill2");
                    break;
                case 151008:
                    PickMobAuto.Chat("skill3");
                    break;
                case 151009:
                    PickMobAuto.Chat("skill4");
                    break;
                case 151010:
                    PickMobAuto.Chat("skill5");
                    break;
                case 151011:
                    PickMobAuto.Chat("skill6");
                    break;
                case 151012:
                    PickMobAuto.Chat("skill7");
                    break;
                case 151013:
                    PickMobAuto.Chat("skill8");
                    break;
                case 151014:
                    PickMobAuto.Chat("clrs");
                    break;
                    /////////// PEAN //////////////
                case 161000:
                    showpeanmenu();
                    break;
                case 161001:
                    PickMobAuto.Chat("xindau");
                    break;
                case 161002:
                    PickMobAuto.Chat("chodau");
                    break;
                case 161003:
                    PickMobAuto.Chat("thudau");
                    break;
                case 161004:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Use_Pean_When_HP_MP
                        , TextFieldChatMod.Instance().strChat_Use_Pean_When_HP_MP, GameScr.gI());
                    break;
                case 171000:
                    showtrainmenu();
                    break;
                case 171001:
                    PickMobAuto.Chat("nsq");
                    break;
                case 171002:
                    PickMobAuto.Chat("clrm");
                    break;
                case 171003:
                    PickMobAuto.Chat("vdh");
                    break;
                case 171004:
                    PickMobAuto.Chat("addt");
                    break;
                case 171005:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_MOB
                        , TextFieldChatMod.Instance().strChat_Input_ID_MOB, GameScr.gI());
                    break;
                case 171006:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_TYPE_MOB
                        , TextFieldChatMod.Instance().strChat_Input_ID_TYPE_MOB, GameScr.gI());
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
                    PickMobAuto.Chat("ukhu"); 
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
                    AutoMapSettingWriter.Write(Setttings.AnDuiGa.ToString() + ":"+PickMobAuto.IsAnDuiGa+"|");
                    AutoMapSettingWriter.Write(Setttings.UseCapsuleNormal.ToString() + ":" + Pk9rXmap.IsUseCapsuleNormal + "|");
                    AutoMapSettingWriter.Close();
                    var AutoGobackWriter = new StreamWriter("Data\\AutoGobackSetting.ini");
                    AutoGobackWriter.Write(Setttings.Goback.ToString() + ":" + Pk9rXmap.isGoBackbt + "|");
                    AutoGobackWriter.Write(Setttings.GobackPosition.ToString() + ":" + Pk9rXmap.isGoBack + "|");
                    AutoGobackWriter.Write(Setttings.MapID.ToString() + ":" + XmapController.MapID + "|");
                    AutoGobackWriter.Write(Setttings.ZoneID.ToString() + ":" + XmapController.ZoneID + "|");
                    AutoGobackWriter.Write(Setttings.cx.ToString() + ":" + XmapController.cx + "|");
                    AutoGobackWriter.Write(Setttings.cy.ToString() + ":" + XmapController.cy + "|");
                    AutoGobackWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Map", 0);
                    break;
                case 00000011:
                    LoadSetting.LOAD_SETTING_AUTO_PET();
                    break;
                case 0000002:
                    var AutoSkillWriter = new StreamWriter("Data\\AutoSkillSetting.ini");
                    AutoSkillWriter.Write(Setttings.AttackForPet.ToString() + ":" + PickMobAuto.IsAttackForPet + "|");
                    AutoSkillWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Skill", 0);
                    break;
                case 00000012:
                    LoadSetting.LOAD_SETTING_AUTO_MAP();
                    break;
                case 0000003:
                    var AutoPeanWriter = new StreamWriter("Data\\AutoPeanSetting.ini");
                    AutoPeanWriter.Write(Setttings.XinDau.ToString() + ":" + PickMobAuto.IsXinDau + "|");
                    AutoPeanWriter.Write(Setttings.ChoDau.ToString() + ":" + PickMobAuto.IsChoDau + "|");
                    AutoPeanWriter.Write(Setttings.ThuDau.ToString() + ":" + PickMobAuto.IsAutoThuDau + "|");
                    AutoPeanWriter.Write(Setttings.UseHPPotionWhen.ToString() + ":" + PickMobAuto.HpBuff + "|");
                    AutoPeanWriter.Write(Setttings.UseMPPotionWhen.ToString() + ":" + PickMobAuto.MpBuff + "|");
                    AutoPeanWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Pean", 0);
                    break;
                case 00000013:
                    LoadSetting.LOAD_SETTING_AUTO_SKILL();
                    break;
                case 0000004:
                    var AutoTrainWriter = new StreamWriter("Data\\AutoTrainSetting.ini");
                    AutoTrainWriter.Write(Setttings.NeSieuQuai.ToString() + ":" + PickMobAuto.IsNeSieuQuai + "|");
                    AutoTrainWriter.Write(Setttings.TanSat.ToString() + ":" + PickMobAuto.IsTanSat + "|");
                    AutoTrainWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Train", 0);
                    break;
                case 00000014:
                    LoadSetting.LOAD_SETTING_AUTO_PEAN();
                    break;
                case 0000005:
                    var AutoUpPetWriter = new StreamWriter("Data\\AutoUpPetSetting.ini");
                    AutoUpPetWriter.Write(Setttings.PickPetDropItem.ToString() + ":" + PickMobAuto.IsNhatDoUpDT + "|");
                    AutoUpPetWriter.Write(Setttings.AutoFlag_8.ToString() + ":" + PickMobAuto.IsAutoFlag + "|");
                    AutoUpPetWriter.Write(Setttings.UseTTNLWhenHP.ToString() + ":" + PickMobAutoController.aHP + "|");
                    AutoUpPetWriter.Write(Setttings.UseTTNLWhenMP.ToString() + ":" + PickMobAutoController.aMP + "|");
                    //AutoUpPetWriter.Write(Setttings.XoaDoHoa.ToString() + ":" + OnScreen.IsXoaMap + "|");
                    //AutoUpPetWriter.Write(Setttings.XoaDoHoaGiamCPU.ToString() + ":" + OnScreen.IsXoaMapV + "|");
                    AutoUpPetWriter.Write(Setttings.XNhat.ToString() + ":" + PickMobAutoController.XNhat + "|");
                    AutoUpPetWriter.Write(Setttings.YNhat.ToString() + ":" + PickMobAutoController.YNhat + "|");
                    
                    AutoUpPetWriter.Close();
                    GameScr.info1.addInfo("Đã lưu cài đặt Auto Up Pet", 0);
                    break;
                case 0000006:
                    var DoHoaWriter = new StreamWriter("Data\\DoHoaSetting.ini");
                    DoHoaWriter.Write(Setttings.XoaDoHoa.ToString() + ":" + OnScreen.IsXoaMap + "|");
                    DoHoaWriter.Write(Setttings.XoaDoHoaGiamCPU.ToString() + ":" + OnScreen.IsXoaMapV + "|");
                    DoHoaWriter.Close();
                    break;
                case 00000015:
                    LoadSetting.LOAD_SETTING_AUTO_TRAIN();
                    break;
                case 190001:
                    PickMobAuto.Chat("add");
                    break;
                case 190002:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_ITEM_TO_PICK_LIST
                        , TextFieldChatMod.Instance().strChat_Input_ID_ITEM_TO_PICK_LIST, GameScr.gI());
                    break;
                case 190003:
                    PickMobAuto.Chat("addt");
                    break;
                case 190004:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_ITEM_TO_PICK_BLOCK_LIST
                        , TextFieldChatMod.Instance().strChat_Input_ID_ITEM_TO_PICK_BLOCK_LIST, GameScr.gI());
                    break;
                case 190005:
                    PickMobAuto.Chat("blocki");
                    break;
                case 190006:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_SLN
                        , TextFieldChatMod.Instance().strChat_Input_SLN, GameScr.gI());
                    break;
                case 190007:
                    PickMobAuto.Chat("itm");
                    break;
                case 190008:
                    PickMobAuto.Chat("clri");
                    break;
                case 190009:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_TYPE_ITEM_PICK
                        , TextFieldChatMod.Instance().strChat_Input_ID_TYPE_ITEM_PICK, GameScr.gI());
                    break;
                case 190010:
                    CreateStartChat(TextFieldChatMod.Instance().tfChatName_Input_ID_TYPE_ITEM_PICK
                        , TextFieldChatMod.Instance().strChat_Input_ID_TYPE_ITEM_PICK, GameScr.gI());
                    break;
                case 190011:
                    PickMobAuto.Chat("cnn");
                    break;
                case 190000:
                    showpicksmenu();
                    break;
                case 200000:
                    showchatmenu();
                    break;
                case 200001:
                    PickMobAuto.Chat("atgtg");
                    break;
                case 210000:
                    myTypeList();
                    break;
                case 220000:
                    showdohoamenu();
                    break;
                case 230000:
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_AUTO_GOBACK();
                    })
                    {
                        IsBackground = true
                    }.Start();
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_AUTO_MAP();
                    })
                    {
                        IsBackground = true
                    }.Start();
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_AUTO_PEAN();
                    })
                    {
                        IsBackground = true
                    }.Start();
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_AUTO_PET();
                    })
                    {
                        IsBackground = true
                    }.Start();
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_AUTO_SKILL();
                    })
                    {
                        IsBackground = true
                    }.Start();
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_AUTO_TRAIN();
                    })
                    {
                        IsBackground = true
                    }.Start();
                    new Thread(delegate () {
                        LoadSetting.LOAD_SETTING_DO_HOA(); 
                    })
                    {
                        IsBackground = true
                    }.Start();
                    break;
            }

        }
        public void CapsuleSelect(int selected)
        {
            switch (selected)
            {
                case 0:
                    PickMobAuto.Chat("dd1");
                    break;
                case 1:
                    PickMobAuto.Chat("dd2");
                    break;
                case 2:
                    PickMobAuto.Chat("vq1");
                    break;
                case 3:
                    PickMobAuto.Chat("vq2");
                    break;
                case 4:
                    PickMobAuto.Chat("epnr");

                    break;
                case 5:
                    PickMobAuto.Chat("acn");
                    break;
                case 6:
                    PickMobAuto.Chat("abh");
                    break;
                case 7:
                    PickMobAuto.Chat("abk");
                    break;
                case 8:
                    PickMobAuto.Chat("agx");
                    break;
                case 9:
                    if(PickMobAuto.IsBanMDV == true)
                    {
                        PickMobAuto.IsBanMDV = false;
                        return;
                    }
                    GameCanvas.startYesNoDlg("Đừng để đồ không phải MDV trong hành trang có thể bị lỗi bán nhầm\n[Tiếp tục ?]", new Command(mResources.YES, 25553), new Command(mResources.NO, 25554));
                    break;
                case 10:
                    if (PickMobAuto.IsBanNR7s == true)
                    {
                        PickMobAuto.IsBanNR7s = false;
                        return;
                    }
                    GameCanvas.startYesNoDlg("Đừng để đồ không phải 7S trong hành trang có thể bị lỗi bán nhầm\n[Tiếp tục ?]", new Command(mResources.YES, 25555), new Command(mResources.NO, 25554));
                    break;
                case 11:
                    PickMobAuto.IsDapDa = !PickMobAuto.IsDapDa;
                    GameScr.info1.addInfo("Đập đá: " + OnOrOff(PickMobAuto.IsDapDa), 0);
                    break;
                default:
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
        public void LoadMenuFromFile(string path)
        {
            try
            {
                menus = new List<Menu>();
                Menu menu;

                    StreamReader sr = new StreamReader(path);
                    string textLine;

                    while ((textLine = sr.ReadLine()) != null)
                    {
                        textLine = textLine.Trim();

                        if (textLine.StartsWith("#") || textLine.Equals(""))
                            continue;

                        string[] textData = textLine.Split('|');

                        for (int i = 0; i < textData.Length; i += 2)
                        {

                            //GameScr.info1.addInfo(textData[i] + "\n" + int.Parse(textData[i + 1]), 0);
                            menu = new Menu();
                            menu.Name = textData[i];
                            menu.IdAction = int.Parse(textData[i + 1]);
                            menus.Add(menu);
                        }
                       
                    }
                    myVector.removeAllElements();
                    foreach (var showmenu in menus)
                    {
                        count++;
                        myVector.addElement(new Command(showmenu.Name+menutag, showmenu.IdAction));
                    }
                    GameCanvas.menu.startAt(myVector, count);
                    menus.Clear();
                    count = 1;

            }
            catch (Exception e)
            {
                GameScr.info1.addInfo(e.Message, 0);
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
        public static void myTypeList()
        {
            Panel.isMySelect = true;
            List<string> array = new List<string>();
            try
            {

                StreamReader sr = new StreamReader(Menu.MenuCapsulePath);
                string textLine;

                while ((textLine = sr.ReadLine()) != null)
                {
                    textLine = textLine.Trim();

                    if (textLine.StartsWith("#") || textLine.Equals(""))
                        continue;

                    string[] textData = textLine.Split(',');

                    for (int i = 0; i < textData.Length; i += 2)
                    {
                        array.Add(textData[i]);
                    }

                }
            }
            catch (Exception e)
            {
                GameScr.info1.addInfo(e.Message, 0);
            }
            
            GameCanvas.panel.mapNames = new string[array.Count];
            GameCanvas.panel.planetNames = new string[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                GameCanvas.panel.mapNames[i] = i.ToString()  + ". " + array[i];
                string status = " Status: ";
                switch (i)
                {
                    case 0:
                        GameCanvas.panel.planetNames[0] = status + OnOrOff(PickMob.PickMobAuto.DapDo1);
                        break;
                    case 1:
                        GameCanvas.panel.planetNames[1] = status + OnOrOff(PickMob.PickMobAuto.DapDo2);
                        break;
                    case 2:
                        GameCanvas.panel.planetNames[2] = status + OnOrOff(PickMob.PickMobAuto.VongQuay1);
                        break;
                    case 3:
                        GameCanvas.panel.planetNames[3] = status + OnOrOff(PickMob.PickMobAuto.VongQuay2);
                        break;
                    case 4:
                        GameCanvas.panel.planetNames[4] = status + OnOrOff(PickMob.PickMobAuto.IsEpNR);
                        break;
                    case 5:
                        GameCanvas.panel.planetNames[5] = status + OnOrOff(PickMob.PickMobAuto.IsACN);
                        break;
                    case 6:
                        GameCanvas.panel.planetNames[6] = status + OnOrOff(PickMob.PickMobAuto.IsABH);
                        break;
                    case 7:
                        GameCanvas.panel.planetNames[7] = status + OnOrOff(PickMob.PickMobAuto.IsABK);
                        break;
                    case 8:
                        GameCanvas.panel.planetNames[8] = status + OnOrOff(PickMob.PickMobAuto.IsAGX);
                        break;
                    case 9:
                        GameCanvas.panel.planetNames[9] = status + OnOrOff(PickMob.PickMobAuto.IsBanMDV);
                        break;
                    case 10:
                        GameCanvas.panel.planetNames[10] = status + OnOrOff(PickMob.PickMobAuto.IsBanNR7s);
                        break;
                    case 11:
                        GameCanvas.panel.planetNames[11] = status + OnOrOff(PickMob.PickMobAuto.IsDapDa);
                        break;
                    default:
                        break;
                }
            }
            GameCanvas.panel.setTypeMapTrans();
            GameCanvas.panel.show();
        }
        public void MenuMain()
        {
            //menus.Clear();
            LoadMenuFromFile(Menu.MenuMain);
        }
        public void MenuUpDT()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {               
                new Menu(){ Name =  "Nhặt Đồ\n Đệ Tử"+ "\n["+(PickMobAuto.IsNhatDoUpDT ? "On]" : "Off]")+cansavetag,IdAction = 131002},
                new Menu(){ Name =  "Đánh Khi\nĐệ Cần"+ "\n["+(PickMobAuto.IsAttackForPet ? "On]" : "Off]")+cansavetag,IdAction = 151004},
                new Menu(){ Name =  "Auto cờ đen"+ "\n["+(PickMobAuto.IsAutoFlag ? "On]" : "Off]")+cansavetag,IdAction = 131003},
                new Menu(){ Name = $"TTNL\n{PickMobAutoController.aHP}% HP\n{PickMobAutoController.aMP}% MP"+cansavetag,IdAction = 131004},
                //new Menu(){ Name =  "Xoá Đồ Hoạ"+ "\n["+(OnScreen.IsXoaMap ? "On]" : "Off]")+cansavetag,IdAction = 121008},
                //new Menu(){ Name =  "Xoá Đồ Hoạ\n Và Giảm CPU"+ "\n["+(OnScreen.IsXoaMapV ? "On]" : "Off]")+cansavetag,IdAction = 121009},
                new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000005},
                //new Menu(){ Name =  "Load Setting",IdAction = 00000011},
            };
            
        }
        public void MenuDoHoa()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Xoá Đồ Hoạ"+ "\n["+(OnScreen.IsXoaMap ? "On]" : "Off]")+cansavetag,IdAction = 121008},
                new Menu(){ Name =  "Xoá Đồ Hoạ\n Và Giảm CPU"+ "\n["+(OnScreen.IsXoaMapV ? "On]" : "Off]")+cansavetag,IdAction = 121009},
                new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000006},
            };
            
        }
        public void MenuChat()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Auto Chat"+ "\n["+(PickMobAuto.IsAutoChat ? "On]" : "Off]"),IdAction = 121004},
                new Menu(){ Name =  "Auto Chat\n Thế Giới"+ "\n["+(PickMobAuto.IsAutoChat ? "On]" : "Off]"),IdAction = 200001},
            };
            
        }
        public void MenuUpAutoMap()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {               
                new Menu(){ Name =  "Load Map"+menutag,IdAction = 121001},
                new Menu(){ Name =  "Ăn Đùi Gà"+ "\n["+(PickMobAuto.IsAnDuiGa ? "On]" : "Off]")+cansavetag,IdAction = 141003},
                new Menu(){ Name =  "Về Nhà\n Thu Đậu"+ "\n["+(PickMobAuto.IsAutoThuDauXin ? "On]" : "Off]"),IdAction = 141004},
                new Menu(){ Name =  "Sử Dụng\nCapsule"+ "\n["+(Pk9rXmap.IsUseCapsuleNormal ? "On]" : "Off]")+cansavetag,IdAction = 141006},
                new Menu(){ Name =  "Tới Map\n",IdAction = 141007},
                new Menu(){ Name =  "Goback"+ "\n["+(Pk9rXmap.isGoBackbt ? "On]" : "Off]")+cansavetag,IdAction = 121006},
                new Menu(){ Name =  "Goback\n Toạ Độ"+ "\n["+(Pk9rXmap.isGoBack ? "On]" : "Off]")+cansavetag,IdAction = 121007},
                new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000001},
                //new Menu(){ Name =  "Load Setting",IdAction = 00000012},
            };
            
        }
        public void MenuPicks()
        {
            string addItemToDS = "Thêm Item Nhặt";
            string addBlockItemToDS = "Thêm Item Không Nhặt";
            if (PickMobAuto.IdItemPicks.Count > 0)
            {
                foreach (var item in PickMobAuto.IdItemPicks)
                {
                    if (Char.myCharz().itemFocus.template.id == PickMobAuto.IdItemPicks[item])
                        addItemToDS = "Xoá Item Nhặt";
                }
                
            }
            if (PickMobAuto.IdItemBlocks.Count > 0)
            {
                foreach (var item in PickMobAuto.IdItemPicks)
                {
                    if (Char.myCharz().itemFocus.template.id == PickMobAuto.IdItemBlocks[item])
                        addBlockItemToDS = "Xoá Item Không Nhặt (trỏ vào vp)";
                }
                
            }
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Auto Pick" + "\n["+(PickMobAuto.IsAutoPickItems ? "On]" : "Off]"),IdAction = 121002},//anhat
                new Menu(){ Name =  addItemToDS ,IdAction = 190001},// add
                new Menu(){ Name =  "Nhập ID Item Cần Nhặt" ,IdAction = 190002},// addi
                new Menu(){ Name =  "Nhập ID Loại Item Cần Nhặt" ,IdAction = 190009},// addti
                new Menu(){ Name =  "Nhặt Loại Item",IdAction = 190003},//addt
                new Menu(){ Name =  "Nhập ID Item Không Nhặt" ,IdAction = 190004},//blocki_
                new Menu(){ Name =  addBlockItemToDS,IdAction = 190005},//blocki
                new Menu(){ Name =  "Nhập ID Loại Item Không Nhặt",IdAction = 190010},//blockti
                new Menu(){ Name =  "Số Lần Nhặt",IdAction = 190006},//sln
                new Menu(){ Name =  "Nhặt Vật Phẩm Của Mình",IdAction = 190007},//itm
                new Menu(){ Name =  "Clear Danh Sách Lọc",IdAction = 190008},
                new Menu(){ Name =  "Chỉ Nhặt Ngọc",IdAction = 190011},
            };
            
        }
        public void MenuAutoSkill()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {               
                new Menu(){ Name =  "Tự Đánh"+ "\n["+(PickMobAuto.IsAK ? "On]" : "Off]"),IdAction = 151003},
                
                new Menu(){ Name = $"TTNL\n{PickMobAutoController.aHP}% HP\n{PickMobAutoController.aMP}% MP"+cansavetag,IdAction = 131004},
                new Menu(){ Name = $"Thêm skill khi tàn sát",IdAction = 151005},
                //new Menu(){ Name =  "Lưu Cài Đặt",IdAction = 0000002},
                //new Menu(){ Name =  "Load Setting",IdAction = 00000013},
            };
            
            Menu menu = new Menu();
            menu.Name = "Lưu Cài Đặt";
            menu.IdAction = 0000002;
            menus.Add(menu);
        }
        public void MenuAuto8Skill()
        {
            //menus.Clear();
            Menu menu1;
            int a = 151005;
            for (int i = 0; i < global::Char.myCharz().nClass.skillTemplates.Length; i++)
            {
                a += 1;
                menu1 = new Menu();
                SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
                menu1.Name = skillTemplate.name;
                menu1.IdAction = a;
                menus.Add(menu1);
            }
            menu1 = new Menu();
            menu1.Name = "Clear Danh Sách Skill";
            menu1.IdAction = 151014;
            menus.Add(menu1);

        }
        public void MenuPean()
        {
            //menus.Clear();
            menus = new List<Menu>()
            {
                new Menu(){ Name =  "Xin Đậu"+ "\n["+(PickMobAuto.IsXinDau ? "On]" : "Off]")+cansavetag,IdAction = 161001},
                new Menu(){ Name =  "Cho Đậu"+ "\n["+(PickMobAuto.IsChoDau ? "On]" : "Off]")+cansavetag,IdAction = 161002},
                new Menu(){ Name =  "Thu Đậu"+ "\n["+(PickMobAuto.IsAutoThuDau ? "On]" : "Off]")+cansavetag,IdAction = 161003},
                new Menu(){ Name =  $"Ăn Đậu Khi\n HP: {PickMobAuto.HpBuff}% \nMP: {PickMobAuto.MpBuff}%"+cansavetag,IdAction = 161004},
                new Menu(){ Name =  "Thu Đậu"+ "\n["+(PickMobAuto.IsAutoThuDau ? "On]" : "Off]")+cansavetag,IdAction = 161003},
                new Menu(){ Name =  "Lưu\n Cài Đặt",IdAction = 0000003},
                //new Menu(){ Name =  "Load Setting",IdAction = 00000014},
            };

        }
        public void MenuTrain()
        {
            string text = "Tàn Sát\n Tất Cả";
            if (PickMobAuto.IdMobsTanSat.Count > 0)
            {
                text = "Tàn Sát\n Theo Danh Sách";
            }
            
            //menus.Clear();
            menus = new List<Menu>()
            {
                
                new Menu(){ Name =  text+ "\n["+(PickMobAuto.IsTanSat ? "On]" : "Off]"),IdAction = 121003},
                new Menu(){ Name =  "Né Siêu Quái" + "\n["+(PickMobAuto.IsNeSieuQuai ? "On]" : "Off]")+cansavetag,IdAction = 171001},
                //new Menu(){ Name =  "Goback"+ "\n["+(Pk9rXmap.isGoBackbt ? "On]" : "Off]")+cansavetag,IdAction = 121006},
                new Menu(){ Name =  "Thêm loại quái",IdAction = 171004},
                new Menu(){ Name =  "Nhập ID Quái",IdAction = 171005},
                new Menu(){ Name =  "Nhập ID Loại Quái",IdAction = 171006},
                new Menu(){ Name =  "Clear\n Danh Sách Quái",IdAction = 171002},
                new Menu(){ Name =  "Vượt Địa Hình"+ "\n["+(PickMobAuto.IsVuotDiaHinh ? "On]" : "Off]"),IdAction = 171003},
                new Menu(){ Name =  "Lưu\n Cài Đặt",IdAction = 0000004},
                //new Menu(){ Name =  "Load Setting",IdAction = 00000015},
            };

        }
        public void MenuMore()
        {
            menus = new List<Menu>()
            {
                //new Menu(){ Name =  "Goback"+ "\n["+(Pk9rXmap.isGoBackbt ? "On]" : "Off]"),IdAction = 121006},
                //new Menu(){ Name =  "Goback\n Toạ Độ"+ "\n["+(Pk9rXmap.isGoBack ? "On]" : "Off]"),IdAction = 121007},
                new Menu(){ Name =  "Thông Báo\n Boss"+ "\n["+(OnScreen.viewBoss ? "On]" : "Off]"),IdAction = 181001},
                new Menu(){ Name =  "Danh Sách\n Char",IdAction = 181002},
                new Menu(){ Name =  "Cập Nhật Khu"+ "\n["+(PickMobAuto.ukhu ? "On]" : "Off]"),IdAction = 181003},
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
        public void MenuAuto()
        {
            LoadMenuFromFile("Data\\Menu\\MenuAuto.txt");

            //menus = new List<Menu>()
            //{
            //    new Menu(){ Name =  "Hào Quang 1",IdAction = 1810051},
            //    new Menu(){ Name =  "Hào Quang 2",IdAction = 1810052},
            //    new Menu(){ Name =  "Hào Quang 3",IdAction = 1810053},
            //    new Menu(){ Name =  "Hào Quang 4",IdAction = 1810054},

            //};
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
        public static void show8skillmenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuAuto8Skill();
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
        public static void showautomenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().LoadMenuFromFile("Data\\Menu\\MenuAuto.txt");
            foreach (var showmenu in menus)
            {
                count++;
                myVector.addElement(new Command(showmenu.Name, showmenu.IdAction));
            }
            GameCanvas.menu.startAt(myVector, count);
            menus.Clear();
            count = 1;
        }
        public static void showdohoamenu()
        {
            myVector.removeAllElements();
            ShowMenu.Instance().MenuDoHoa();
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
