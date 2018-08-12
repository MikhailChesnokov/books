namespace ConfiguringApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Middlewares;



    internal class Startup
    {
        public IConfiguration Configuration;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }
        
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services
                .AddMvc()
                .AddMvcOptions(options =>
                {
                    var c = options.Conventions;
                    FilterCollection f = options.Filters;
                    FormatterMappings m = options.FormatterMappings;
                    var i = options.InputFormatters;
                    var v = options.ModelValidatorProviders;
                    var o = options.OutputFormatters;
                    bool r = options.RespectBrowserAcceptHeader;
                })
                //.AddFormatterMappings()
                //.AddJsonOptions()
                //.AddRazorOptions()
                //.AddViewOptions()
                ;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if ((Configuration.GetSection("ShortCircuitMiddleware")?.GetValue<bool>("EnableBrowserShortCircuit")).Value)
            {
                
            }
            
            if (env.IsDevelopment())
            {
                app.UseMiddleware<ResponceEditingMiddleware>();
                app.UseMiddleware<RequestEditingMiddleware>();
                app.UseMiddleware<ShortCircuitMiddleware>();
                app.UseMiddleware<ContentMiddleware>();

                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("Home/Error");
            };


            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}