using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;

namespace WebApi.controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly AppDbContext dbContext;

    
    public UserController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto userDto)
    {
        if(await dbContext.Users.AnyAsync(u => u.Username.ToLower() == userDto.Username.ToLower()))
            return Conflict("User with this username exists");
            
        var created = dbContext.Users.Add(new Entities.User
        {
            Id = Guid.NewGuid(),
            Fullname = userDto.Fullname,
            Username = userDto.Username,
            PhoneNumber = userDto.PhoneNumber,
            Age = userDto.Age,
            Location = userDto.Location
        });

        await dbContext.SaveChangesAsync();
        return Ok(created.Entity.Id);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var userId = await dbContext.Users
            .Where(u => u.Id == id)
            .Include(c => c.Courses)
            .FirstOrDefaultAsync();

        if(userId is null)
            return NotFound();
        return Ok(new GetUserDto(userId));
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string search)
    {
        var usersquary = dbContext.Users.AsQueryable();

        if(false == string.IsNullOrWhiteSpace(search))
            usersquary = usersquary.Where(i =>
            i.Fullname.ToLower().Contains(search.ToLower()) ||
            i.Username.ToLower().Contains(search.ToLower()));

        var users = await usersquary
            .Select(i => new GetUserDto(i))
            .Include(c => c.Courses)
            .ToListAsync();

        return Ok(users);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromRoute]Guid id, UpdateUserDto userDto)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);
        if(user is null)
            return NotFound();
        if(await dbContext.Users.AnyAsync(u => u.Username.ToLower() == user.Username.ToLower()))
            return Conflict("User with this username exists");
        user.Fullname = userDto.Fullname;
        user.Username = userDto.Username;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Age = userDto.Age;
        user.Location = userDto.Location;
        
        await dbContext.SaveChangesAsync();
        return Ok(user.Id);
    } 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
    {    
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if(user is null) 
            return BadRequest("User not found");
        dbContext.Users.Remove(user);

        dbContext.SaveChanges();
        return Ok();
    }
}