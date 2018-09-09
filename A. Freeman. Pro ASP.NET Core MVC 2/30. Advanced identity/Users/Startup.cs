using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Users
{
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        
        
        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));

            services
                .AddIdentity<AppUser, IdentityRole>(options => { options.Password.RequiredLength = 3; })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IClaimsTransformation, LocationClaimsProvider>();
            services.AddTransient<IAuthorizationHandler, BlockUserHandler>();
            services.AddTransient<IAuthorizationHandler, DocumentAuthorizationHandler>();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Blocked", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new BlockUserRequirement("Bob"));
                });

                options.AddPolicy("AuthorsAndEditors", policy =>
                {
                    policy.AddRequirements(new DocumentAuthorizationRequirement
                    {
                        AllowAuthors = true,
                        AllowEditors = true
                    });
                });
            });
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}