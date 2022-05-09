using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.Mod.PickMob
{
    public class TextFieldChatMod
    {
        public string strChat_ASTTNL_When_HP_MP = "Tái tạo năng lượng khi dưới %HP và %MP";
        public string tfChatName_ASTTNL_When_HP_MP = "%HP_%MP(dấu _ là dấu cách)";

        //public string strChat_BuyS_Delay = "Giá và số lượng cách nhau bởi giấu phẩy";
        //public string tfChatName_BuyS_Delay = "Nhập thời gian delay(mili giây) và số lượng";

        public string strChat_Use_Pean_When_HP_MP = "Dùng đậu khi dưới %HP và %MP";
        public string tfChatName_Use_Pean_When_HP_MP = "%HP_%MP(dấu _ là dấu cách).";

        public string strChat_Set_Time_Quantity_When_Buy_Item = "Nhập thời gian (giây) và số lượng khi mua nhiều vật phẩm";
        public string tfChatName_Set_Time_Quantity_When_Buy_Item = "Thời Gian_Số Lượng (dấu _ là dấu cách)";

        public string strChat_Input_Quantity= "Nhập số lượng";
        public string tfChatName_Input_Quantity = "Nhập số lượng mua";

        public string strChat_Input_Quantity_Sales= "Nhập số lượng";
        public string tfChatName_Input_Quantity_Sales = "Nhập số lượng bán";

        public string strChat_Input_ID_Map= "Nhập ID Map Cần Đến";
        public string tfChatName_Input_ID_Map = "Nhập ID Map";

        public string strChat_Input_ID_ITEM_TO_PICK_LIST= "Nhập ID Item Cần Cho Vào Danh Sách Nhặt";
        public string tfChatName_Input_ID_ITEM_TO_PICK_LIST = "Nhập ID Item Cần Cho Vào Danh Sách Nhặt";

        public string strChat_Input_ID_ITEM_TO_PICK_BLOCK_LIST= "Nhập ID Item Cần Cho Vào Danh Sách Không Nhặt";
        public string tfChatName_Input_ID_ITEM_TO_PICK_BLOCK_LIST = "Nhập ID Item Cần Cho Vào Danh Sách Không Nhặt";

        public string strChat_Input_SLN= "Nhập Số Lần Nhặt";
        public string tfChatName_Input_SLN = "Nhập Số Lần Nhặt";

        public string strChat_Input_Auto_Vao_Khu= "Nhập Khu Cần Vào";
        public string tfChatName_Input_Auto_Vao_Khu = "Nhập Khu Cần Vào";

        public string strChat_Input_Quantity_USE = "Nhập Số Lượng Cần Mở";
        public string tfChatName_Input_Quantity_USE = "Nhập Số Lượng Cần Mở";

        public string strChat_Input_Auto_Chat = "Nhập nội dung auto chat";
        public string tfChatName_Input_Auto_Chat = "Nhập nội dung auto chat";

        public string strChat_Input_Auto_ChatTG = "Nhập nội dung auto chat thế giới";
        public string tfChatName_Input_Auto_ChatTG = "Nhập nội dung auto chat thế giới";

        public string strChat_Input_ID_TYPE_ITEM_PICK = "Nhập ID Loại Item";
        public string tfChatName_Input_ID_TYPE_ITEM_PICK = "ID Loại Item";

        public string strChat_Input_ID_TYPE_ITEM_BLOCK = "Nhập ID Loại Item Không Nhặt";
        public string tfChatName_Input_ID_TYPE_ITEM_BLOCK = "ID Loại Item Không Nhặt";

        public string strChat_Input_ID_MOB = "Nhập ID Quái";
        public string tfChatName_Input_ID_MOB = "ID Quái";

        public string strChat_Input_ID_TYPE_MOB = "Nhập ID Loại Quái";
        public string tfChatName_Input_ID_TYPE_MOB= "ID Loại Quái";

        private static TextFieldChatMod _Instance;
        public static TextFieldChatMod Instance()
        {
            if (_Instance == null)
                _Instance = new();
            return _Instance;
        }
    }
}
