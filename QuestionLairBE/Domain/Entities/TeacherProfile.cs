namespace QuestionLairBE.Domain.Entities;

public class TeacherProfile
{
    public int Id { get; set; }

    public string Title { get; set; } = String.Empty;
    public string Bio { get; set; } = String.Empty;
    public string Office { get; set; } = String.Empty;



    public List<TeacherCourse> Courses { get; set; } = new();
    public required User User { get; set; }
}