using QuickAccessAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class SiteManager
{
    [Key, ForeignKey("User")]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string SiteName { get; set; }
}
