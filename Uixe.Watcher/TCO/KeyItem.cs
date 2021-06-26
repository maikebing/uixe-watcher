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

        /*车型定义*/
        public const int VEHICLE_TYPE_INPUT_MIN = 1;//录入车型的最小值
        public const int VEHICLE_TYPE_INPUT_BUS_MAX = 4;//录入的客车车型的最大值
        public const int VEHICLE_TYPE_INPUT_MAX = 6;//录入车型的最大值
        public const int VEHICLE_TYPE_BUS_01 = 1;//一类客车
        public const int VEHICLE_TYPE_BUS_02 = 2;//二类客车
        public const int VEHICLE_TYPE_BUS_03 = 3;//三类客车
        public const int VEHICLE_TYPE_BUS_04 = 4;//四类客车
        public const int VEHICLE_TYPE_BUS_05 = 5;//五类客车
        public const int VEHICLE_TYPE_BUS_06 = 6;//六类客车
        public const int VEHICLE_TYPE_TRUCK_01 = 11;//一类货车
        public const int VEHICLE_TYPE_TRUCK_02 = 12;//二类货车
        public const int VEHICLE_TYPE_TRUCK_03 = 13;//三类货车
        public const int VEHICLE_TYPE_TRUCK_04 = 14;//四类货车
        public const int VEHICLE_TYPE_TRUCK_05 = 15;//五类货车
        public const int VEHICLE_TYPE_TRUCK_06 = 16;//六类货车
        public const int VEHICLE_TYPE_SPECIAL_01 = 21;//一类专项作业车
        public const int VEHICLE_TYPE_SPECIAL_02 = 22;//二类专项作业车
        public const int VEHICLE_TYPE_SPECIAL_03 = 23;//三类专项作业车
        public const int VEHICLE_TYPE_SPECIAL_04 = 24;//四类专项作业车
        public const int VEHICLE_TYPE_SPECIAL_05 = 25;//五类专项作业车
        public const int VEHICLE_TYPE_SPECIAL_06 = 26;//六类专项作业车

        /*车型定义中文名称*/
        public const string VEHICLE_TYPE_BUS_01_STR = "客一";
        public const string VEHICLE_TYPE_BUS_02_STR = "客二";
        public const string VEHICLE_TYPE_BUS_03_STR = "客三";
        public const string VEHICLE_TYPE_BUS_04_STR = "客四";
        public const string VEHICLE_TYPE_BUS_05_STR = "客五";
        public const string VEHICLE_TYPE_BUS_06_STR = "客六";
        public const string VEHICLE_TYPE_TRUCK_01_STR = "货一";
        public const string VEHICLE_TYPE_TRUCK_02_STR = "货二";
        public const string VEHICLE_TYPE_TRUCK_03_STR = "货三";
        public const string VEHICLE_TYPE_TRUCK_04_STR = "货四";
        public const string VEHICLE_TYPE_TRUCK_05_STR = "货五";
        public const string VEHICLE_TYPE_TRUCK_06_STR = "货六";
        public const string VEHICLE_TYPE_SPECIAL_01_STR = "专项一";
        public const string VEHICLE_TYPE_SPECIAL_02_STR = "专项二";
        public const string VEHICLE_TYPE_SPECIAL_03_STR = "专项三";
        public const string VEHICLE_TYPE_SPECIAL_04_STR = "专项四";
        public const string VEHICLE_TYPE_SPECIAL_05_STR = "专项五";
        public const string VEHICLE_TYPE_SPECIAL_06_STR = "专项六";

        private static KeyItem[] _VEHICLE_TYPEs = null;

        public static KeyItem[] GetVEHICLE_TYPES()
        {
            if (_VEHICLE_TYPEs == null)
            {
                _VEHICLE_TYPEs = new KeyItem[]{
                    new KeyItem( VEHICLE_TYPE_BUS_01 ,VEHICLE_TYPE_BUS_01_STR),
                    new KeyItem( VEHICLE_TYPE_BUS_02 ,VEHICLE_TYPE_BUS_02_STR),
                    new KeyItem( VEHICLE_TYPE_BUS_03 ,VEHICLE_TYPE_BUS_03_STR),
                    new KeyItem( VEHICLE_TYPE_BUS_04 ,VEHICLE_TYPE_BUS_04_STR),
                    new KeyItem( VEHICLE_TYPE_TRUCK_01 ,VEHICLE_TYPE_TRUCK_01_STR),
                    new KeyItem( VEHICLE_TYPE_TRUCK_02 ,VEHICLE_TYPE_TRUCK_02_STR),
                    new KeyItem( VEHICLE_TYPE_TRUCK_03 ,VEHICLE_TYPE_TRUCK_03_STR),
                    new KeyItem( VEHICLE_TYPE_TRUCK_04 ,VEHICLE_TYPE_TRUCK_04_STR),
                    new KeyItem( VEHICLE_TYPE_TRUCK_05 ,VEHICLE_TYPE_TRUCK_05_STR),
                    new KeyItem( VEHICLE_TYPE_TRUCK_06 ,VEHICLE_TYPE_TRUCK_06_STR),
                    new KeyItem( VEHICLE_TYPE_SPECIAL_01 ,VEHICLE_TYPE_SPECIAL_01_STR),
                    new KeyItem( VEHICLE_TYPE_SPECIAL_02 ,VEHICLE_TYPE_SPECIAL_02_STR),
                    new KeyItem( VEHICLE_TYPE_SPECIAL_03 ,VEHICLE_TYPE_SPECIAL_03_STR),
                    new KeyItem( VEHICLE_TYPE_SPECIAL_04 ,VEHICLE_TYPE_SPECIAL_04_STR),
                    new KeyItem( VEHICLE_TYPE_SPECIAL_05 ,VEHICLE_TYPE_SPECIAL_05_STR),
                    new KeyItem( VEHICLE_TYPE_SPECIAL_06 ,VEHICLE_TYPE_SPECIAL_06_STR)
            };
            }
            return _VEHICLE_TYPEs;
        }

        private static KeyItem[] keyItems = null;

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

        private static KeyItem[] keyItemshuoche = null;

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

        private static KeyItem[] keyItemsud = null;

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

        public KeyItem(int id, string name)
        {
            KeyId_Int = id;
            KeyName = name;
        }

        public int KeyId_Int { get; set; }

        public string KeyID { get; set; }
        public string KeyName { get; set; }
    }
}