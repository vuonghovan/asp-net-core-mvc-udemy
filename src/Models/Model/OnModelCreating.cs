using Microsoft.EntityFrameworkCore;
using Models.Identity;

namespace Models
{
     public partial class MyDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().Ignore(e => e.FullName);
        }
     }
}