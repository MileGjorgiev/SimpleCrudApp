using SimpleCrudApp.Models.DTO;
using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.BLL.Abstract
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync(string UserId);
        Task<Project> GetAsync(int id);
        Task<int> SaveAsync(Project project, string userId);
        Task DeleteAsync(int id);
    }
}
