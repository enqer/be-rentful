using Rentful.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("roles", Schema = "rentful")]
    public class Role
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("type")]
        public RoleEnum Type { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new List<User>();

    }
}
