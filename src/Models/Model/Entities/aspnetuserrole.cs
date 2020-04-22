namespace Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("aspnetuserroles")]
    public partial class UserRole
    {
        [Key]
        [Column(Order = 0)]
        public long UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public long RoleId { get; set; }
    }
}
