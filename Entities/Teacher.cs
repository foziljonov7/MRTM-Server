using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;

public class Teacher
{
    [Key, Required]
    public Guid Id { get; set; }
    [Required, MaxLength(125)]
    public string Fullname { get; set; }
    [Required]
    public string Skills { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();
}