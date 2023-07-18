namespace WebApi.Dtos;

public class CreateUserDto
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string Age { get; set; }
    public string Location { get; set; }
}