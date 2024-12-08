using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("roles", Schema = "rentful")]
    public class Role
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
