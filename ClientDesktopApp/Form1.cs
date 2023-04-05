using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;

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


        private async void buttonReadText_Click(object sender, EventArgs e)
        {
            string route = "api/DesktopApi/ReadText";
            Log($"Try connect to {route}...");
            HttpResponseMessage responseMessage = await client.GetAsync(route);
            if (responseMessage.IsSuccessStatusCode)
            {
                Log($"{route} - success!");
            }
            else
            {
                Log($"{route} - failed!");
            }
            string answer = await responseMessage.Content.ReadAsStringAsync();
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
            Log($"Try connect to {route}...");
            List<string> texts = await client.GetFromJsonAsync<List<string>>(route);
            foreach (string line in texts)
            {
                textBoxTextsList.Text += $"{line}{nl}";
            }
            Log("Read texts list finished");
        }
    }
}
