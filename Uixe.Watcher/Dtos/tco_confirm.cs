using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Dtos
{

    //#define MenuUnEnable 2//不可用未选中
    //#define MenuNone		0	//无菜单。
    //#define MenuCardBox		1	//卡机
    //#define MenuSimuate		2	//模拟菜单
    //#define MenuWeight		3	//计重菜单
    //#define MenuWeiZhang	4	//违章
    //#define MenuSystem		5	//系统菜单
    //#define MenuPassCard    6   //通行卡菜单
    //#define MenuSaoMa       7   //扫码菜单
    //#define MenuETCSystem   8   //ETC系统菜单

    public enum WATCHER_TYPE : int
    {

        WATCHER_MenuCardBox = 1,
        WATCHER_MenuSimuate = 2,
        WATCHER_MenuWeight = 3,//
        WATCHER_IsULoopCar = 4,//卡读取成功
        WATCHER_State43_ModifyCarType = 5,  // 读卡后发现车型不符。
        WATCHER_State44_ModifyCarNumber = 6,    // 修改车牌号码
        WATCHER_State40_CarRuningTimeout = 7,   //超时车辆
        WATCHER_Status_Error = 8,//状态错误，未经入口站发出
        WATCHER_UnKnowPlaza = 9,    //不明入口站
        WATCHER_IWantModifyWeightInfo = 10, //告诉监控，我要修改记重信息  是否允许
        WATCHER_IWantAddWeightInfo = 11,//告诉监控，我要添加一辆车的计重信息， 是否允许
        WATCHER_IWantDelWeightInfo = 12,    //告诉监控，我要删除一辆车的计重信息，是否允许
        WATCHER_FoundALongCar = 13,//告诉监控，发现一个长车
        WATCHER_FoundTrailer = 14,//告诉监控，发现一个拖车
        WATCHER_GreenPassage = 15,  //绿色通道
        WATCHER_ContainerTruck = 16,    // 集装箱
        WATCHER_TankTruck = 17, //罐车
        WATCHER_ContainerTruck_UseCard = 18,// 集装箱
        WATCHER_LTORNONGYONG = 19,//农用车
        WATCHER_DAJIANCHE = 20,//大件运输车
        WATCHER_FixedUnitCar = 21,//载有固定装置车
        WATCHER_YouHuiJianBan = 22,  //减半优惠车辆 add by hxz 20180322
        WATCHER_ShuaiGuaChe = 23, //甩挂运输车    add by hxz 20180329
        WATCHER_BlacklistPlate = 24,    //黑名单车牌
        WATCHER_CarRunInfoCheck = 25,//封闭式出口车辆运行信息检查
        WATCHER_MergeVehicles = 26	//告诉监控，我要删除一辆车的计重信息，是否允许
    }
    public enum DlgType
    {
        Weight,
        Call
    }
    public class tco_confirm
    {
        /// <summary>
        /// 
        /// </summary>
        public Head Head { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SubHead SubHead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MsgTcoTran MsgTcoTran { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WATCHER_TYPE WatcherID { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DlgType DlgType { get; set; }
    }

    public class MSG_TCOConfirm
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WATCHER_TYPE TCOType { get; set; }

        [JsonIgnore]
        public string TCOType_Str
        {
            get
            {
                return Enum.GetName(typeof(WATCHER_TYPE), TCOType);
            }
            set
            {
                if (Enum.TryParse(value, out WATCHER_TYPE _TCOType))
                {
                    TCOType = _TCOType;
                }

            }
        }
        public string TCOStaffID { get; set; }         //000182
        public  int  ConfirmType { get; set; }          //1
        public string EntryNetNo { get; set; }       //01
        public string EntryPlazaNo { get; set; }       //29
        public int CarClass { get; set; }           //12
        public int CarType { get; set; }               //1
        public string CarPlate { get; set; }          //439
        public bool DifPlaza { get; set; }     //0,入口站没有更改，1监控员更改入口站0
        public bool DifPlate { get; set; }      //0,入口输入错误,   1确实不符0
        public bool DifType { get; set; }      //0,车型没有更改，1,监控员修改车型0
        public bool DifKind { get; set; }      //0,车种没有更改  1,监控员修改车种0
        public int   UCar { get; set; }         //0,正常，1，U型；2：J型0
         public int    TimeoutCar { get; set; }  //0,正常；1，超时；2：超速0

        public string TransNo { get; set; }//00001
        public int WeightType { get; set; }//2
        public bool IsConfirm { get; set; }//1
        public int WeightLimit { get; set; }//046000
        public bool IsDiscountCard { get; set; }// 0
        public int WeightInput { get; set; }//000000
        public int AxleLastNo { get; set; }//0001
        public DateTime DateTime { get; set; }
        public string EntryPlazaName { get;  set; }
        public string EntryPlazaId { get;  set; }
        public string EntryPlazaHEX { get;  set; }
        public DateTime EntryDateTime { get; set; }
        public string EntryDHM { get; set; }
        public string EntryLaneID { get;  set; }
        public bool DifEntryDateTime { get;  set; }
    }
}
