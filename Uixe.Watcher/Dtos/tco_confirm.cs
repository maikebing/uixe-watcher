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
   
    public enum WATCHER_TYPE:int 
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

}
