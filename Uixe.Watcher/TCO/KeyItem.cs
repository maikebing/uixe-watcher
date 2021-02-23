using System;
using Uixe.Watcher.Dtos;

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
                    new KeyItem ( Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_DAJIANCHE),"大件运输车"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE. WATCHER_ContainerTruck ),"标准集装箱"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_FoundTrailer),"特种罐装车"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_ShuaiGuaChe),"甩挂运输"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_TankTruck),"煤炭运输车"),
                      new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_YouHuiJianBan),"半价优惠车"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_IWantModifyWeightInfo)  ,"修正车辆信息"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_IWantAddWeightInfo),"添加车辆信息"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_IWantDelWeightInfo),"删除车辆信息"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_FoundALongCar),"长车"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_FixedUnitCar),"载有固定装置车"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_GreenPassage),"绿色通道"),
                    new KeyItem(Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_State43_ModifyCarType),"修改车型"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_LTORNONGYONG)              ,"农用车"),
                      new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_BlacklistPlate)             ,"黑名单"),
                    new KeyItem (Enum.GetName(typeof(WATCHER_TYPE) ,WATCHER_TYPE.WATCHER_MergeVehicles)              ,"合并车辆")
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