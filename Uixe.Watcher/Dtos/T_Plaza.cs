using System.Collections.Generic;

namespace Uixe.Watcher.Dtos
{
    public class T_Plaza
    {
        public string Id { get; set; }
        public PlazaType PlazaType { get; set; } = PlazaType.Plaza;

        public string Ip { get; set; }

        public string RoadId { get; set; }

        public string RoadName { get; set; }

        public string StationId { get; set; }

        public string StationName { get; set; }

        public string AgentIp { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        public List<T_Lane> Lanes { get; set; }

        public string VncPwd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

    }
}