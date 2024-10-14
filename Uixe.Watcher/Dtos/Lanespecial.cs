using System.Collections.Generic;

namespace Uixe.Watcher.Dtos
{
    public class Lanespecial
    {

        /******************************特情代码************************************************************/
        //OBU 特情类型(1-20)(十进制值)
        public const int SPECIAL_CODE_OBU_POWER_LOW = 1;//OBU 电量低 识别到 OBU（单片、双片）状态为低电状态
        public const string SPECIAL_CODE_OBU_POWER_LOW_STR = "OBU电量低";
        public const int SPECIAL_CODE_OBU_REMOVAL = 2;//OBU 拆卸 识别到 OBU（单片、双片）状态为拆卸状态（含预激活）
        public const string SPECIAL_CODE_OBU_REMOVAL_STR = "OBU拆卸";
        public const int SPECIAL_CODE_OBU_EXPIRED = 3;	//OBU 过期 识别到 OBU（单片、双片）有效期状态为过期状态
        public const string SPECIAL_CODE_OBU_EXPIRED_STR = "OBU过期";
        public const int SPECIAL_CODE_OBU_UNABLED = 4;	//OBU 未启用 识别到 OBU（单片、双片）有效期状态为未启用状态
        public const string SPECIAL_CODE_OBU_UNABLED_STR = "OBU未启用";
        public const int SPECIAL_CODE_OBU_NOCARD = 5;//OBU 无卡 识别到 OBU（双片）状态为未插卡状态
        public const string SPECIAL_CODE_OBU_NOCARD_STR = "OBU无卡";
        public const int SPECIAL_CODE_OBU_IN_STATUS_LIST = 6;	//OBU 在状态名单里 识别到 OBU（单片、双片）序号为状态名单，导致交易失败
        public const string SPECIAL_CODE_OBU_IN_STATUS_LIST_STR = "OBU在状态名单里";
        public const int SPECIAL_CODE_OBU_LOCKED = 7;	//OBU 已锁 识别到 OBU（单片、双片）状态为锁定状态
        public const string SPECIAL_CODE_OBU_LOCKED_STR = "OBU已锁";
        public const int SPECIAL_CODE_OBU_INVALID_ISSUER = 8;//OBU 发行方无效 识别到 OBU（单片、双片）发行方无效
        public const string SPECIAL_CODE_OBU_INVALID_ISSUER_STR = "OBU发行方无效";
        public const int SPECIAL_CODE_OBU_INVALID_VEHICLETYPE = 9;//OBU 车型不合法 识别到 OBU（单片、双片）车辆信息文件中的车型不合法
        public const string SPECIAL_CODE_OBU_INVALID_VEHICLETYPE_STR = "OBU车型不合法";
        public const int SPECIAL_CODE_OBU_INVALID_EF04_PREFIX = 10;//出口车道识别到 OBU（双片）EF04 文件中第 320－322 字节为 0xBB 0x290x00 或 0xCC 0x29 0x00 或其他非定义值
        public const string SPECIAL_CODE_OBU_INVALID_EF04_PREFIX_STR = "OBU EF04内前缀异常";
        public const int SPECIAL_CODE_OBU_INVALID_NO_CARD_TIMES = 11;//出口车道识别到 OBU（双片） EF04文件中标签无卡次数大于 0
        public const string SPECIAL_CODE_OBU_INVALID_NO_CARD_TIMES_STR = "签内无卡次数大于0";
        public const int SPECIAL_CODE_OBU_INVALID_ENTRYINFO = 12;//出口车道识别到 OBU（双片） EF04文件中入口信息无效（第 320－322字节为 0xAA 0x29 0x00 情况下）或者OBU（单片）中入口信息无效
        public const string SPECIAL_CODE_OBU_INVALID_ENTRYINFO_STR = "OBU 收费站入口无效";
        public const int SPECIAL_CODE_OBU_EF04_0_TRANSNUM = 13;	//识别到 OBU（双片） EF04 文件中累计通行省份数量为 0
        public const string SPECIAL_CODE_OBU_EF04_0_TRANSNUM_STR = "OBU EF04中累计通行省份为0";
        public const int SPECIAL_CODE_OBU_TOTALAMOUNT_BIG = 14;//识别到 OBU（双片） EF04 中总累计应收金额异常（超出指定阀值）
        public const string SPECIAL_CODE_OBU_TOTALAMOUNT_BIG_STR = "识别到OBU(双片)EF04中总累计应收金额异常";
        public const int SPECIAL_CODE_OBU_TOTALAMOUNT_SMALL = 15;	//识别到 OBU（双片） EF04 中总累计应收金额异常（小于指定阀值） 出口
        public const string SPECIAL_CODE_OBU_TOTALAMOUNT_SMALL_STR = "识别到OBU(双片)EF04中总累计应收金额异常(小于指定阀值)出口";

