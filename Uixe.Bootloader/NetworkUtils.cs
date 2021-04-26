using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Bootloader
{
    class NetworkUtils
    {
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        private static bool Fun_InternetGetConnectedState()
        {
            int INTERNET_CONNECTION_MODEM = 1;
            int INTERNET_CONNECTION_LAN = 2;
            int INTERNET_CONNECTION_PROXY = 4;
            int INTERNET_CONNECTION_MODEM_BUSY = 8;
            bool online = false;
            string outPut = null;
            Int32 flags = new int();//上网方式 
            bool m_bOnline = true;//是否在线 

            m_bOnline = InternetGetConnectedState(ref flags, 0);
            if (m_bOnline)//在线   
            {
                if ((flags & INTERNET_CONNECTION_MODEM) == INTERNET_CONNECTION_MODEM)
                {
                    online = true;
                    outPut = "在线：拨号上网\n";
                }
                if ((flags & INTERNET_CONNECTION_LAN) == INTERNET_CONNECTION_LAN)
                {
                    online = true;
                    outPut = "在线：通过局域网\n";
                }
                if ((flags & INTERNET_CONNECTION_PROXY) == INTERNET_CONNECTION_PROXY)
                {
                    online = true;
                    outPut = "在线：代理\n";
                }
                if ((flags & INTERNET_CONNECTION_MODEM_BUSY) == INTERNET_CONNECTION_MODEM_BUSY)
                {

                    outPut = "MODEM被其他非INTERNET连接占用\n";
                }
            }
            else
            {
                outPut = "不在线\n";
            }
            Console.WriteLine(outPut);
            return online ;
        }


        [DllImport("sensapi.dll")]
        private extern static bool IsNetworkAlive(out int connectionDescription);

        private static bool Fun_IsNetworkAlive()
        {
            int NETWORK_ALIVE_LAN = 0;
            int NETWORK_ALIVE_WAN = 2;
            int NETWORK_ALIVE_AOL = 4;

            string outPut = null;
            int flags;//上网方式 
            bool m_bOnline = false;//是否在线 

            m_bOnline = IsNetworkAlive(out flags);
            if (m_bOnline)//在线   
            {
                if ((flags & NETWORK_ALIVE_LAN) == NETWORK_ALIVE_LAN)
                {
                    outPut = "在线：NETWORK_ALIVE_LAN\n";
                }
                if ((flags & NETWORK_ALIVE_WAN) == NETWORK_ALIVE_WAN)
                {
                    outPut = "在线：NETWORK_ALIVE_WAN\n";
                }
                if ((flags & NETWORK_ALIVE_AOL) == NETWORK_ALIVE_AOL)
                {
                    outPut = "在线：NETWORK_ALIVE_AOL\n";
                }
            }
            else
            {
                outPut = "不在线\n";
            }
         //   Console.WriteLine(outPut);
            return m_bOnline;
        }

        public static bool IsOnline()
        {
            bool ok = false;
            try
            {
              ok=  Fun_IsNetworkAlive() || Fun_InternetGetConnectedState();
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsOnline" + ex.Message);
               
            }
            return ok;
        }
        public static Task<bool> IsOnlineAsync()
        {
            return Task.Factory.StartNew(() => IsOnline());
        }

     
    }
}
