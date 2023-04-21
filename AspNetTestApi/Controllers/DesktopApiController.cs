using AspNetTestApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using DomainModel;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AspNetTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DesktopApiController : ControllerBase
    {
        public static List<string> texts = new List<string> { "Text1", "Текст2" };
        private readonly DB db;

        public DesktopApiController(DB db)
        {
            this.db = db;
        }

        public IActionResult ReadText()
        {
            string text = "ABCDabcd_АБВГабвг1234 ~!@#$%^&*()_+=\"<>,.";
            return Content(text);
        }

        public List<string> ReadTextsList()
        {
            return texts;
        }

        [HttpGet("{text?}")]
        public IActionResult AddTextToList(string? text)
        {
            //using StreamReader reader = new StreamReader(HttpContext.Request.Body);
            //string name = await reader.ReadToEndAsync();
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Parameter \"text\" is null!");
            }
            if (texts.Contains(text))
            {
                return BadRequest($"Text {text} already exists!");
            }
            texts.Add(text);
            Console.WriteLine($"Text added: {text}");
            return Ok($"Text is added: {text}");
        }

        //[HttpGet("{id}")]
        //public TestObject ReadComplexObjectAsJson(int id)
        //{
        //    TestObject? o = objects.FirstOrDefault(i => i.Id == id)
        //        ?? throw new InvalidOperationException($"Object id {id} not found");

        //    return o; //объект должен сериализоваться в json? да, но неудобно возвращать сообщения или статус коды
        //}
        //[HttpGet("{id}")]
        //public async Task ReadComplexObjectAsJson(int id, HttpResponse response)
        //{
        //    TestObject? o = objects.FirstOrDefault(i => i.Id == id);
        //    if (o == null)
        //    {
        //        response.StatusCode = 404; // NotFound($"Object id{id} not exists");
        //        return;
        //    }
        //    await response.WriteAsJsonAsync(o); //не работает, эксепшн на стороне клиента
        //}

        [HttpGet("{id}")]
        public ActionResult<TestObject> ReadComplexObjectAsJson(int id)
        {
            TestObject? o = db.TestObjects.Include(i => i.Tags).FirstOrDefault(i => i.Id == id);
            //if (o == null)
                //вот тут как раз будет trrow new CustomError 
                //return Results.NotFound($"Object id {id} not found");

            return Ok(o); //а вот так всё работает
        }

        // Это что за путь такой интересный 
        [HttpGet("{name}&{description}&{tags}")]
        public async Task<IActionResult> AddComplexObject(string name, string description, string tags)
        {
            if (db.TestObjects.Any(i => i.Name == name))
                return BadRequest($"Name {name} already exists");
            
            TestObject to = new TestObject(0, name, description, tags);
            await db.TestObjects.AddAsync(to);
            await db.SaveChangesAsync();
            return Ok($"Object added with id {to.Id}");
        }

        [HttpPost]
        public async Task<IResult> AddComplexObjectPost(TestObject newObject)
        {
            string name = newObject.Name;
            if (await db.TestObjects.AnyAsync(i => i.Name == name))
                return Results.BadRequest($"Name {name} already exists");
            
            await db.TestObjects.AddAsync(newObject);
            await db.SaveChangesAsync();
            return Results.Json(newObject);
        }

        [HttpGet("{filename}")]
        public IActionResult DownloadFile(string filename)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", filename);
            var contentType = "application/octet-stream";

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = new FileStreamResult(fileStream, contentType)
            {
                FileDownloadName = filename
            };

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            if (file.Length == 0)
                return BadRequest("File is empty Файл пустой");
            string filePath = Path.Combine(folder, file.FileName);
            if (System.IO.File.Exists(filePath))
                return BadRequest("File is already exists Файл уже существует");

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Success успешно");
        }

        [HttpGet]
        public IEnumerable<string> GetFilesList()
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            if (!Directory.Exists(folder))
            {
                //await HttpContext.Response.WriteAsync($"Folder not exists: {folder})");
                throw new Exception($"Folder not exists: {folder}");
            }
            IEnumerable<string> files = Directory.GetFiles(folder).Select(i => Path.GetFileName(i));
            return files;
        }
    }
}
