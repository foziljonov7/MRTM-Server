using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;

namespace WebApi.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public TeacherController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateTeacherDto createTeacher)
    {
        if (await dbContext.Teachers.AnyAsync(t => t.Fullname.ToLower() == createTeacher.Fullname.ToLower()))
            return BadRequest("Teacher with this Fullname exists");

        if (await dbContext.Teachers.AnyAsync(t => t.PhoneNumber.ToLower() == createTeacher.PhoneNumber.ToLower()))
            return BadRequest("Teacher with this Phone number exists");

        var Teacher = dbContext.Teachers.Add(new Entities.Teacher
        {
            Id = Guid.NewGuid(),
            Fullname = createTeacher.Fullname,
            Skills = createTeacher.Skills,
            Age = createTeacher.Age,
            PhoneNumber = createTeacher.PhoneNumber
        });

        await dbContext.SaveChangesAsync();
        return Ok(Teacher.Entity.Id);
    }
    [HttpGet("{id}")]
    public IActionResult GetTeacher([FromRoute] Guid id)
    {
        var teacherId = dbContext.Teachers
            .Where(t => t.Id == id)
            .Include(c => c.Courses)
            .FirstOrDefault();

        if (teacherId is null)
            return NotFound();

        return Ok(new GetTeacherDto(teacherId));
    }
    [HttpGet]
    public async Task<IActionResult> GetTeachers([FromQuery] string search)
    {
        var teacaherquary = dbContext.Teachers.AsQueryable();

        if (false == string.IsNullOrEmpty(search))
            teacaherquary = teacaherquary.Where(i =>
            i.Fullname.ToLower().Contains(search.ToLower()));

        var teachers = await teacaherquary
            .Select(t => new GetTeacherDto(t))
            .ToListAsync();

        return Ok(teachers);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateTeacherDto updateTeacher)
    {
        var teachers = await dbContext.Teachers
            .FirstOrDefaultAsync(t => t.Id == id);

        if (teachers is null)
            return NotFound();

        if (await dbContext.Teachers.AnyAsync(t => t.Fullname.ToLower() == updateTeacher.Fullname.ToLower()))
            return Conflict("Teacher with this Fullname exists");

        if (await dbContext.Teachers.AnyAsync(t => t.PhoneNumber.ToLower() == updateTeacher.PhoneNumber.ToLower()))
            return Conflict("Teacher with this Phone number exists");

        teachers.Fullname = updateTeacher.Fullname;
        teachers.Skills = updateTeacher.Skills;
        teachers.Age = updateTeacher.Age;
        teachers.PhoneNumber = updateTeacher.PhoneNumber;

        await dbContext.SaveChangesAsync();
        return Ok(teachers.Id);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var teachers = await dbContext.Teachers
            .FirstOrDefaultAsync(t => t.Id == id);
        if (teachers is null)
            return NotFound();

        dbContext.Teachers.Remove(teachers);

        await dbContext.SaveChangesAsync();
        return Ok(teachers.Id);
    }
}