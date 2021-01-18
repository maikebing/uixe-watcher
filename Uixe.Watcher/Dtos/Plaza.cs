using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Dtos
{

    public class Plaza
    {
        public string id { get; set; }

        public string ip { get; set; }

        public List<Lane> lanes { get; set; }

        public string station_id { get; set; }

        public string station_name { get; set; }

        public string road_name { get; set; }
        public string road_id { get; set; }
    }

}
