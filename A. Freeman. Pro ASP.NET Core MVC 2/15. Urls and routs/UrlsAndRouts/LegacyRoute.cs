namespace Web
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    
    public class LegacyRoute : IRouter
    {
        private readonly string[] _urls;
        
        public LegacyRoute(params string[] targetUrls) {
            _urls = targetUrls;
        }
        
        public async Task RouteAsync(RouteContext context)
        {
            string requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');
            
            if (_urls.ToList().Contains(requestedUrl, StringComparer.OrdinalIgnoreCase)) {
                context.Handler = async ctx => {
                    HttpResponse response = ctx.Response;
                    byte[] bytes = Encoding.ASCII.GetBytes($"URL: {requestedUrl}");
                    await response.Body.WriteAsync(bytes, 0, bytes.Length);
                };
            }
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}