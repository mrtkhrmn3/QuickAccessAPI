﻿namespace QuickAccessAPI.Dto
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
