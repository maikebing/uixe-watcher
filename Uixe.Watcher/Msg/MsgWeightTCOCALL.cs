using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.Msg
{

    public class MsgWeightTCOCALL : tco_confirm
    {
        public MsgWeightTCOCALL() : base()
        {
            base.MsgTcoTran = new MsgTcoTran();
        }
    
        public static MsgWeightTCOCALL Parse(string json)
        {
            var ls = JsonConvert.DeserializeObject<MsgWeightTCOCALL>(json, new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            return ls;
        }

        public string ID => $"650{Head.NetNo}{Head.PlazaNo}{ Head.LaneID}";

        public string MsgType => Head.MsgType;

        public string LaneType => $"{Head.LaneType}";


        public string LaneMode => $"{SubHead.LaneMode}";
        public string Network => Head.NetNo;
        public string Plaza => Head.PlazaNo;



        public DateTime YMDHM => DateTime.TryParseExact(Head.DDHM, "yyyyMMDDHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dt) ? dt : DateTime.MinValue;



        public string LaneNo => Head.LaneID;

        public string Collector => SubHead.StaffID;

        public WATCHER_TYPE CallType => base.WatcherID ;

        public string WeightFunctions { get; set; }
        public string FareFormula { get; set; }
        public string WeightType => base.MsgTcoTran.WeightType;
        public string WeightCarKind => MsgTcoTran.ExitVehiKind;
        public string CarType => MsgTcoTran.CarClass;


        public int CarType_INT
        {
            get
            {

                int ct = 0;
                Int32.TryParse(CarType, out ct);
                return ct;
            }
        }

        public string CarKind => MsgTcoTran.ExitVehiKind;
        public int CarKind_INT
        {
            get
            {

                int ct = 0;
                Int32.TryParse(CarKind, out ct);
                return ct;
            }
        }

        public string TollFareDistance => MsgTcoTran.Distance;
        public string CarPlate => MsgTcoTran.ExitPlate;
        public string CarShortPlate => MsgTcoTran.InputPlate;

        public string CarAlex => MsgTcoTran.DetectAxleCount;
        public string CarDevWeight => MsgTcoTran.DetectWeightTotal;

        public string CarSpeed => MsgTcoTran.Speed;
        public string OverLoadWeight => MsgTcoTran.OverloadWeight;
        [Obsolete("无转换")]
        public string OverLoadWeightRate => MsgTcoTran.OverloadReason;

        [Obsolete("无转换")]
        public float Money { get; set; }

        public int TimeOut { get; set; }

        public string CarFareWeight => MsgTcoTran.FWeightTotal;
        public string Text => MsgTcoTran.Detail?.Trim()?.Replace("^r^n", "\r\n");
        public string TCO => MsgTcoTran.Detail?.Trim()?.Replace("^r^n", "\r\n");
        public string WeightLimit => MsgTcoTran.WeightLimit;

        public int TranID => int.Parse(MsgTcoTran.TransNO);
        
    }
}