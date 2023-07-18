using WebApi.Entities;

namespace WebApi.Dtos;

public class GetUserDto
{
    public GetUserDto(User entity)
    {
        Id = entity.Id;
        Fullname = entity.Fullname;
        Username = entity.Username;
        PhoneNumber = entity.PhoneNumber;
        Age = entity.Age;
        Location = entity.Location;
    }

    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }

    public string PhoneNumber { get; set; }
    public string Age { get; set; }
    public string Location { get; set; }
    public GetCourseDto Courses { get; set; }
}