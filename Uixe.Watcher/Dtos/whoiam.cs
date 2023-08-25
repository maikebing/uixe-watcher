using System.Collections.Generic;

namespace Uixe.Watcher.Dtos
{
#pragma warning disable CS8981 // 该类型名称仅包含小写 ascii 字符。此类名称可能会成为该语言的保留值。
    public class whoiam
#pragma warning restore CS8981 // 该类型名称仅包含小写 ascii 字符。此类名称可能会成为该语言的保留值。
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