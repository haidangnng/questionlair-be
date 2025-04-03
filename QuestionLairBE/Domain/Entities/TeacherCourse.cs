namespace QuestionLairBE.Domain.Entities;
public class TeacherCourse
{
    public int TeacherProfileId { get; set; }
    public TeacherProfile TeacherProfile { get; set; } = default!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;
}
