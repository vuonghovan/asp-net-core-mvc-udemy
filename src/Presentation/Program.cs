using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(AddDbConfiguration)
            .UseStartup<Startup>();

        private static void AddDbConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            var env = context.HostingEnvironment.EnvironmentName;

            builder.AddJsonFile("azuresettings.json", optional: true, reloadOnChange: true);
            if (env == "Development")
                builder.AddJsonFile($"azuresettings.{env}.json", optional: true, reloadOnChange: true)
                       .AddEnvironmentVariables();
        }
    }
}
