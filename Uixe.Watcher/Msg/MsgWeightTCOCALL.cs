using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Uixe.Watcher.Msg
{

    public class MsgWeightTCOCALL
    {


        public string ID { get; set; }

        public string MsgType { get; set; }

        public string LaneType { get; set; }


        public string LaneMode { get; set; }
        public string Network { get; set; }
        public string Plaza { get; set; }



        public DateTime YMDHM { get; set; }


        public string LaneNo { get; set; }

        public string Collector { get; set; }

        public string CallType { get; set; }
        public string WeightFunctions { get; set; }
        public string FareFormula { get; set; }
        public string WeightType { get; set; }
        public string WeightCarKind { get; set; }
        public string CarType { get; set; }
        public int CarType_INT { get { return Int32.Parse(CarType); } set { CarType = value.ToString(); } }

        public string CarKind { get; set; }
        public int CarKind_INT { get { return Int32.Parse(CarKind); } set { CarKind = value.ToString(); } }

        public string TollFareDistance { get; set; }
        public string CarPlate { get; set; }

        public string CarAlex { get; set; }
        public string CarDevWeight { get; set; }

        public string CarSpeed { get; set; }
        public string OverLoadWeight { get; set; }
        public string OverLoadWeightRate { get; set; }
        public float Money { get; set; }
        public int TimeOut { get; set; }
        public string Text { get; set; }
        public string TCO { get; set; }
        public string WeightLimit { get; set; }

        public int TranID { get; set; }

    }
}