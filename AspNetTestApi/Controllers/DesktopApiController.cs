﻿using AspNetTestApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using DomainModel;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;

namespace AspNetTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DesktopApiController : ControllerBase
    {
        public static List<string> texts = new List<string> { "Text1", "Текст2" };
        public static List<TestObject> objects = new List<TestObject>
        {
            new TestObject(0, "First объект", "First описание", new List<string> {"tag1", "тэг2"})
        };

        public IActionResult ReadText()
        {
            string text = "ABCDabcd_АБВГабвг1234 ~!@#$%^&*()_+=\"<>,.";
            Thread.Sleep(5000);
            return Content(text);
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
        public IResult ReadComplexObjectAsJson(int id)
        {
            TestObject? o = objects.FirstOrDefault(i => i.Id == id);
            if (o == null)
                return Results.NotFound($"Object id {id} not found");

            return Results.Json(o); //а вот так всё работает
        }

        [HttpGet("{name}&{description}&{tags}")]
        public IActionResult AddComplexObject(string name, string description, string tags)
        {
            if (objects.Any(i => i.Name == name))
                return BadRequest($"Name {name} already exists");
            int newId = 1 + objects.Max(i => i.Id);
            List<string> tagsList = tags.Replace(" ", "").Split(',').ToList();
            TestObject to = new TestObject(newId, name, description, tagsList);
            objects.Add(to);
            return Ok($"Object added with id {newId}");
        }

        [HttpPost]
        public IResult AddComplexObjectPost(TestObject newObject)
        {
            string name = newObject.Name;
            if (objects.Any(i => i.Name == name))
                return Results.BadRequest($"Name {name} already exists");
            newObject.Id = 1 + objects.Max(i => i.Id);
            objects.Add(newObject);
            return Results.Json(newObject);
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
                return BadRequest(new { type = "error", msg = "No files нет файлов" });
            if (formFiles.Count > 1)
                return BadRequest(new { type = "error", msg = "More than 1 file Допускается только 1 файл" });

            IFormFile file = formFiles[0];
            string filePath = Path.Combine(folder, filename);
            if (System.IO.File.Exists(filePath))
                return Content("File is already exists Файл уже существует");

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {

            }

            string msg = "Success успешно";
            return Ok(msg);
        }
    }
}
