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

        internal  Dtos.T_Plaza Plaza { get; set; }

        internal Dtos.T_Boss  Boss { get; set; }

        public  RptLoginResult Token { get; set; }
        public  UserRole[] UserRole { get; set; }
       
        public int KeyboardPort { get; set; } = 8080;
        public string serialNum { get; set; }
    }
}