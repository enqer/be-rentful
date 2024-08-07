﻿namespace Rentful.Domain.Options
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int TimelifeInMinutes { get; set; }
    }
}
