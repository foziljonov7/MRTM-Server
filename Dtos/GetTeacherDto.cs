using WebApi.Entities;

namespace WebApi.Dtos;

public class GetTeacherDto
{
    private Teacher teacherId;

    public GetTeacherDto(Teacher teacherId)
    {
        this.teacherId = teacherId;
    }

    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Skills { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
}