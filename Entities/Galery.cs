using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class Galery
{
    [Key, Required]
    public Guid Id { get; set; }
    [Required, MaxLength(125)]
    public string Name { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}