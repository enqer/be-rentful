using Rentful.Domain.Entities;
using Rentful.Domain.Entities.Enums;

namespace Rentful.Tests.Controllers.Reservations.Factories
{
    internal class ReservationsRequestFactory
    {
        public static Common.Models.User CreateAuthUser()
        {
            return new Common.Models.User
            {
                Id = 2
            };
        }

        public static User CreateUser()
        {
            return new User
            {
                Id = 2,
                Email = "test2@wp.pl",
                Password = "$2a$11$tuhS17EwyCAYsz1PMOwKlOCp4Brf.HMbaqjAc/7MwOyvJsF5lzro."
            };
        }

        public static List<Reservation> CreateReservations()
        {
            return new List<Reservation>
            {
                new Reservation
                {
                    Id = 1,
                    Date = "2024-12-12 12:12",
                    Status = ReservationStatusEnum.Unresolved,
                    User = null
                }
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
