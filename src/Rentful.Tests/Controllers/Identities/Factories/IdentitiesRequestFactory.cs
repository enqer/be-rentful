using Rentful.Domain.Entities;

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
    }
}
