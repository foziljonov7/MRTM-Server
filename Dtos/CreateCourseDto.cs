namespace WebApi.Dtos;

public class  CreateCourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string price { get; set; }
    public string Davomiylik { get; set; }
    public string Description { get; set; }
}