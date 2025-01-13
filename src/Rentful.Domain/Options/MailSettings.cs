namespace Rentful.Domain.Options
{
    public class MailSettings
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Smtp { get; set; } = string.Empty;
    }
}
