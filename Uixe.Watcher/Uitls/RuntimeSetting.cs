using System;
using System.Reflection;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher
{
    public  class RuntimeSetting
    {
        internal  User NowCollect;

        internal  Dtos.Plaza Plaza { get; set; }

        public  RptLoginResult Token { get; set; }
        public  UserRole[] UserRole { get; set; }
 
    }
}