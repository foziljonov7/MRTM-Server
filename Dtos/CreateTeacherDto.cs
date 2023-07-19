namespace WebApi.Dtos;

public class CreateTeacherDto
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Skills { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
}