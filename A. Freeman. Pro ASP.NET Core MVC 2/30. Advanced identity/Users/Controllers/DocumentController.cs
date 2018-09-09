namespace Users.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public sealed class DocumentController : Controller
    {
        private readonly ProtectedDocument[] _docs = new ProtectedDocument[]
        {
            new ProtectedDocument
            {
                Title = "Q3 Budget",
                Author = "Alice",
                Editor = "Joe"
            },
            new ProtectedDocument()
            {
                Title = "Project Plan",
                Author = "Bob",
                Editor = "Alice"
            }, 
        };

        private IAuthorizationService _authorizationService;
        
        public DocumentController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        
        
        public ViewResult Index() => View();

        public async Task<IActionResult> Edit(string title)
        {
            var doc = _docs.FirstOrDefault(x => x.Title == title);

            AuthorizationResult result = await _authorizationService.AuthorizeAsync(User, doc, "AuthorsAndEditors");

            if (result.Succeeded)
            {
                return View("Index", doc);
            }

            return new ChallengeResult();
        }
    }
}