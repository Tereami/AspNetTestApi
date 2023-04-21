using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;
using System.IO;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace ClientDesktopApp
{
    public partial class Form1 : Form
    {
        string nl = System.Environment.NewLine;

        private IRestClient _restClient { get; set; }
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

            var options = new RestClientOptions(serverUrl);
            _restClient = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());

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
            var request = new RestRequest(route);
            var response = await _restClient.GetAsync(request);
            textBoxReadText.Text = response.Content;
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
            var request = new RestRequest(route);
            var response = await _restClient.GetAsync<List<string>>(request);
            foreach (string line in response)
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
            var request = new RestRequest(route).AddParameter("text", text, ParameterType.QueryString);
            try
            {
                var response = await _restClient.GetAsync(request);
                Log("Add text finished!");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
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
            var request = new RestRequest(route).AddParameter("id", id, ParameterType.UrlSegment);
            try
            {
                var response = await _restClient.GetAsync<TestObject>(request);
                textBoxObject.Text = $"Name: {response.Name}{nl}" +
                $"Description: {response.Description}{nl}" +
                $"Created time: {response.CreatedAt}{nl}" +
                $"Tags: {string.Join(", ", response.Tags)}";
                Log("Read object finished!");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
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
            // using (HttpResponseMessage response = await client.GetAsync(route))
            // {
            //     await LogResponse(response);
            // }
            Log("Add object finished!");
        }

        //отправка через post
        private async void buttonAddObjectV2_Click(object sender, EventArgs e)
        {
            FormNewObject formNew = new FormNewObject();
            if (formNew.ShowDialog() != DialogResult.OK) return;
            TestObject newObject = formNew.model;
            string route = "api/DesktopApi/AddComplexObjectPost";
            // using (HttpResponseMessage response = await client.PostAsJsonAsync(route, newObject))
            // {
            //     await LogResponse(response);
            // }
        }

        private async void buttonRefreshFiles_Click(object sender, EventArgs e)
        {
            string route = "api/DesktopApi/GetFilesList";
            Log($"Try connect to {route}...");

            var request = new RestRequest(route);
            try
            {
                var response = await _restClient.GetAsync<List<string>>(request);
                comboBoxFiles.DataSource = response;
                Log("Read files list finished!");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
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

            var request = new RestRequest(route).AddParameter("filename", filename, ParameterType.UrlSegment);
            try
            {
                var response = await _restClient.DownloadStreamAsync(request);
                if (response == null)
                {
                    Log($"File not found");
                }
                using (FileStream newfile = File.Create(filepath))
                {
                    response.CopyTo(newfile);
                }
            }
            catch (Exception ex)
            {
                Log($"Error while reading file: {ex.Message}");
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

            var restRequest = new RestRequest(route);
            restRequest.AlwaysMultipartFormData = true;
            restRequest.AddFile("file", filepath);
            try
            {
                var response = await _restClient.PostAsync(restRequest);
            }
            catch (Exception ex)
            {
                Log($"Error while uploading file: {ex.Message}");
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string route = "AccountApi/Login";
            LoginModel loginModel = new LoginModel { Username = textBoxUsername.Text, Password = textBoxPassword.Text };

            var request = new RestRequest(route).AddJsonBody(loginModel);
            try
            {
                var response = await _restClient.PostAsync(request);
                Log($"Status code : {response.StatusCode}, Content: {response.Content}");
            }
            catch (Exception ex)
            {
                Log($"Error : {ex.Message}");
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

            Log($"Try connect to {route}...");
            var request = new RestRequest(route);
            try
            {
                var response = await _restClient.GetAsync(request);
                Log($"Status code : {response.StatusCode}, Content: {response.Content}");
            }
            catch (Exception ex)
            {
                Log($"Error : {ex.Message}");
            }
        }
    }
}
