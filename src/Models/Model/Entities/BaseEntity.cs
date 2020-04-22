using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class BaseEntity
    {
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Column("m_user_created_id")]
        public long m_user_created_id { get; set; }

        [Column("modified")]
        public DateTime Modified { get; set; } = DateTime.UtcNow;

        [Column("m_user_modified_id")]
        public long m_user_modified_id { get; set; }

        [Column("deleted")]
        public DateTime? Deleted { get; set; }

        [Column("m_user_deleted_id")]
        public long? m_user_deleted_id { get; set; }

        [Column("delete_flag")]
        public byte delete_flag { get; set; }
    }
}
