using System;
using System.Reflection;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher
{
    public  class RuntimeSetting
    {
        public RuntimeSetting() {
            serialNum=Guid.NewGuid().ToString("N").ToLower();
        }
        internal  User NowCollect;

        internal  Dtos.Plaza Plaza { get; set; }

        public  RptLoginResult Token { get; set; }
        public  UserRole[] UserRole { get; set; }
        public string Ring { get;  set; }
        public string SkinStyle { get;  set; }
        public bool SpeedBlackListPlate { get; set; } = true;
        public bool SpeechLvSeTongDao { get; set; } = true;
        public int KeyboardPort { get; set; } = 8080;
        public string serialNum { get; set; }

    }
}