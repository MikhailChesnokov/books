namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("app/[controller]/actions/[action]/{id:weekday?}")]
    public class CustomerController : Controller
    {
        // 1. Complete route
        // 2. Prevent default routing
        [Route("myroute")]
        public IActionResult Index()
        {
            return View("Result", new Result
            {
                Controller = nameof(CustomerController),
                Action = nameof(Index)
            });
        }
        
        [Route("[controller]/MyAction")] // like nameof(CustomerController)
        public IActionResult List()
        {
            return View("Result", new Result
            {
                Controller = nameof(CustomerController),
                Action = nameof(List)
            });
        }
    }
}