using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class User
{
    [Key, Required]
    public Guid Id { get; set; }
    [Required, MaxLength(125)]
    public string Fullname { get; set; }
    [Required, MaxLength(20)]
    public string Username { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public string Age { get; set; }
    public string Location { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();
}