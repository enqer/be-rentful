namespace Rentful.Application.Common.Interfaces
{
    public interface IUserResolver
    {
        int UserId { get; }
        string FirstName { get; }
        string LastName { get; }
        List<string> Roles { get; }
    }
}
