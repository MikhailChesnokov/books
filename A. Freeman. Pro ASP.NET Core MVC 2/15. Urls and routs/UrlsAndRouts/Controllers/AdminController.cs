namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Result", new Result
            {
                Controller = nameof(AdminController),
                Action = nameof(Index)
            });
        }
    }
}