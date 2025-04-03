namespace QuestionLairBE.Domain.Entities;

public class AdminProfile
{
    public int UserId { get; set; }
    public required User User { get; set; }
}