        //卡片特情类型(21-40)(十进制值)
        public const int SPECIAL_CODE_ETC_EXPIRED = 21;	//ETC 卡过期 识别到卡片有效期状态为过期状态
        public const string SPECIAL_CODE_ETC_EXPIRED_STR = "卡片过期";
        public const int SPECIAL_CODE_ETC_UNABLED = 22; //ETC 卡未启用 识别到卡片有效期状态为未启用状态
        public const string SPECIAL_CODE_ETC_UNABLED_STR = "卡片未启用";
        public const int SPECIAL_CODE_ETC_LOCKED = 23;//ETC 卡已锁 识别到卡片状态为锁定状态
        public const string SPECIAL_CODE_ETC_LOCKED_STR = "卡片已锁";
        public const int SPECIAL_CODE_ETC_IN_STATUS_LIST = 24;//ETC 卡在状态名单内 识别到 ETC 卡片为状态名单
        public const string SPECIAL_CODE_ETC_IN_STATUS_LIST_STR = "卡片状态名单";
        public const int SPECIAL_CODE_ETC_INVALID_ISSUER = 25;//ETC 卡发行方无效 识别到 ETC 卡发行方无效
        public const string SPECIAL_CODE_ETC_INVALID_ISSUER_STR = "卡片发行方无效";
        public const int SPECIAL_CODE_ETC_INVALID_ENTRY = 26;//ETC/CPC 卡入口无效 识别到 ETC/CPC 卡片中入口信息无效
        public const string SPECIAL_CODE_ETC_INVALID_ENTRY_STR = "通行卡入口无效";
        public const int SPECIAL_CODE_ETC_BALANCE_NOT_ENOUGH = 27;	//ETC 卡片储值卡余额不足识别到 ETC 卡片中储值卡余额不足
        public const string SPECIAL_CODE_ETC_BALANCE_NOT_ENOUGH_STR = "储值卡余额不足";
        public const int SPECIAL_CODE_ETC_BALANCE_0 = 28;//ETC 卡片余额为 0 识别到 ETC 储值卡卡片电子钱包余额或记账卡虚拟电子钱包余额为 0
        public const string SPECIAL_CODE_ETC_BALANCE_0_STR = "卡片余额为零";

        //卡签一致性特情类型(41－50)(十进制值)
        public const int SPECIAL_OBU_ETC_VEHICLE_ID_INCONFORMITY = 41; //ETC 卡与 OBU 车牌（含颜色）不符识别到 ETC 卡片与标签内车牌号（包含颜色）类型非法
        public const string SPECIAL_OBU_ETC_VEHICLE_ID_INCONFORMITY_STR = "卡签车牌不一致";
        public const int SPECIAL_OBU_ETC_VEHICLE_TYPE_INCONFORMITY = 42;  //ETC 卡与 OBU 车型不符 识别到 ETC 卡片与标签内车型类型
        public const string SPECIAL_OBU_ETC_VEHICLE_TYPE_INCONFORMITY_STR = "卡签车型不一致";
        public const int SPECIAL_OBU_ETC_ISSUER_INCONFORMITY = 43;//识别到 ETC 卡片与 OBU 发行方不符
        public const string SPECIAL_OBU_ETC_ISSUER_INCONFORMITY_STR = "卡签发行方不一致";
        public const int SPECIAL_OBU_ETC_EF04_INCONFORMITY = 44; //ETC 卡与 OBU EF04 内卡片文件不一致识别到 OBU EF04 中的卡片信息与卡片 0015 文件卡号不一致
        public const string SPECIAL_OBU_ETC_EF04_INCONFORMITY_STR = "卡签通行信息不一致";
        public const int SPECIAL_OBU_ETC_EF04_ENTRY_INCONFORMITY = 45;//ETC 卡与 OBU EF04 内入口信息不一致（入口站或者入口时间）识别到 OBU EF04 中入口信息与卡片0019 文件中入口信息不一致
        public const string SPECIAL_OBU_ETC_EF04_ENTRY_INCONFORMITY_STR = "卡签入口信息不一致";
        public const int SPECIAL_OBU_ETC_EF04_LASTGANTRYHEX_INCONFORMITY = 46; //ETC 卡与 OBU EF04 内上个门架编号信息不一致（ 门 架编 号或 通 行时间）识别到 OBU EF04 文件过站信息中的上个门架信息与卡片上个门架信息不一致
        public const string SPECIAL_OBU_ETC_EF04_LASTGANTRYHEX_INCONFORMITY_STR = "卡签内门架编号不一致";
        public const int SPECIAL_OBU_ETC_VHEICLE_ERROR_DIFF_INCONFORMITY = 47;  //卡签车种不一致
        public const string SPECIAL_OBU_ETC_VHEICLE_ERROR_DIFF_INCONFORMITY_STR = "卡签车种不一致";


