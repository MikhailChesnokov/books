namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DerivedController : Controller
    {
        public ViewResult Index() => View("Result", "This is a derived controller.");

        public IActionResult Properties()
        {
            var request = Request;
            var responce = Response;
            var httpContext = HttpContext;
            var routeData = RouteData;
            var modelState = ModelState;
            var user = User;

            return null;
        }
    }
}