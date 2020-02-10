using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using moment2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace moment2
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            var dt = DateTime.Now.ToString("HH:mm:ss"); // Instansierar nytt tidsobjekt

            // Kollar om det redan finns en sessionsvariabel,
            // Så den inte skrivs över när man besöker index-sidan igen
            if (HttpContext.Session.GetString("date") == null) {
                HttpContext.Session.SetString("date", dt); // Spara ned i ny sessionsvariabel
                ViewBag.dateSession = dt; // Spara ned i ViewBag som sidan sedan kan använda
            }
            else {
                var sessionString = HttpContext.Session.GetString("date");
                ViewBag.dateSession = sessionString;
            }

            // Sparar ned data i en ViewData som sedan visas i View'n för Index-sidan
            ViewData["subtitle"] = "Detta är min sida i moment 2, ASP.NET Core MVC";
            ViewData["content"] = "Löksås ipsum strand gör hela där vid stora färdväg ingalunda samma år, tiden del tre sitt sällan mjuka vi det sällan hwila. Jäst kunde olika plats vid annan hav icke, se hav stig inom ingalunda verkligen, och samtidigt hwila där kanske jäst.";
            return View();
        }


        [HttpPost]
        [Route("/bmi")] 
        public IActionResult BMI(IFormCollection fc)
        {
            // Koll så att man inte kan skicka in noll eller minusvärden.
            if (int.Parse(fc["weight-input"]) <= 0 || int.Parse(fc["height-input"]) <= 0) {
                return View();
            }

            // Hämta Sessionsvariabeln och spara ned i en ViewBag som specifika sidan sedan kan använda
            var sessionString = HttpContext.Session.GetString("date");
            ViewBag.dateSession = sessionString;

            double weight = double.Parse(fc["weight-input"]); 
            double meterHeight = double.Parse(fc["height-input"]);
            double cmHeight = meterHeight / 100;

            // Räkna ut BMI: Vikt / (längd x längd )
            double sum = weight / (cmHeight * cmHeight);
            ViewBag.BMI = Math.Round(sum, 1);
            return View();
        }

        [HttpGet]
        [Route("/bmi")] // Ändrar om routing till /bmi istället för /Home/BMI
        public IActionResult BMI()
        {
            // Hämta Sessionsvariabeln och spara ned i en ViewBag som specifika sidan sedan kan använda
            var sessionString = HttpContext.Session.GetString("date");
            ViewBag.dateSession = sessionString;
            return View();
        }

        [HttpGet]
        [Route("/mvc")]
        public IActionResult MVC()
        {
            // Hämta Sessionsvariabeln och spara ned i en ViewBag som specifika sidan sedan kan använda
            var sessionString = HttpContext.Session.GetString("date");
            ViewBag.dateSession = sessionString;

            // Läs in JSON-fil och parse'a
            var jsonString = System.IO.File.ReadAllText("Info.json");
            var jsonObj = JsonConvert.DeserializeObject<IEnumerable<InfoModel>>(jsonString);

            return View(jsonObj);
        }

    }


}