        //入出口特情类型(51-60)(十进制值)
        public const int SPECIAL_ENTRY_EXITY_VEHICLE_TYPE_INCONFORMITY = 51;//出入口车辆车型不符 识别到与入口车型不符合
        public const string SPECIAL_ENTRY_EXITY_VEHICLE_TYPE_INCONFORMITY_STR = "出入口车辆车型不符";
        public const int SPECIAL_ENTRY_EXITY_VEHICLE_ID_INCONFORMITY = 52;//识别到与入口车牌（包含颜色）不符合
        public const string SPECIAL_ENTRY_EXITY_VEHICLE_ID_INCONFORMITY_STR = "出入口车辆车牌不符";
        public const int SPECIAL_ENTRY_EXITY_VEHICLE_STATUS_FLAG_INCONFORMITY = 53;//出入口车辆状态标识（货车 ETC）不符识别到货车ETC入口写入车辆状态标识不合法
        public const string SPECIAL_ENTRY_EXITY_VEHICLE_STATUS_FLAG_INCONFORMITY_STR = "出入口车辆状态标识不符";
        public const int SPECIAL_ENTRY_EXITY_TIME_ABNORMAL = 54;//入口时间异常 识别到入口时间异常，如大于当前时间、1970 年等
        public const string SPECIAL_ENTRY_EXITY_TIME_ABNORMAL_STR = "入口时间异常";
        public const int SPECIAL_ENTRY_EXITY_CIRCULATION_STATUS = 55;//入口流通状态有误（非0x01/0x03/0x10）识别到入口流通状态有误，参考0019文件的入出口状态字节
        public const string SPECIAL_ENTRY_EXITY_CIRCULATION_STATUS_STR = "入口流通状态有误";
        public const int SPECIAL_ENTRY_EXITY_READ_CARD_ERROR = 56;//入口标签无卡或读卡出错或入口储值卡余额为0识别到标签内EF04 0019文件前缀为0xBB 0x29 0x00
        public const string SPECIAL_ENTRY_EXITY_READ_CARD_ERROR_STR = "入口标签无卡或读卡出错或入口储值卡余额为0";
        public const int SPECIAL_ENTRY_EXITY_GANTRY_WRITE_ENTRY = 57;//门架代写入口 识别到标签内EF04 0019文件前缀为0xCC 0x29 0x00, 即表示门架检测到车辆非法或收费站入口车道出现异常情况进入驶入高速公路，会以第一个读取到的门架信息作为入口信息写入。
        public const string SPECIAL_ENTRY_EXITY_GANTRY_WRITE_ENTRY_STR = "门架代写入口";
        public const int SPECIAL_ENTRY_EXITY_DRIVING_TIMEOUT = 58;//从进入路网到离开路网超时（未拦截无需记录，即按正常交易看待）识别到当前时间到与入口时间相差超过阀值（省内自定义） 出
        public const string SPECIAL_ENTRY_EXITY_DRIVING_TIMEOUT_STR = "行驶超时";
        public const int SPECIAL_ENTRY_EXITY_PATH_NOT_REACHABLE = 59;//路径不可通达 识别入口站点到出口站点无通行路径
        public const string SPECIAL_ENTRY_EXITY_PATH_NOT_REACHABLE_STR = "路径不可通达";
        public const int SPECIAL_ENTRY_EXITY_AXLE_INCONFORMITY = 60;//入出口轴数不一致 识别到入出口轴数不一致
        public const string SPECIAL_ENTRY_EXITY_AXLE_INCONFORMITY_STR = "入出口轴数不一致";

        //计费特情类型（61－70）(十进制值)
        public const int SPECIAL_CALCFEE_INIT_FAILED = 61;//计费模块初始化失败 计费模块初始化异常导致计费失败
        public const string SPECIAL_CALCFEE_INIT_FAILED_STR = "计费模块初始化失败";

        public const int SPECIAL_CALCFEE_QUERY_FEE_FAILED = 63;//计费模块查询费率返回失败 调用计费模块计费接口返回报错
        public const string SPECIAL_CALCFEE_QUERY_FEE_FAILED_STR = "计费模块查询费率返回失败";

        //天线设备特情（71－90）(十进制值)
        public const int SPECIAL_RSU_RES_TIMEOUT = 71;  //无DSRC数据返回（超时）DSRC 下行帧发送后 100ms 内未收到OBU 回复数据
        public const string SPECIAL_RSU_RES_TIMEOUT_STR = "OBU交互超时";
        public const int SPECIAL_RSU_READ_OBU_CARINFO_FAILD = 72;//读取 OBU 车辆信息文件失败（双片式 OBU）RSU 对 OBU 进行车辆信息读取操作，OBU 执行指令失败
        public const string SPECIAL_RSU_READ_OBU_CARINFO_FAILD_STR = "读取OBU车辆信息文件失败";
        public const int SPECIAL_RSU_READ_ETC_CARINFO_FAILD = 74;//RSU 对 OBU 进行 ETC 卡片读取操作，OBU 执行指令失败
        public const string SPECIAL_RSU_READ_ETC_CARINFO_FAILD_STR = "读取 ETC 卡片文件失败(双片式OBU)";
        public const int SPECIAL_RSU_COMPOUND_CONSUM_FAILD = 77;//复合消费失败（双片式OBU）RSU 对 OBU 进行复合消费操作，OBU 执行指令失败
        public const string SPECIAL_RSU_COMPOUND_CONSUM_FAILD_STR = "RSU对OBU进行复合消费操作失败";
        public const int SPECIAL_RSU_GET_TAC_FAILD = 78;    //获取 TAC 码失败（双片式 OBU）RSU 对 OBU 进行重取 TAC 码操作，OBU 执行指令失败
        public const string SPECIAL_RSU_GET_TAC_FAILD_STR = "RSU对OBU进行取TAC码操作失败";
        public const int SPECIAL_RSU_READ_OBU_EF04_FAILD = 79;//读取 OBU EF04 文件失败（双片式 OBU）RSU 对 OBU 进行 EF04 文件读取操作，OBU 执行指令失败
        public const string SPECIAL_RSU_READ_OBU_EF04_FAILD_STR = "读取OBUEF04文件失败";

        //PSAM 卡特情（91 -100）(十进制值)
        public const int SPECIAL_PSAM_AUTHOR_EXCEPTION = 97;//PSAM 卡未授权 PSAM 卡授权失败
        public const string SPECIAL_PSAM_AUTHOR_EXCEPTION_STR = "PSAM卡授权失败";

        //其他特情（111－130）(十进制值)
        public const int SPECIAL_CODE_U = 111; //U 型车拦截（未拦截无需记录）识别车辆为 U 型车。（未拦截无需记录）
        public const string SPECIAL_CODE_U_STR = "U型车拦截";
        public const int SPECIAL_CODE_HOLIDAY = 116;//节假日
        public const string SPECIAL_CODE_HOLIDAY_STR = "节假日";
        public const int SPECIAL_CODE_GREENTRUCK = 128; //绿通车
        public const string SPECIAL_CODE_GREENTRUCK_STR = "绿通车";
        public const int SPECIAL_CODE_COMBINE = 132;//联合收割机
        public const string SPECIAL_CODE_COMBINE_STR = "联合收割机";


