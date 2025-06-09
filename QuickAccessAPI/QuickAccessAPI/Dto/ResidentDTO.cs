using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickAccessAPI.Dto
{
    public class ResidentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Block { get; set; }
        public int AptNo { get; set; }
        public string PhoneNumber { get; set; }
    }
}
