namespace Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("aspnetusers")]
    public partial class aspnetusers
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string NormalizedUserName { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string NormalizedEmail { get; set; }

        [Required]
        [MaxLength(1)]
        public byte[] EmailConfirmed { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        [StringLength(256)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(1)]
        public byte[] PhoneNumberConfirmed { get; set; }

        [Required]
        [MaxLength(1)]
        public byte[] TwoFactorEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }

        [Required]
        [MaxLength(1)]
        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastLogoutDate { get; set; }
    }
}
