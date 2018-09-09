namespace Web.Infrastructure
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class CustomHtmlResult : IActionResult
    {
        public string Content { get; set; }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 200;
            context.HttpContext.Response.ContentType = "text/html";
            byte[] content = Encoding.ASCII.GetBytes(Content);
            await context.HttpContext.Response.Body.WriteAsync(content, 0, content.Length);
        }
    }
}