namespace Uixe.Watcher
{
    public class KeyItem
    {
        public const string TCO_JZ_DJYSC = "0";//大件运输车
        public const string TCO_JZ_BJJZX = "1";//标准集装箱
        public const string TCO_JZ_GZC = "2";//罐装车
        public const string TCO_JZ_SGYS = "3";//甩挂运输
        public const string TCO_JZ_CI_ModifyCarInfo = "4";//修正车辆信息
        public const string TCO_JZ_CI_AddCarInfo = "5";//添加车辆信息
        public const string TCO_JZ_CI_DelCarInfo = "6";//删除车辆信息
        public const string TCO_JZ_CI_ModifyAlex = "7";//修正车辆轴信息
        public const string TCO_JZ_ChangChe = "8";//长车
        public const string TCO_JZ_TZCL = "9";// 特种车辆
        public const string TCO_JZ_ZXCL = "A";//转换车辆
        public const string TCO_CK_JUNCHE = "B";//军车
        public const string TCO_CK_GONGWU = "C";//公务车
        public const string TCO_CK_JINJI = "D";//紧急车
        public const string TCO_CK_LVSETONGDAO = "E";//绿色通道
        public const string TCO_CK_YPCHE = "F";//月票车
        public const string TCO_CK_YHCHE = "G";//优惠卡车
        public const string TCO_CK_JINCHE = "H";//警车
        public const string TCO_CK_NONGYONGCHE = "I";//农用车
        public const string TCO_CK_HapplyDay = "J";//节假日
        public const string TCO_CK_BLACKPlate = "Z";//黑名单
        public const string TCO_CK_ShowInfo = "K"; //显示信息
        public const string TCO_JZ_MeiTan = "M"; //煤炭
        public const string TCO_JZ_5Zhe = "N"; //半价优惠车
        static KeyItem[] keyItems = null;
        public static KeyItem[] GetTCOCK()
        {
            if (keyItems == null)
            {
                keyItems = new KeyItem[]{
                    new KeyItem (TCO_JZ_DJYSC               ,"大件运输车"),
                    new KeyItem (TCO_JZ_BJJZX               ,"标准集装箱"),
                    new KeyItem (TCO_JZ_GZC                 ,"特种罐装车"),
                    new KeyItem (TCO_JZ_SGYS                ,"甩挂运输"),
                    new KeyItem (TCO_JZ_MeiTan                ,"煤炭运输车"),
                      new KeyItem (TCO_JZ_5Zhe                ,"半价优惠车"),
                    new KeyItem (TCO_JZ_CI_ModifyCarInfo    ,"修正车辆信息"),
                    new KeyItem (TCO_JZ_CI_AddCarInfo       ,"添加车辆信息"),
                    new KeyItem (TCO_JZ_CI_DelCarInfo       ,"删除车辆信息"),
                    new KeyItem (TCO_JZ_CI_ModifyAlex       ,"修正车辆轴信息"),
                    new KeyItem (TCO_JZ_ChangChe            ,"长车"),
                    new KeyItem (TCO_JZ_TZCL                 ,"载有固定装置车"),
                    new KeyItem (TCO_JZ_ZXCL                 ,"车型收费模式"),
                    new KeyItem (TCO_CK_JUNCHE               ,"军车"),
                    new KeyItem (TCO_CK_GONGWU               ,"公务车"),
                    new KeyItem (TCO_CK_JINJI                ,"紧急车"),
                    new KeyItem (TCO_CK_LVSETONGDAO          ,"绿色通道"),
                    new KeyItem (TCO_CK_YPCHE                ,"月票车"),
                    new KeyItem (TCO_CK_YHCHE                ,"优惠卡车"),
                    new KeyItem (TCO_CK_JINCHE               ,"警车"),
                    new KeyItem (TCO_CK_NONGYONGCHE          ,"农用车"),
                      new KeyItem (TCO_CK_BLACKPlate          ,"黑名单"),
                    new KeyItem (TCO_CK_HapplyDay            ,"节假日"),
                    new KeyItem( TCO_CK_ShowInfo            ,   "车道特殊情况上报")
            };
            }
            return keyItems;
        }
        static KeyItem[] keyItemshuoche = null;
        public static KeyItem[] GetHueChe()
        {
            if (keyItemshuoche == null)
            {
                keyItemshuoche = new KeyItem[] {
                   new KeyItem ("A" ,"轿客") ,
                   new KeyItem ("B" ,"货车") ,
                   new KeyItem ("C" ,"车型收费模式") ,
                   new KeyItem ("D" ,"载有固定装置车") ,
                   new KeyItem ("E" ,"大件运输车") ,
                   new KeyItem ("F" ,"特种罐装车") ,
                   new KeyItem ("G" ,"标准集装箱") ,
                   new KeyItem ("L" ,"煤炭运输车") ,
                   new KeyItem ("M" ,"半价优惠车") ,
                   new KeyItem ("H" ,"甩挂运输") ,
                   new KeyItem ("I" ,"绿色通道") ,
                   new KeyItem ("J" ,"月票车") ,
                   new KeyItem (TCO_CK_BLACKPlate          ,"黑名单1"),
                   new KeyItem ("K" ,"优惠车") };
            }
            return keyItemshuoche;
        }
        static KeyItem[] keyItemsud = null;
        public static KeyItem[] GetUD()
        {
            if (keyItemsud == null)
            {
                keyItemsud = new KeyItem[]{
                    new KeyItem ("U"               ,"上行"),
                    new KeyItem ("E"               ,"入口"),
                    new KeyItem ("X"               ,"出口"),
                    new KeyItem ("D"                ,"下行")};
            }
            return keyItemsud;
        }

        public KeyItem()
        {
        }

        public KeyItem(string id, string name)
        {
            KeyID = id;
            KeyName = name;
        }

        public string KeyID { get; set; }
        public string KeyName { get; set; }
    }
}