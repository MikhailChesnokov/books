using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Party_Invites.Models;

namespace Party_Invites.Controllers
{
    using System;
    using System.Linq;



    public class HomeController : Controller
    {
        public string Index1() => "Hello, world";

        public ViewResult Index2() => View("MyView");

        public ViewResult Index()
        {
            ViewBag.Greeting = DateTime.Now.Hour < 12 ? "GM" : "GA";

            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm() => View(new GuestResponce());

        [HttpPost]
        public ViewResult RsvpForm(GuestResponce responce)
        {
            if (ModelState.IsValid)
            {
                Repository.Add(responce);

                return View("Thanks", responce);
            }
            else
            {
                return View(responce);
            }
        }

        public ViewResult ListResponses() => View(Repository.Responses.Where(x => x.WillAttend is true));

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
