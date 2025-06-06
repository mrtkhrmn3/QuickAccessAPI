namespace QuickAccessAPI.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public string? SiteName { get; set; }
        public string? Block { get; set; }
        public int? AptNo { get; set; }
        public string? PhoneNumber { get; set; }
    }

}
