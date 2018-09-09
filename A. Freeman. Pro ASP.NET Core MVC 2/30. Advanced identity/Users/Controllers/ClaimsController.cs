namespace Users.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    
    
    public class ClaimsController : Controller
    {
        public ViewResult Index() => View(User?.Claims);
    }
}