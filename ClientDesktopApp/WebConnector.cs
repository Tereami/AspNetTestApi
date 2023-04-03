using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientDesktopApp
{
    public class WebConnector
    {
        HttpClient client = new HttpClient();

        public WebConnector(string serverUrl)
        {
            client.BaseAddress = new Uri(serverUrl);
        }
    }
}
