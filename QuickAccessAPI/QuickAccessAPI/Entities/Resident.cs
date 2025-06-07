using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Resident
{
    [Key, ForeignKey("User")]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Block { get; set; }

    [Required]
    public int AptNo { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string SiteName { get; set; }
}
