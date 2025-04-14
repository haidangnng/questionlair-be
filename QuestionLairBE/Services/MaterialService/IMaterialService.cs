using QuestionLairBE.Application.DTOs.Material;
using QuestionLairBE.Domain.Entities;

namespace QuestionLairBE.Services.MaterialService;

public interface IMaterialService
{
  Task<List<Material>> CreateMaterial(int courseId, List<string> filePaths, int teacherUserId);
}
