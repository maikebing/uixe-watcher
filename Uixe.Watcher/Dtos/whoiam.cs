using System.Collections.Generic;

namespace Uixe.Watcher.Dtos
{
    public class whoiam
    {
        /// <summary>
        /// 自治区总中心1
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Plaza> plazas { get; set; }
    }
}