namespace ConfiguringApp.Middlewares
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;



    public class ResponceEditingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ResponceEditingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);

            if (context.Response.StatusCode is 403)
            {
                await context.Response.WriteAsync("Edge hot supported", Encoding.UTF8);
            }
            else if (context.Response.StatusCode is 404)
            {
                await context.Response.WriteAsync("No content");
            }
        }
    }
}