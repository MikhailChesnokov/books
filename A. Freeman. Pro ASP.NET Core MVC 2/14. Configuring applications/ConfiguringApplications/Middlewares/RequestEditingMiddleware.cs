namespace ConfiguringApp.Middlewares
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;



    public class RequestEditingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RequestEditingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items["Edge"] = context.Request.Headers["User-Agent"].Any(x => x.ToLower().Contains("edge"));

            await _next.Invoke(context);
        }
    }
}