        //省内自定义特情（141 －150）(十进制值)
        /**********************************************************************************************************/
        public const int SPECIAL_CODE_RETRANS_SAME_STATION = 141;//同站重复交易
        public const string SPECIAL_CODE_RETRANS_SAME_STATION_STR = "同站重复交易";
        public const int SPECIAL_CODE_INVALID_USER_CARD = 142; //用户卡异常
        public const string SPECIAL_CODE_INVALID_USER_CARD_STR = "用户卡异常";
        public const int SPECIAL_CODE_INVALID_TRUCK_AXLE = 143; //货车轴数异常
        public const string SPECIAL_CODE_INVALID_TRUCK_AXLE_STR = "货车轴数异常";
        public const int SPECIAL_CODE_INVALID_TRUCK_CARD = 144;//货车卡异常
        public const string SPECIAL_CODE_INVALID_TRUCK_CARD_STR = "货车卡异常";
        public const int SPECIAL_CODE_STORED_CARD_DEDUCTION_FAILD = 145;//储值卡扣款失败
        public const string SPECIAL_CODE_STORED_CARD_DEDUCTION_FAILD_STR = "储值卡扣款失败";
        public const int SPECIAL_CODE_PLEASE_CAR_RETURN = 146;//劝返车辆
        public const string SPECIAL_CODE_PLEASE_CAR_RETURN_STR = "劝返车辆";
        public const int SPECIAL_CODE_INFO_ERROR = 147;//信息错误
        public const string SPECIAL_CODE_INFO_ERROR_STR = "信息错误";
        public const int SPECIAL_CODE_UNWEIGHT = 148;//未称重
        public const string SPECIAL_CODE_UNWEIGHT_STR = "未称重";
        public const int SPECIAL_CODE_STATUS_LIST_ABNORMAL = 149;//状态名单故障
        public const string SPECIAL_CODE_STATUS_LIST_ABNORMAL_STR = "状态名单故障";
        public const int SPECIAL_CODE_LANE_CLOSE = 150; //车道关闭
        public const string SPECIAL_CODE_LANE_CLOSE_STR = "车道关闭";
        public const int SPECIAL_CODE_TRUCK_OBU_ABNORMAL = 151; //货车签异常
        public const string SPECIAL_CODE_TRUCK_OBU_ABNORMAL_STR = "货车签异常";
        public const int SPECIAL_CODE_OVERLOAD = 152;//车辆超载
        public const string SPECIAL_CODE_OVERLOAD_STR = "车辆超载";
        public const int SPECIAL_CODE_IN_OVERLOAD_LIST = 153;//车辆在超载名单
        public const string SPECIAL_CODE_IN_OVERLOAD_LIST_STR = "车辆在超载名单";
        public const int SPECIAL_CODE_IN_BLACK_LIST_GUASHI = 154;//黑名单挂失
        public const string SPECIAL_CODE_IN_BLACK_LIST_GUASHI_STR = "黑名单挂失";
        public const int SPECIAL_CODE_IN_BLACK_LIST_JINYONG = 155;//黑名单禁用
        public const string SPECIAL_CODE_IN_BLACK_LIST_JINYONG_STR = "黑名单禁用";
        public const int SPECIAL_CODE_IN_BLACK_LIST_ZHUXIAO = 156;//黑名单注销
        public const string SPECIAL_CODE_IN_BLACK_LIST_ZHUXIAO_STR = "黑名单注销";
        public const int SPECIAL_CODE_IN_BLACK_LIST_TOUZHI = 157;//黑名单透支;
        public const string SPECIAL_CODE_IN_BLACK_LIST_TOUZHI_STR = "黑名单透支";
        public const int SPECIAL_CODE_IN_BLACK_LIST_ELSE = 158;//其他黑名单
        public const string SPECIAL_CODE_IN_BLACK_LIST_ELSE_STR = "其他黑名单";
        public const int SPECIAL_CODE_ETC_JUN_CHE = 159;    //军车
        public const string SPECIAL_CODE_ETC_JUN_CHE_STR = "军车";
        public const int SPECIAL_CODE_ETC_RESCUE = 160; //应急救援车
        public const string SPECIAL_CODE_ETC_RESCUE_STR = "应急救援车";
        public const int SPECIAL_CODE_QUEUE_ABNORMAL = 161;//队列异常
        public const string SPECIAL_CODE_QUEUE_ABNORMAL_STR = "队列异常";
        public const int SPECIAL_CODE_QWETOLL = 162;//出口追缴
        public const string SPECIAL_CODE_QWETOLL_STR = "出口追缴";
        public const int SPECIAL_CODE_USERCARD_SPECIAL = 163;//用户卡其他特情
        public const string SPECIAL_CODE_USERCARD_SPECIAL_STR = "用户卡其他特情";
        public const int SPECIAL_CODE_VehicleType_Axle_DIFF = 164;//车型和车轴不匹配
        public const string SPECIAL_CODE_VehicleType_Axle_DIFF_STR = "车型和车轴不匹配";
        public const int SPECIAL_CODE_FiveTransWaste = 165;//五分钟内已交易
        public const string SPECIAL_CODE_FiveTransWaste_STR = "五分钟内已交易";
        public const int SPECIAL_CODE_AmountHasError = 166;//金额异常
        public const string SPECIAL_CODE_AmountHasError_STR = "金额异常";
        public const int SPECIAL_CODE_ETCCalcFeeFaild = 167;
        public const string SPECIAL_CODE_ETCCalcFeeFaild_STR = "ETC计费失败";
        public const int SPECIAL_CODE_MutiProMinGuateeError = 168;
        public const string SPECIAL_CODE_MutiProMinGuateeError_STR = "跨省兜底计费失败";
        public const int SPECIAL_CODE_ProMinGuateeError = 169;
        public const string SPECIAL_CODE_ProMinGuateeError_STR = "省内兜底计费失败";
        public const int SPECIAL_CODE_MinGuateeError = 170;
        public const string SPECIAL_CODE_MinGuateeError_STR = "兜底计费失败无法确认跨省通行";
        public const int SPECIAL_CODE_FeeComPareError = 171;
        public const string SPECIAL_CODE_FeeComPareError_STR = "ETC费率比较时错误";
        public const int SPECIAL_CODE_ETCCalcFeeResError = 172;
        public const string SPECIAL_CODE_ETCCalcFeeResError_STR = "ETC计费结果指针为空";
        public const int SPECIAL_CODE_PLATE_NOT_MATCH_SPECIAL = 173;//车牌识别和卡内车牌不一致
        public const string SPECIAL_CODE_PLATE_NOT_MATCH_SPECIAL_STR = "车牌识别和卡内车牌不一致";

