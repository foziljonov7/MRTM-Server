using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class Course
{
    [Key, Required]
    public Guid Id { get; set; }
    [Required, MaxLength(125)]
    public string Name { get; set; }
    [Required]
    public string price { get; set; }
    [Required]
    public string Davomiylik { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; } = new List<User>();
    public virtual Teacher Teacher { get; set; }

}