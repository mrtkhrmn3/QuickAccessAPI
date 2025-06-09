using System.ComponentModel.DataAnnotations;

namespace QuickAccessAPI.Dto
{
    public class AdminDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Role {  get; set; }
    }
}
