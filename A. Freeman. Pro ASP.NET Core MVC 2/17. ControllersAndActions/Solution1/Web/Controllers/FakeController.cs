namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [NonController]
    public class FakeController
    {
        [NonAction]
        public string Index() => "This is not a POCO controller";
    }
}