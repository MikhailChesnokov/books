namespace AsyncAction.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;



    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            long? result = await GetPageLength();

            return View(result);
        }

        private Task<long?> GetPageLength()
        {
            var client = new HttpClient();

            Task<HttpResponseMessage> requesTask = client.GetAsync("http://apress.com");

            return requesTask.ContinueWith(t => t.Result.Content.Headers.ContentLength);
        }
    }
}