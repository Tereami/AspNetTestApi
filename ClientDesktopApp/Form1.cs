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
            using (HttpResponseMessage responseMessage = await client.GetAsync(route))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    Log($"{route} - success!");
                }
                else
                {
                    Log($"{route} - failed!");
                }
                string response = await responseMessage.Content.ReadAsStringAsync();
                textBoxReadText.Text = response;
            }
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

        private async void buttonAddTextToList_Click(object sender, EventArgs e)
        {
            string text = textBoxTextToAdd.Text;
            string route = $"api/DesktopApi/AddTextToList/{text}";
            Log($"Try connect to {route}...");

            //
            //StringContent stringContent = new StringContent(text, Encoding.UTF8);
            //using (HttpResponseMessage response = await client.PostAsync(route, stringContent))
            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                if (response.IsSuccessStatusCode)
                    Log($"Success: {response.StatusCode}");
                else
                    Log($"Failed: {response.StatusCode}");
                string responseText = await response.Content.ReadAsStringAsync();
                Log(responseText);
            }
            Log("Add text finished!");
        }

        private void buttonAddTextToList_MouseEnter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTextToAdd.Text))
                buttonAddTextToList.Enabled = false;
        }

        private void textBoxTextToAdd_TextChanged(object sender, EventArgs e)
        {
            buttonAddTextToList.Enabled = true;
        }

        private async void buttonReadObject_Click(object sender, EventArgs e)
        {
            int id = (int)numericObjectId.Value;
            string route = $"api/DesktopApi/ReadComplexObjectAsJson/{id}";
            Log($"Try connect to {route}...");

            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                if (!response.IsSuccessStatusCode)
                {
                    Log($"Failed: {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                    return;
                }
                else
                {
                    Log($"Success! JSON:{await response.Content.ReadAsStringAsync()}");
                }
                TestObject testObject = await response.Content.ReadFromJsonAsync<TestObject>();
                textBoxObject.Text = $"Name: {testObject.Name}{nl}" +
                    $"Description: {testObject.Description}{nl}" +
                    $"Created time: {testObject.CreatedAt}{nl}" +
                    $"Tags: {string.Join(", ", testObject.Tags)}";
            }
        }
    }
}
