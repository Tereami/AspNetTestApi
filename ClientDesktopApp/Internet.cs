using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ClientDesktopApp
{
    public class Internet
    {
        public static bool Ok(string server)
        {
            if(server.StartsWith("http"))
            {
                Uri uri = new Uri(server);
                server = uri.Host;
            }
            server = server.Replace("/", "");
            try
            {
                Dns.GetHostEntry(server);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
