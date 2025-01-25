namespace Rentful.MassTransit.Models
{
    public class UserNotification
    {
        public List<string> Recepients { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
    }
}
