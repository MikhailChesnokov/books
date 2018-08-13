namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Result", new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(Index)
            });
        }
    }
}