        /*专用于省内的SPECIAL_TYPE的特情类型定义,1-99*/
        public const int SPECIAL_TYPE_PRO_ONLINE_FAILD = 49;//省内在线计费失败
        public const int SPECIAL_TYPE_PRO_ONLINE_FEE0 = 50;//省内在线计费金额为0
        public const int SPECIAL_TYPE_PRO_ONLINE_SMALL = 51;//;省内在线计费金额小于兜底费额
        public const int SPECIAL_TYPE_PRO_ONLINE_BIG = 52;//省内在线计费金额大于兜底的倍率
        public const int SPECIAL_TYPE_MUTIPRO_ONLINE_FAILD = 53;//跨省在线计费失败
        public const int SPECIAL_TYPE_MUTIPRO_ONLINE_FEE0 = 54;//跨省在线计费金额为0
        public const int SPECIAL_TYPE_MUTIPRO_ONLINE_SMALL = 55;//跨省在线计费金额小于兜底费额
        public const int SPECIAL_TYPE_MUTIPRO_ONLINE_BIG = 56;//跨省在线计费金额大于兜底的倍率
        public const int SPECIAL_TYPE_CPC_CARDFILES_SMALL = 57;//CPC卡文件金额小于兜底费额
        public const int SPECIAL_TYPE_CPC_CARDFILES_BIG = 58;//CPC卡文件金额大于兜底费额
        public const int SPECIAL_TYPE_CPC_CARDFILES_OK = 59;//CPC卡文件计费正常
        public const int SPECIAL_TYPE_CPC_MINGUATEE_ERROR = 60;//兜底计费失败

        public const string  SPECIAL_TYPE_PRO_ONLINE_FAILD_STR = "省内在线计费失败";
        public const string SPECIAL_TYPE_PRO_ONLINE_FEE0_STR = "省内在线计费金额为0";
        public const string SPECIAL_TYPE_PRO_ONLINE_SMALL_STR = "省内在线计费金额小于兜底费额";
        public const string SPECIAL_TYPE_PRO_ONLINE_BIG_STR = "省内在线计费金额大于兜底的倍率";
        public const string SPECIAL_TYPE_MUTIPRO_ONLINE_FAILD_STR = "跨省在线计费失败";
        public const string SPECIAL_TYPE_MUTIPRO_ONLINE_FEE0_STR = "跨省在线计费金额为0";
        public const string SPECIAL_TYPE_MUTIPRO_ONLINE_SMALL_STR = "跨省在线计费金额小于兜底费额";
        public const string SPECIAL_TYPE_MUTIPRO_ONLINE_BIG_STR = "跨省在线计费金额大于兜底的倍率";
        public const string SPECIAL_TYPE_CPC_CARDFILES_SMALL_STR = "CPC卡文件金额小于兜底费额";
        public const string SPECIAL_TYPE_CPC_CARDFILES_BIG_STR = "CPC卡文件金额大于兜底费额";
        public const string SPECIAL_TYPE_CPC_CARDFILES_OK_STR = "CPC卡文件计费正常";
        public const string SPECIAL_TYPE_CPC_MINGUATEE_ERROR_STR = "兜底计费失败";


        public const int SPECIAL_CODE_Backing_Weight = 200;//秤台倒车
       public const int SPECIAL_CODE_Backing_Coil = 201;//线圈倒车
       public const int SPECIAL_CODE_Change_Lane = 202;//换道
       public const int SPECIAL_CODE_NotFound_OBU = 203;//未检测到OBU



        //用于闯道车辆的特情 
        public const int SPECIAL_CODE_FeeEvasion = 220;//未知原因闯关
      public const int SPECIAL_CODE_FeeEvasion_REVERSING = 221;//倒车原因闯关
        public const int SPECIAL_CODE_FeeEvasion_WOODTRUCK = 222;//长车原因闯关
        public const int SPECIAL_CODE_FeeEvasion_VIOLATION = 223;//一般违章原因闯关
        public const int SPECIAL_CODE_FeeEvasion_ALARMWITHOUTREASON = 224;//误报警原因闯关
        public const int SPECIAL_CODE_FeeEvasion_USERINHURRY = 225;//用户匆忙原因闯关
        public const int SPECIAL_CODE_FeeEvasion_TOWINGVEHICING = 226;//被拖车原因闯关

