using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;
using Newtonsoft.Json;

namespace ClientDesktopApp
{
    public partial class Form1 : Form
    {
        string nl = System.Environment.NewLine;
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
            EnableControls(this, true);
        }

        private void EnableControls(Control control, bool enabled)
        {
            control.Enabled = enabled;
            foreach (Control child in control.Controls)
            {
                EnableControls(child, enabled);
            }
        }

        private async Task<string> SendRequestAndReadText(string route)
        {
            Log($"Try connect to {route}...");
            HttpResponseMessage responseMessage = await client.GetAsync(route);
            if (responseMessage.IsSuccessStatusCode)
            {
                Log($"{route} - success!");
            }
            else
            {
                Log($"{route} - failed!");
                return "Failed";
            }
            string answer = await responseMessage.Content.ReadAsStringAsync();
            Log(answer);
            return answer;
        }

        private async void buttonReadText_Click(object sender, EventArgs e)
        {
            string route = "api/DesktopApi/ReadText";
            string answer = await SendRequestAndReadText(route);
            textBoxReadText.Text = answer;
            Log("Read text finished");
        }

        private void Log(string log)
        {
            rtbLog.Text += $"{DateTime.Now.ToString("HH:mm:ss.fff")}\t{log}\n";
        }

        

        private async void buttonReadTextList_Click(object sender, EventArgs e)
        {
            textBoxTextsList.Clear();
            string route = "api/DesktopApi/ReadTextsList";
            string answer = await SendRequestAndReadText(route);
            List<string> texts = JsonConvert.DeserializeObject<List<string>>(answer);
            foreach(string line in texts)
            {
                textBoxTextsList.Text += $"{line}{nl}";
            }
            Log("Read texts list finished");
        }
    }
}
