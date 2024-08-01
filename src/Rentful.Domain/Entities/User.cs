using System.ComponentModel.DataAnnotations.Schema;

namespace Rentful.Domain.Entities
{
    [Table("users", Schema = "public")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