        static Dictionary<int, string> pairs = null;
        public static Dictionary<int, string> lanespecal 
        {
            get
            {
                if (pairs == null)
                {
                    pairs=new Dictionary<int, string>(175);
                    pairs[SPECIAL_CODE_OBU_POWER_LOW] = SPECIAL_CODE_OBU_POWER_LOW_STR;
                    pairs[SPECIAL_CODE_OBU_REMOVAL] = SPECIAL_CODE_OBU_REMOVAL_STR;
                    pairs[SPECIAL_CODE_OBU_EXPIRED] = SPECIAL_CODE_OBU_EXPIRED_STR;
                    pairs[SPECIAL_CODE_OBU_UNABLED] = SPECIAL_CODE_OBU_UNABLED_STR;
                    pairs[SPECIAL_CODE_OBU_NOCARD] = SPECIAL_CODE_OBU_NOCARD_STR;
                    pairs[SPECIAL_CODE_OBU_IN_STATUS_LIST] = SPECIAL_CODE_OBU_IN_STATUS_LIST_STR;
                    pairs[SPECIAL_CODE_OBU_LOCKED] = SPECIAL_CODE_OBU_LOCKED_STR;
                    pairs[SPECIAL_CODE_OBU_INVALID_ISSUER] = SPECIAL_CODE_OBU_INVALID_ISSUER_STR;
                    pairs[SPECIAL_CODE_OBU_INVALID_VEHICLETYPE] = SPECIAL_CODE_OBU_INVALID_VEHICLETYPE_STR;
                    pairs[SPECIAL_CODE_OBU_INVALID_EF04_PREFIX] = SPECIAL_CODE_OBU_INVALID_EF04_PREFIX_STR;
                    pairs[SPECIAL_CODE_OBU_INVALID_NO_CARD_TIMES] = SPECIAL_CODE_OBU_INVALID_NO_CARD_TIMES_STR;
                    pairs[SPECIAL_CODE_OBU_INVALID_ENTRYINFO] = SPECIAL_CODE_OBU_INVALID_ENTRYINFO_STR;
                    pairs[SPECIAL_CODE_OBU_EF04_0_TRANSNUM] = SPECIAL_CODE_OBU_EF04_0_TRANSNUM_STR;
                    pairs[SPECIAL_CODE_OBU_TOTALAMOUNT_BIG] = SPECIAL_CODE_OBU_TOTALAMOUNT_BIG_STR;
                    pairs[SPECIAL_CODE_OBU_TOTALAMOUNT_SMALL] = SPECIAL_CODE_OBU_TOTALAMOUNT_SMALL_STR;
                    pairs[SPECIAL_CODE_ETC_EXPIRED] = SPECIAL_CODE_ETC_EXPIRED_STR;
                    pairs[SPECIAL_CODE_ETC_UNABLED] = SPECIAL_CODE_ETC_UNABLED_STR;
                    pairs[SPECIAL_CODE_ETC_LOCKED] = SPECIAL_CODE_ETC_LOCKED_STR;
                    pairs[SPECIAL_CODE_ETC_IN_STATUS_LIST] = SPECIAL_CODE_ETC_IN_STATUS_LIST_STR;
                    pairs[SPECIAL_CODE_ETC_INVALID_ISSUER] = SPECIAL_CODE_ETC_INVALID_ISSUER_STR;
                    pairs[SPECIAL_CODE_ETC_INVALID_ENTRY] = SPECIAL_CODE_ETC_INVALID_ENTRY_STR;
                    pairs[SPECIAL_CODE_ETC_BALANCE_NOT_ENOUGH] = SPECIAL_CODE_ETC_BALANCE_NOT_ENOUGH_STR;
                    pairs[SPECIAL_CODE_ETC_BALANCE_0] = SPECIAL_CODE_ETC_BALANCE_0_STR;
                    pairs[SPECIAL_OBU_ETC_VEHICLE_ID_INCONFORMITY] = SPECIAL_OBU_ETC_VEHICLE_ID_INCONFORMITY_STR;
                    pairs[SPECIAL_OBU_ETC_VEHICLE_TYPE_INCONFORMITY] = SPECIAL_OBU_ETC_VEHICLE_TYPE_INCONFORMITY_STR;
                    pairs[SPECIAL_OBU_ETC_ISSUER_INCONFORMITY] = SPECIAL_OBU_ETC_ISSUER_INCONFORMITY_STR;
                    pairs[SPECIAL_OBU_ETC_EF04_INCONFORMITY] = SPECIAL_OBU_ETC_EF04_INCONFORMITY_STR;
                    pairs[SPECIAL_OBU_ETC_EF04_ENTRY_INCONFORMITY] = SPECIAL_OBU_ETC_EF04_ENTRY_INCONFORMITY_STR;
                    pairs[SPECIAL_OBU_ETC_EF04_LASTGANTRYHEX_INCONFORMITY] = SPECIAL_OBU_ETC_EF04_LASTGANTRYHEX_INCONFORMITY_STR;
                    pairs[SPECIAL_OBU_ETC_VHEICLE_ERROR_DIFF_INCONFORMITY] = SPECIAL_OBU_ETC_VHEICLE_ERROR_DIFF_INCONFORMITY_STR;
                    pairs[SPECIAL_ENTRY_EXITY_VEHICLE_TYPE_INCONFORMITY] = SPECIAL_ENTRY_EXITY_VEHICLE_TYPE_INCONFORMITY_STR;
                    pairs[SPECIAL_ENTRY_EXITY_VEHICLE_ID_INCONFORMITY] = SPECIAL_ENTRY_EXITY_VEHICLE_ID_INCONFORMITY_STR;
                    pairs[SPECIAL_ENTRY_EXITY_VEHICLE_STATUS_FLAG_INCONFORMITY] = SPECIAL_ENTRY_EXITY_VEHICLE_STATUS_FLAG_INCONFORMITY_STR;
                    pairs[SPECIAL_ENTRY_EXITY_TIME_ABNORMAL] = SPECIAL_ENTRY_EXITY_TIME_ABNORMAL_STR;
                    pairs[SPECIAL_ENTRY_EXITY_CIRCULATION_STATUS] = SPECIAL_ENTRY_EXITY_CIRCULATION_STATUS_STR;
                    pairs[SPECIAL_ENTRY_EXITY_READ_CARD_ERROR] = SPECIAL_ENTRY_EXITY_READ_CARD_ERROR_STR;
                    pairs[SPECIAL_ENTRY_EXITY_GANTRY_WRITE_ENTRY] = SPECIAL_ENTRY_EXITY_GANTRY_WRITE_ENTRY_STR;
                    pairs[SPECIAL_ENTRY_EXITY_DRIVING_TIMEOUT] = SPECIAL_ENTRY_EXITY_DRIVING_TIMEOUT_STR;
                    pairs[SPECIAL_ENTRY_EXITY_PATH_NOT_REACHABLE] = SPECIAL_ENTRY_EXITY_PATH_NOT_REACHABLE_STR;
                    pairs[SPECIAL_ENTRY_EXITY_AXLE_INCONFORMITY] = SPECIAL_ENTRY_EXITY_AXLE_INCONFORMITY_STR;
                    pairs[SPECIAL_CALCFEE_INIT_FAILED] = SPECIAL_CALCFEE_INIT_FAILED_STR;
                    pairs[SPECIAL_CALCFEE_QUERY_FEE_FAILED] = SPECIAL_CALCFEE_QUERY_FEE_FAILED_STR;
                    pairs[SPECIAL_RSU_RES_TIMEOUT] = SPECIAL_RSU_RES_TIMEOUT_STR;
                    pairs[SPECIAL_RSU_READ_OBU_CARINFO_FAILD] = SPECIAL_RSU_READ_OBU_CARINFO_FAILD_STR;
                    pairs[SPECIAL_RSU_READ_ETC_CARINFO_FAILD] = SPECIAL_RSU_READ_ETC_CARINFO_FAILD_STR;
                    pairs[SPECIAL_RSU_COMPOUND_CONSUM_FAILD] = SPECIAL_RSU_COMPOUND_CONSUM_FAILD_STR;
                    pairs[SPECIAL_RSU_GET_TAC_FAILD] = SPECIAL_RSU_GET_TAC_FAILD_STR;
                    pairs[SPECIAL_RSU_READ_OBU_EF04_FAILD] = SPECIAL_RSU_READ_OBU_EF04_FAILD_STR;
                    pairs[SPECIAL_PSAM_AUTHOR_EXCEPTION] = SPECIAL_PSAM_AUTHOR_EXCEPTION_STR;
                    pairs[SPECIAL_CODE_U] = SPECIAL_CODE_U_STR;
                    pairs[SPECIAL_CODE_HOLIDAY] = SPECIAL_CODE_HOLIDAY_STR;
                    pairs[SPECIAL_CODE_GREENTRUCK] = SPECIAL_CODE_GREENTRUCK_STR;
                    pairs[SPECIAL_CODE_COMBINE] = SPECIAL_CODE_COMBINE_STR;
                    pairs[SPECIAL_CODE_RETRANS_SAME_STATION] = SPECIAL_CODE_RETRANS_SAME_STATION_STR;
                    pairs[SPECIAL_CODE_INVALID_USER_CARD] = SPECIAL_CODE_INVALID_USER_CARD_STR;
                    pairs[SPECIAL_CODE_INVALID_TRUCK_AXLE] = SPECIAL_CODE_INVALID_TRUCK_AXLE_STR;
                    pairs[SPECIAL_CODE_INVALID_TRUCK_CARD] = SPECIAL_CODE_INVALID_TRUCK_CARD_STR;
                    pairs[SPECIAL_CODE_STORED_CARD_DEDUCTION_FAILD] = SPECIAL_CODE_STORED_CARD_DEDUCTION_FAILD_STR;
                    pairs[SPECIAL_CODE_PLEASE_CAR_RETURN] = SPECIAL_CODE_PLEASE_CAR_RETURN_STR;
                    pairs[SPECIAL_CODE_INFO_ERROR] = SPECIAL_CODE_INFO_ERROR_STR;
                    pairs[SPECIAL_CODE_UNWEIGHT] = SPECIAL_CODE_UNWEIGHT_STR;
                    pairs[SPECIAL_CODE_STATUS_LIST_ABNORMAL] = SPECIAL_CODE_STATUS_LIST_ABNORMAL_STR;
                    pairs[SPECIAL_CODE_LANE_CLOSE] = SPECIAL_CODE_LANE_CLOSE_STR;
                    pairs[SPECIAL_CODE_TRUCK_OBU_ABNORMAL] = SPECIAL_CODE_TRUCK_OBU_ABNORMAL_STR;
                    pairs[SPECIAL_CODE_OVERLOAD] = SPECIAL_CODE_OVERLOAD_STR;
                    pairs[SPECIAL_CODE_IN_OVERLOAD_LIST] = SPECIAL_CODE_IN_OVERLOAD_LIST_STR;
                    pairs[SPECIAL_CODE_IN_BLACK_LIST_GUASHI] = SPECIAL_CODE_IN_BLACK_LIST_GUASHI_STR;
                    pairs[SPECIAL_CODE_IN_BLACK_LIST_JINYONG] = SPECIAL_CODE_IN_BLACK_LIST_JINYONG_STR;
                    pairs[SPECIAL_CODE_IN_BLACK_LIST_ZHUXIAO] = SPECIAL_CODE_IN_BLACK_LIST_ZHUXIAO_STR;
                    pairs[SPECIAL_CODE_IN_BLACK_LIST_TOUZHI] = SPECIAL_CODE_IN_BLACK_LIST_TOUZHI_STR;
                    pairs[SPECIAL_CODE_IN_BLACK_LIST_ELSE] = SPECIAL_CODE_IN_BLACK_LIST_ELSE_STR;
                    pairs[SPECIAL_CODE_ETC_JUN_CHE] = SPECIAL_CODE_ETC_JUN_CHE_STR;
                    pairs[SPECIAL_CODE_ETC_RESCUE] = SPECIAL_CODE_ETC_RESCUE_STR;
                    pairs[SPECIAL_CODE_QUEUE_ABNORMAL] = SPECIAL_CODE_QUEUE_ABNORMAL_STR;
                    pairs[SPECIAL_CODE_QWETOLL] = SPECIAL_CODE_QWETOLL_STR;
                    pairs[SPECIAL_CODE_USERCARD_SPECIAL] = SPECIAL_CODE_USERCARD_SPECIAL_STR;
                    pairs[SPECIAL_CODE_VehicleType_Axle_DIFF] = SPECIAL_CODE_VehicleType_Axle_DIFF_STR;
                    pairs[SPECIAL_CODE_FiveTransWaste] = SPECIAL_CODE_FiveTransWaste_STR;
                    pairs[SPECIAL_CODE_AmountHasError] = SPECIAL_CODE_AmountHasError_STR;
                    pairs[SPECIAL_CODE_ETCCalcFeeFaild] = SPECIAL_CODE_ETCCalcFeeFaild_STR;
                    pairs[SPECIAL_CODE_MutiProMinGuateeError] = SPECIAL_CODE_MutiProMinGuateeError_STR;
                    pairs[SPECIAL_CODE_ProMinGuateeError] = SPECIAL_CODE_ProMinGuateeError_STR;
                    pairs[SPECIAL_CODE_MinGuateeError] = SPECIAL_CODE_MinGuateeError_STR;
                    pairs[SPECIAL_CODE_FeeComPareError] = SPECIAL_CODE_FeeComPareError_STR;
                    pairs[SPECIAL_CODE_ETCCalcFeeResError] = SPECIAL_CODE_ETCCalcFeeResError_STR;
                    pairs[SPECIAL_CODE_PLATE_NOT_MATCH_SPECIAL] = SPECIAL_CODE_PLATE_NOT_MATCH_SPECIAL_STR;
                    pairs[SPECIAL_TYPE_PRO_ONLINE_FAILD] = SPECIAL_TYPE_PRO_ONLINE_FAILD_STR;
                    pairs[SPECIAL_TYPE_PRO_ONLINE_FEE0] = SPECIAL_TYPE_PRO_ONLINE_FEE0_STR;
                    pairs[SPECIAL_TYPE_PRO_ONLINE_SMALL] = SPECIAL_TYPE_PRO_ONLINE_SMALL_STR;
                    pairs[SPECIAL_TYPE_PRO_ONLINE_BIG] = SPECIAL_TYPE_PRO_ONLINE_BIG_STR;
                    pairs[SPECIAL_TYPE_MUTIPRO_ONLINE_FAILD] = SPECIAL_TYPE_MUTIPRO_ONLINE_FAILD_STR;
                    pairs[SPECIAL_TYPE_MUTIPRO_ONLINE_FEE0] = SPECIAL_TYPE_MUTIPRO_ONLINE_FEE0_STR;
                    pairs[SPECIAL_TYPE_MUTIPRO_ONLINE_SMALL] = SPECIAL_TYPE_MUTIPRO_ONLINE_SMALL_STR;
                    pairs[SPECIAL_TYPE_MUTIPRO_ONLINE_BIG] = SPECIAL_TYPE_MUTIPRO_ONLINE_BIG_STR;
                    pairs[SPECIAL_TYPE_CPC_CARDFILES_SMALL] = SPECIAL_TYPE_CPC_CARDFILES_SMALL_STR;
                    pairs[SPECIAL_TYPE_CPC_CARDFILES_BIG] = SPECIAL_TYPE_CPC_CARDFILES_BIG_STR;
                    pairs[SPECIAL_TYPE_CPC_CARDFILES_OK] = SPECIAL_TYPE_CPC_CARDFILES_OK_STR;
                    pairs[SPECIAL_TYPE_CPC_MINGUATEE_ERROR] = SPECIAL_TYPE_CPC_MINGUATEE_ERROR_STR;
                    pairs[SPECIAL_CODE_Backing_Weight] = "秤台倒车";
                    pairs[SPECIAL_CODE_Backing_Coil] = "线圈倒车";
                    pairs[SPECIAL_CODE_Change_Lane] = "换道";
                    pairs[SPECIAL_CODE_NotFound_OBU] = "未检测到OBU";
                    pairs[SPECIAL_CODE_FeeEvasion] = "未知原因闯关";
                    pairs[SPECIAL_CODE_FeeEvasion_REVERSING] = "倒车原因闯关";
                    pairs[SPECIAL_CODE_FeeEvasion_WOODTRUCK] = "长车原因闯关";
                    pairs[SPECIAL_CODE_FeeEvasion_VIOLATION] = "一般违章原因闯关";
                    pairs[SPECIAL_CODE_FeeEvasion_ALARMWITHOUTREASON] = "误报警原因闯关";
                    pairs[SPECIAL_CODE_FeeEvasion_USERINHURRY] = "用户匆忙原因闯关";
                    pairs[SPECIAL_CODE_FeeEvasion_TOWINGVEHICING] = "被拖车原因闯关";


    }
                return pairs;
            }

        } 

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
        public MsgT_Lanespecial_Waste MsgT_Lanespecial_Waste { get; set; }

        public string Title => $"{Head.LaneID} {MsgT_Lanespecial_Waste.VEHICLE_ID} {lanespecal[int.Parse(MsgT_Lanespecial_Waste.SPECIAL_TYPE)]}";
        public string Context => $"{MsgT_Lanespecial_Waste.SPECIAL_DESC}";
    }

}