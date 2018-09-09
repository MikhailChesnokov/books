namespace Web.Controllers
{
    using System.Text;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("SimpleForm");
        }

        public ViewResult ReceiveForm()
        {
            // parameters from Request.Form[]
            
            var name = Request.Form["name"];
            var city = Request.Form["city"];
            
            return View("Result", $"{name} lives in {city}.");
        }

        public ViewResult ReceiveForm(string name, string city)
        {
            return View("Result", $"{name} lives in {city}.");
        }
        
        // responce
        public void ReceiveForm2(string name, string city)
        {
            Response.StatusCode = 200;
            Response.ContentType = "text/html";
            byte[] content = Encoding.ASCII.GetBytes($"<html><body><h1>CONTENT</h1></body></html>");
            Response.Body.WriteAsync(content, 0, content.Length);
        }

        public IActionResult ReceiveForm3(string name, string city)
        {
            return new CustomHtmlResult
            {
                Content = $"{name} lives in {city}."
            };
        }
    }
}