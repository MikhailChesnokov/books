namespace ConfiguringApp
{
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;



    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
//            return WebHost
//                   .CreateDefaultBuilder(args)
//                   .UseStartup<Startup>();

            return
                new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory()) // For loading config files and delivering static content
                    .ConfigureAppConfiguration((hostingContext, config) => // For prepearing configuration data for the app (optional)
                    {
                        IHostingEnvironment env = hostingContext.HostingEnvironment;

                        config
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                        if (env.IsDevelopment())
                        {
                            Assembly appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));

                            if (appAssembly != null) config.AddUserSecrets(appAssembly, true);
                        }

                        config.AddEnvironmentVariables();

                        if (args != null) config.AddCommandLine(args);
                    })
                    .ConfigureLogging((hostingContext, logging) => // optional
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                    })
                    .UseIISIntegration()
                    .UseDefaultServiceProvider((context, options) => options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                    .UseStartup(nameof(ConfiguringApp));
        }
    }
}