using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Identity;

namespace Models
{
    public partial class MyDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public MyDbContext()
        {
            
        }
    }
}
