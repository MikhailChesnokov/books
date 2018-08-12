namespace ConfiguringApp.Middlewares
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;


    // this type of middleware doesn’t always forward requests to the next component in the chain
    public class ShortCircuitMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ShortCircuitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if ((bool)context.Items["Edge"])
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}