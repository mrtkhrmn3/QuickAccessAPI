using System.ComponentModel.DataAnnotations;

namespace QuickAccessAPI.Dto
{
    public class ResidentRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı.")]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Block { get; set; }

        [Required]
        public int AptNo { get; set; }

        [Required]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string SiteName { get; set; }
    }
}
