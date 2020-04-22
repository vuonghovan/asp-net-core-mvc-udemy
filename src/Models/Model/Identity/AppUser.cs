using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Identity
{
    public class AppUser : IdentityUser<long>
    {
        [PersonalData, Required, StringLength(20)]
        public string FirstName { get; set; }

        [PersonalData, Required, StringLength(20)]
        public string LastName { get; set; }

        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? LastLogoutDate { get; set; }
        
        [Column("is_disable")]
        public byte IsDisable { get; set; } = 0;

        [Column("is_admin")]
        public byte IsAdmin { get; set; } = 0;

        public string FullName
        {
            get { return $"{LastName} {LastName}"; }
        }

        public string ConvertToLocalTime(DateTimeOffset? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToLocalTime().ToString("dd/MM/yyyy hh:mm:ss");
            return "N/A";
        }
    }
}
