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
using System.IO;
using System.Net.Mime;
using System.Net.Http.Headers;

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
            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                await LogResponse(response);
                string text = await response.Content.ReadAsStringAsync();
                textBoxReadText.Text = text;
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
                await LogResponse(response);
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
                await LogResponse(response);
                if (!response.IsSuccessStatusCode)
                    return;

                TestObject testObject = await response.Content.ReadFromJsonAsync<TestObject>();
                if (testObject.Tags == null)
                {
                    Log("Tags is not loaded!");
                    return;
                }
                textBoxObject.Text = $"Name: {testObject.Name}{nl}" +
                $"Description: {testObject.Description}{nl}" +
                $"Created time: {testObject.CreatedAt}{nl}" +
                $"Tags: {string.Join(", ", testObject.Tags)}";
            }
        }

        //отправка через get
        private async void buttonAddObject_Click(object sender, EventArgs e)
        {
            FormNewObject formNew = new FormNewObject();
            if (formNew.ShowDialog() != DialogResult.OK) return;
            TestObject newObject = formNew.model;

            string name = newObject.Name;
            string description = newObject.Description;
            //List<string> tags = newObject.Tags;
            string tags = string.Join(",", newObject.Tags);
            string route = $"api/DesktopApi/AddComplexObject/{name}&{description}&{tags}";
            Log($"Try connect to {route}...");
            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                await LogResponse(response);
            }
            Log("Add object finished!");
        }

        //отправка через post
        private async void buttonAddObjectV2_Click(object sender, EventArgs e)
        {
            FormNewObject formNew = new FormNewObject();
            if (formNew.ShowDialog() != DialogResult.OK) return;
            TestObject newObject = formNew.model;
            string route = "api/DesktopApi/AddComplexObjectPost";
            using (HttpResponseMessage response = await client.PostAsJsonAsync(route, newObject))
            {
                await LogResponse(response);
            }
        }

        private async void buttonRefreshFiles_Click(object sender, EventArgs e)
        {
            string route = "api/DesktopApi/GetFilesList";
            Log($"Try connect to {route}...");
            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                await LogResponse(response);
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                List<string> texts = await response.Content.ReadFromJsonAsync<List<string>>();
                comboBoxFiles.DataSource = texts;
            }
            Log("Read files list finished!");
        }

        private async void buttonDownloadFile_Click(object sender, EventArgs e)
        {
            string filename = comboBoxFiles.Text;
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string filepath = Path.Combine(folder, filename);
            if (File.Exists(filepath))
            {
                Log($"File exists, deleted: {filepath}");
                File.Delete(filepath);
            }

            string route = $"api/DesktopApi/DownloadFile/{filename}";
            Log($"Try connect to {route}...");
            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                await LogResponse(response);
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }
                //можно сохранять через поток или через байт массив
                //using (Stream stream = await response.Content.ReadAsStreamAsync())
                //{
                //    using(FileStream newfile = File.Create(filepath))
                //    {
                //        stream.Seek(0, SeekOrigin.Begin);
                //        stream.CopyTo(newfile);
                //    }
                //}
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes(filepath, bytes);
            }
            Log($"File downloaded to {filepath}");
        }

        private async void buttonUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string filepath = openFileDialog.FileName;
            string filename = Path.GetFileName(filepath);
            string route = $"api/DesktopApi/UploadFile";
            using (MultipartFormDataContent form = new MultipartFormDataContent())
            {
                StreamContent content = new StreamContent(File.OpenRead(filepath));
                //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                //content.Headers.ContentLength = 0;
                form.Add(content, filename, filename);
                using (HttpResponseMessage response = await client.PostAsync(route, form))
                {
                    await LogResponse(response);
                }
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string route = "AccountApi/Login";
            //string route = "api/DesktopApi/LoginApi";
            LoginModel loginModel = new LoginModel { Username = username, Password = password };
            using (HttpResponseMessage response = await client.PostAsJsonAsync(route, loginModel))
            {
                await LogResponse(response);
            }
        }

        private async Task LogResponse(HttpResponseMessage response)
        {
            string msg = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Log($"Success {response.StatusCode}: {msg}");
            }
            else
            {
                Log($"Error {response.StatusCode}: {msg}");
            }
        }

        private async void buttonReadAuthText_Click(object sender, EventArgs e)
        {
            string route = "AccountApi/ReadTextAuth";
            using (HttpResponseMessage response = await client.GetAsync(route))
            {
                await LogResponse(response);
            }
            
        }
    }
}
