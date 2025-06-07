using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Notification
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }  // Bildirimi oluşturan kullanıcı

    [Required]
    public string Block { get; set; }
    [Required]
    public int AptNo { get; set; } 
    [Required]
    public string Type { get; set; }  // Örn: Food Delivery, Guest Coming

    [Required]
    public string Status { get; set; } = "Pending";  // Bildirimin durumu
    [Required]
    public string Description { get; set; }  // Detaylı bilgi
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]  
    public string SiteName { get; set; }  
}
