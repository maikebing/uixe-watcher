using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher
{

    public enum PlaceType
    {
        Unknow,
        Root,
        SubCenter,
        Plaza,
        Lane
    }
    public class AppSettings
    {
        public PlaceType PlaceType { get;  set; }
        public string PlaceId { get; set; }
        public whoiam whoiam { get; set; }

    }
}
