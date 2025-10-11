using DevExpress.CodeParser;
using DevExpress.Data.ODataLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Dtos
{
    public class EnStations
    {
        /// <summary>
        /// 
        /// </summary>
        public string cardId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string enStationId { get; set; }
    
        public string enTime
        {
            get
            {

                return enDateTime.ToString("yyyy-MM-ddTHH:mm:ss");
            }

            set
            {
                if (DateTime.TryParseExact(enTime, "yyyy-MM-ddTHH:mm:ss", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime _value))
                {
                    enDateTime = _value;
                }
                else
                {
                    enDateTime = DateTime.Now.AddDays(-1);
                }
            }
        }

        public DateTime enDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string enTollLaneId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mediaNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int mediaType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int resultVoucher { get; set; }
    }
    public class ConfirmEnInfo
    {
        /// <summary>
        /// 车道号
        /// </summary>
        public string laneId { get; set; }

        public string plazaId => laneId?.Substring(0, 7);
        public string laneNo => laneId?.Substring(7, 3);


        public string genTime { get; set; }

        public string vehicleId { get; set; }

        public int vehicleType { get; set; }

        public int resCount { get; set; }

        public int retQuery { get; set; }
        public int code { get; set; }
        public string msg { get; set; }

        public List<EnStations> enStations { get; set; } = new List<EnStations>() { };
    }

}
