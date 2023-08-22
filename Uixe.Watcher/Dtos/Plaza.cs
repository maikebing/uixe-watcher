using System.Collections.Generic;

namespace Uixe.Watcher.Dtos
{
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
        public string vnc_password { get; set; }

    }
}