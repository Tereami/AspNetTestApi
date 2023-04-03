using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;

namespace ClientDesktopApp
{
    public partial class Form1 : Form
    {
        HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string serverUrl = textBoxServerUrl.Text;
            Log($"Try connect to {serverUrl}...");
            if (Internet.Ok(serverUrl))
            {
                Log("Internet OK");
            }
            else
            {
                Log("Internet OK");
                return;
            }

            client.BaseAddress = new Uri(serverUrl);
        }

        private async void buttonReadText_Click(object sender, EventArgs e)
        {
            string route = "api/DesktopApi/ReadText";
            Log($"Try connect to {route}...");
            HttpResponseMessage responseMessage = await client.GetAsync(route);
            if(responseMessage.IsSuccessStatusCode)
            {
                Log("Success!");
            }
            else
            {
                Log("Failed!");
                return;
            }
            string answer = await responseMessage.Content.ReadAsStringAsync();
            Log(answer);
        }

        private void Log(string log)
        {
            rtbLog.Text += $"{DateTime.Now.ToLongTimeString()}\t{log}\n";
        }
    }
}
