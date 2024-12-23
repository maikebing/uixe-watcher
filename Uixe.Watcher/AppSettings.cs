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
        public T_Boss whoiam { get; set; }
        public string Ring { get; set; }
        public string SkinStyle { get; set; }
        public bool SpeedBlackListPlate { get; set; } = true;
        public bool SpeechLvSeTongDao { get; set; } = true;
        public bool Only6769 { get; set; } = false;
        public bool Lanespecial { get; set; } = true;
        public bool CanPlayVideo { get; set; } = true;
        public bool laneVideoMute { get; set; } = false;
        public string LaneBossServer { get;  set; }
    }
}
