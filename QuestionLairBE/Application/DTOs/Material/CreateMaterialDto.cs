namespace QuestionLairBE.Application.DTOs.Material;

public class CreateMaterialDto
{
      public int CourseId { get; set; }
      
      public List<IFormFile> Files { get; set; } = [];
}
