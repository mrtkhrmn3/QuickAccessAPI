namespace QuickAccessAPI.Dto
{
    public class NotificationUpdateDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
    }
}
