namespace Rentful.Infrastructure.Consumers.Dtos
{
    public class SendNotify
    {
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderFirstName { get; set; } = string.Empty;
        public string SenderLastName { get; set; } = string.Empty;
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
