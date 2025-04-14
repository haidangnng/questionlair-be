using QuestionLairBE.Application.DTOs.Material;
using QuestionLairBE.Domain.Entities;
using QuestionLairBE.Infrastructure.Data;

namespace QuestionLairBE.Services.MaterialService;

public class MaterialService : IMaterialService
{
  private readonly AppDbContext _context;

  public MaterialService(AppDbContext context)
  {
    _context = context;
  }

  public async  Task<List<Material>> CreateMaterial(int courseId, List<string> filePaths, int teacherUserId)
  {
    var materials = new List<Material>();
    
    foreach (var filePath in filePaths)
    {
      var material = new Material
      {
        CourseId = courseId,
        Url = filePath,
        UploadedBy = teacherUserId,
      };
      
      materials.Add(material);
    }
    
    await _context.Materials.AddRangeAsync(materials);
    await _context.SaveChangesAsync();

    return materials;
  }
}
