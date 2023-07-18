using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base (options) { }
}