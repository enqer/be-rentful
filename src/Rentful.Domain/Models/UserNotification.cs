﻿namespace Rentful.MassTransit.Models
{
    public class UserNotification
    {
        public List<string> Recipients { get; set; } = new List<string>();
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderFirstName { get; set; } = string.Empty;
        public string SenderLastName { get; set; } = string.Empty;
    }
}
