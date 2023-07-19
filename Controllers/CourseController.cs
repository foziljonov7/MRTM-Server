using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        if(await dbContext.Courses.AnyAsync(c => c.Name.ToLower() == createCourse.Name.ToLower()))
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse([FromRoute] Guid id)
    {
        var courseId = await dbContext.Courses
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if(courseId is null)
            return NotFound();
        return Ok(new GetCourseDto(courseId));
    }
    [HttpGet]
    public async Task<IActionResult> GetCourses([FromQuery] string search)
    {
        var coursesearch = dbContext.Courses.AsQueryable();

        if(!string.IsNullOrEmpty(search))
            coursesearch = coursesearch.Where(c =>
                c.Name.ToLower().Contains(search.ToLower()));
        
        var courses = await coursesearch
            .Select(c => new GetCourseDto(c))
            .ToListAsync();
        
        return Ok(courses);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Guid id, UpdateCourseDto updateCourse)
    {
        var course = await dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == id);
        if(course is null)
            return NotFound();
        if(await dbContext.Courses.AnyAsync(c => c.Name.ToLower() == updateCourse.Name.ToLower()))
            return BadRequest("Course with this name already exists");
        
        course.Name = updateCourse.Name;
        course.price = updateCourse.Price;
        course.Description = updateCourse.Description;
        course.Davomiylik = updateCourse.Davomiylik;

        await dbContext.SaveChangesAsync();
        return Ok(course.Id);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        var course = await dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == id);
        if(course is null)
            return NotFound();
        
        dbContext.Courses.Remove(course);
        return Ok(course.Id);
    }
}