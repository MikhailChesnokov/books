namespace ConfiguringApp.Middlewares
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;



    public class ContentMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ContentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().ToLower() == "/middleware")
            {
                await context.Response.WriteAsync("This is from the content middleware", Encoding.UTF8);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}