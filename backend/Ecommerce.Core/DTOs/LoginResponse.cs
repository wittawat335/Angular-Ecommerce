﻿
namespace Ecommerce.Core.DTOs
{
    public class LoginResponse
    {
        public string userId { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
    }
}
