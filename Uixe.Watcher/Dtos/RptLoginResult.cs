using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Dtos
{
    public class RptLoginResult
    {
        /// <summary>
        /// 登录成功！
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime expire { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
    }

}
