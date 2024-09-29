using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoIamDtos
{
    public class Lane
    {
        /// <summary>
        ///
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string lane_id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string lane_no { get; set; }

    }
    public class Plaza
    {
        public string id { get; set; }
        public string agentIp { get; set; }
        public string ip { get; set; }

        public List<Lane> lanes { get; set; }

        public string station_id { get; set; }

        public string station_name { get; set; }

        public string road_name { get; set; }
        public string road_id { get; set; }
        public string vncpwd { get; set; }

    }
    public class whoiam
    {
        /// <summary>
        /// 自治区总中心1
        /// </summary>
        public string name { get; set; }
        public string pc_ip { get; set; }
        public List<Plaza> plazas { get; set; }
    }
}
