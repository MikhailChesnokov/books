using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Routing.Constraints;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Inline custom constraint
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("weekday", typeof(WeekDayConstraint));
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {

                
                routes.Routes.Add(new LegacyRoute(
                    "/articles/Windows_3.1_Overview.html",
                    "/old/.NET_1.0_Class_Library"));
                
                
                
                // Convention based routing
                
                routes.MapRoute(
                    name: "simple",
                    template: "{controller}/{action}");

                routes.MapRoute(
                    name: "default values",
                    template: "{controller}/{action}",
                    defaults: new {action = "Index"});

                routes.MapRoute(
                    name: "Inline default values",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "static segment",
                    template: "Api/{controller}s/{action}");
                
                routes.MapRoute(
                    name: "Custom segment variables",
                    template: "{controller}/{action}/{id=defaultId}");
                
                routes.MapRoute(
                    name: "Optional segments",
                    template: "{controller}/{action}/{id?}");
                
                routes.MapRoute(
                    name: "Variable-lenght routes",
                    template: "{controller}/{action}/{id?}/{*catchAll}");
                
                routes.MapRoute(
                    name: "Outside constraints",
                    template: "{controller:alpha=Home}/{action}/{id:int?}/{tab?}",
                    defaults: new {tab = new IntRouteConstraint()});
                
                routes.MapRoute(
                    name: "Regex constraints",
                    template: "{controller:alpha:minlength(3)=Home}/{action:regex(^Index$|^About$)}/{id:int?}");
                
                routes.MapRoute(
                    name: "Custom constraint",
                    template: "{controller}/{action}/{id}",
                    defaults: new {},
                    constraints: new {id = new WeekDayConstraint()});
                
                routes.MapRoute(
                    name: "Inline custom constraint",
                    template: "{controller}/{action}/{id:weekday?}",
                    defaults: new {},
                    constraints: new {id = new WeekDayConstraint()});
                
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}");
            });
        }
    }

    internal class WeekDayConstraint : IRouteConstraint
    {
        private static readonly string[] Days = {"mon", "tue", "web", "thu", "fri", "sat", "sun"};
        
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Days.Contains(values[routeKey]?.ToString().ToLower());
        }
    }
}