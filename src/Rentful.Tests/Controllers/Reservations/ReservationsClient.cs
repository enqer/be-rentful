using Rentful.Tests.Common.Extensions;
using Rentful.Tests.Common.Models;

namespace Rentful.Tests.Controllers.Reservations
{
    public class ReservationsClient(HttpClient httpClient)
    {
        public async Task<HttpResponseMessage> AssignReservation(int reservationId, User? user = null)
        {
            httpClient.Auth(user);
            return await httpClient.PostAsync($"/api/v1/reservations/{reservationId}", null);
        }

        public async Task<HttpResponseMessage> GetUserReservations(User? user = null)
        {
            httpClient.Auth(user);
            return await httpClient.GetAsync($"/api/v1/reservations");
        }
        public async Task<HttpResponseMessage> CancelReservation(int reservationId, User? user = null)
        {
            httpClient.Auth(user);
            return await httpClient.PostAsync($"/api/v1/reservations/{reservationId}/cancel", null);
        }
    }
}
