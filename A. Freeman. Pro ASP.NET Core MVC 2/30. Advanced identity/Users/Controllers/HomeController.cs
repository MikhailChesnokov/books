namespace Users.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View(new Dictionary<string, object>
            {
                ["Placeholder"] = "Placeholder"
            });
        }
    }
}