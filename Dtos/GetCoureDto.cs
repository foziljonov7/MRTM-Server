using WebApi.Entities;

namespace WebApi.Dtos;

public class GetCourseDto
{
    public GetCourseDto(Course entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Price = entity.price;
        Davomiylik = entity.Davomiylik;
        Description = entity.Description;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public string Davomiylik { get; set; }
    public string Description { get; set; }
}