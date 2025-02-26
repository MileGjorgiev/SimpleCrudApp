using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.DAL.Abstract
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string UserId);
        Task<Project> GetAsync(int id);
        Task<int> SaveAsync(Project project, string userId);
        Task DeleteAsync(int id);
    }
}
