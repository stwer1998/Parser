using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamDotNet.Models;
using ExamDotNet.DataBase;
using ExamDotNet.Parser;

namespace ExamDotNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LinkRepository db;
        public HomeController(ILogger<HomeController> logger, LinkRepository db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public IActionResult Scan() 
        {
            //var parser = new ExamDotNet.Parser.Parser();
            ////var result = parser.GetUrls(a);

            ////var parser = new ExamDotNet.Parser.Parser();
            //parser.SomeParseAsync();
            return View();
        }

        [HttpPost]
        public IActionResult Scan(DtoLink dtoLink)
        {

            var parser = new ParalelParser(dtoLink);
            parser.Parse();
            //if (ModelState.IsValid) 
            //{
            //    var r = db.Get(dtoLink.Link.Origin);
            //    if (r.Length == 0 || r == null)
            //    {
            //        var parser = new ExamDotNet.Parser.Parser();
            //        var result = parser.Parse(dtoLink);
            //        db.Add(result);
            //    }
            //    return RedirectToAction("Result", new { url = dtoLink.Link });
            //}
            //else return View(dtoLink);
            return View();
        }


        public IActionResult Result(string url) 
        {
            var result = db.Get(url);
            
            return View(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
