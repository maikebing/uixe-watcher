using System.Collections.Generic;

namespace Uixe.Watcher.Dtos
{
    public enum PlazaType
    {
        None = 0,
        /// <summary>
        /// 普通站
        /// </summary>
        Plaza ,
        /// <summary>
        /// 分公司的中心
        /// </summary>
        BranchCenter, 
        /// <summary>
        /// 省中心
        /// </summary>
        ProvCenter,
        /// <summary>
        /// 集约化主管收费站
        /// </summary>
        Supervisor 
    }
#pragma warning disable CS8981 // 该类型名称仅包含小写 ascii 字符。此类名称可能会成为该语言的保留值。
    public class T_Boss
#pragma warning restore CS8981 // 该类型名称仅包含小写 ascii 字符。此类名称可能会成为该语言的保留值。
    {

        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 自治区总中心1
        /// </summary>
        public string Name { get; set; }

        public string AgentIp { get; set; }
        /// <summary>
        /// 站类型 
        /// </summary>
        public PlazaType PlazaType { get; set; } = PlazaType.None;
 
        public List<T_Plaza> Plazas { get; set; }
    }
}