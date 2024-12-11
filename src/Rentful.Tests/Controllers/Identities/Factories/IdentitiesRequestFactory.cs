using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;

namespace Rentful.Tests.Controllers.Identities.Factories
{
    internal static class IdentitiesRequestFactory
    {
        public static User CreateUser()
        {
            return new User
            {
                Id = 1,
                Email = "test@wp.pl",
                Password = "$2a$11$tuhS17EwyCAYsz1PMOwKlOCp4Brf.HMbaqjAc/7MwOyvJsF5lzro."
            };
        }

        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "User",
                    Type = RoleEnum.User
                }
            };
        }
    }
}
