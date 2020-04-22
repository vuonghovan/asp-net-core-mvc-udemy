using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configs
{
    public class ModuleCommon
    {
        public static IConfiguration Configuration { get; set; }
    }
    public static class ConfigCommon
    {
        public static DbContextOptionsBuilder InitDbContext(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            var connectProvider = configuration.GetConnectionString("UserProvider");


            var connectString = configuration.GetConnectionString(connectProvider);
            switch (connectProvider)
            {
                case "UseMySql":
                    optionsBuilder.UseMySql(connectString);
                    break;
                case "UseSqlServer":
                    optionsBuilder.UseSqlServer(connectString);
                    break;
                default:
                    //optionsBuilder.UseInMemoryDatabase("UseMySql");
                    break;
            }
            return optionsBuilder;
        }
        public static DbContextOptionsBuilder InitDbContext(this DbContextOptionsBuilder optionsBuilder)
        {
            return optionsBuilder.InitDbContext(ModuleCommon.Configuration);
        }
    }
}
