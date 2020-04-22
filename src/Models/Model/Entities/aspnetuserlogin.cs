using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    [Table("aspnetuserlogins")]
    public partial class UserLogin
    {
        [Key]
        [StringLength(450)]
        public string LoginProvider { get; set; }

        [Key]
        [StringLength(450)]
        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        public long? UserId { get; set; }
    }
}
