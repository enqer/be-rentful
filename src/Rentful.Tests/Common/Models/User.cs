namespace Rentful.Tests.Common.Models
{
    public class User
    {
        public int Id { get; set; }
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    }
}
