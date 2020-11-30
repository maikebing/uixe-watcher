using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uixe.Watcher
{
    public class FreshAgriProducts
    {
        /// <summary>
        ///
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 新鲜蔬菜
        /// </summary>
        public string Catalog { get; set; }

        /// <summary>
        /// 白菜类
        /// </summary>
        public string Kinds { get; set; }

        /// <summary>
        /// 大白菜、普通白菜（油菜、小青菜）、菜薹
        /// </summary>
        public string Example { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Display { get { return $"{Catalog}-{Kinds}"; } }
    }

    public class FAP
    {
        /// <summary>
        ///
        /// </summary>
        public List<FreshAgriProducts> FreshAgriProducts { get; set; }

        private static FAP _fap = null;

        public static FAP GetFreshAgriProducts
        {
            get
            {
                if (_fap == null) _fap = Newtonsoft.Json.JsonConvert.DeserializeObject<FAP>(System.Text.Encoding.GetEncoding(936).GetString(Properties.Resources.FreshAgriProducts));
                return _fap;
            }
        }
    }
}