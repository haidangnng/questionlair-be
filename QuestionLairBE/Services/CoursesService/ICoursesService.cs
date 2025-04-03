namespace QuestionLairBE.Services.CourseService;
using QuestionLairBE.Application.DTOs.Course;
using QuestionLairBE.Domain.Entities;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(CourseCreateDto dto, int teacherUserId);
    Task EnrollStudentAsync(int courseId, int studentUserId);
    Task<List<Course>> GetCoursesForStudent(int studentUserId);
    Task<List<Course>> GetCoursesForTeacher(int teacherUserId);
}