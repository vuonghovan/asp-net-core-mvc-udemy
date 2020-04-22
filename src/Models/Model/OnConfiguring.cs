using Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class MyDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.InitDbContext();
            }
        }
    }
}