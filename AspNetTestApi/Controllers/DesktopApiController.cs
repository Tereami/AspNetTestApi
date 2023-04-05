using AspNetTestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using DomainModel;

namespace AspNetTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DesktopApiController : ControllerBase
    {
        private List<string> texts = new List<string> { "Text1", "Текст2" };

        public IActionResult ReadText()
        {
            string text = "ABCDabcd_АБВГабвг1234 ~!@#$%^&*()_+=\"<>,.";
            Thread.Sleep(5000);
            return Content(text);
        }

        public TestObject ReadComplexObjectAsJson()
        {
            TestObject o = new TestObject(1, "Test", "Описание", new List<string> { "One", "Two", "Три" });
            return o; //объект должен сериализоваться в json
        }

        public List<string> ReadTextsList()
        {
            return texts;
        }

        public IActionResult AddTextToList(string text)
        {
            texts.Add(text);
            return Ok();
        }

        [Authorize]
        public IActionResult ReadTextAuth()
        {
            string text = "Text only for auththorized users";
            return Content(text);
        }

        public IActionResult DownloadFile(string filename)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", filename);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                string file_type = "application/octet-stream";
                string file_name = filename;
                return File(fs, file_type, file_name);
            }
        }

        [HttpPost]
        public IActionResult UploadFile(string filename)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            IFormFileCollection formFiles = Request.Form.Files;
            if (formFiles.Count == 0)
                return BadRequest(new {type = "error", msg = "No files нет файлов"});
            if (formFiles.Count > 1)
                return BadRequest(new { type = "error", msg = "More than 1 file Допускается только 1 файл" });

            IFormFile file = formFiles[0];
            string filePath = Path.Combine(folder, filename);
            if(System.IO.File.Exists(filePath))
                return Content("File is already exists Файл уже существует");

            using(FileStream stream = new FileStream(filePath, FileMode.Create))
            {

            }

            string msg = "Success успешно";
            return Ok(msg);
        }
    }
}
