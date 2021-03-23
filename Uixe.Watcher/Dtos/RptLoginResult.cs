using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Dtos
{
    public class ApiResult<T>
    {
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }


        public T data { get; set; }

    }
    public class ProvCode
    {
        public int provId { get; set; }
        public string provName { get; set; }
    }
    public class ProvPlazaInfo
    {
        public string plazaId { get; set; }
        public string plazaName { get; set; }
        public string plazaHEX { get; set; }
        public string plazaNameInitials { get; set; }
    }

    public class UserRole
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
    }

    public class RptLoginResult
    {

        public DateTime LoginDateTime { get; set; } = DateTime.Now;
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
        public int expire { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
    }

}
