using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionLairBE.Application.DTOs.Course;
using QuestionLairBE.Services.CourseService;
using System.Security.Claims;

namespace QuestionLairBE.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // ========================
    // Professors: Create Course
    // ========================
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateCourse([FromBody] CourseCreateDto dto)
    {
        var course = await _courseService.CreateCourseAsync(dto, GetUserId());
        return Ok(course);
    }

    // ========================
    // Students: Enroll
    // ========================
    [HttpPost("{courseId}/enroll")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> EnrollStudent(int courseId)
    {
        await _courseService.EnrollStudentAsync(courseId, GetUserId());
        return Ok(new { message = "Enrolled successfully" });
    }


    // ========================
    // Get Courses
    // ========================
    [HttpGet("my")]
    public async Task<IActionResult> GetMyCourses()
    {
        var userId = GetUserId();

        if (User.IsInRole("Student"))
        {
            var courses = await _courseService.GetCoursesForStudent(userId);
            return Ok(courses);
        }

        if (User.IsInRole("Teacher"))
        {
            var courses = await _courseService.GetCoursesForTeacher(userId);
            return Ok(courses);
        }

        return Forbid("Only students and teachers can access this endpoint.");
    }
}

