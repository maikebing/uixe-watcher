using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Dtos
{

    public class Plaza
    {
        /// <summary>
        /// 
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Lane> lanes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plaza_id { get; set; }
        /// <summary>
        /// 新疆木垒匝道站
        /// </summary>
        public string plaza_name { get; set; }
        /// <summary>
        /// G7奇木高速
        /// </summary>
        public string road_name { get; set; }
    }

}
