namespace Users.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserForm model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}