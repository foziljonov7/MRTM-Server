using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Dtos;

namespace WebApi.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public CourseController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto createCourse)
    {
        if(await dbContext.Courses.anyasync(c => c.Name.ToLower() == createCourse.Name.ToLower()))
            return BadRequest("Course with this name already exists");
        
        var course = dbContext.Courses.Add(new Entities.Course
        {
            Id = Guid.NewGuid(),
            Name = createCourse.Name,
            price = createCourse.price,
            Description = createCourse.Description,
            Davomiylik = createCourse.Davomiylik
        }); 

        await dbContext.SaveChangesAsync();
        return Ok(course.Entity.Id);
    }
}