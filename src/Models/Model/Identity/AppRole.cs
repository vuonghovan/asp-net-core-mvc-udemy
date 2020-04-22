using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Identity
{
    public class AppRole : IdentityRole<long>
    {
        public AppRole() { }

        public AppRole(string name)
        {
            Name = name;
        }
    }

    public class AppRoleEdit
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
