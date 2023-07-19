using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
}