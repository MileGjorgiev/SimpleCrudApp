using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.BLL.Abstract
{
    public interface IProjectTaskService
    {
        Task<List<ProjectTask>> GetAllAsync();
        Task<List<ProjectTask>> GetAllByProjectIdAsync(int projectId);
        Task<ProjectTask> GetAsync(int id);
        Task<int> SaveAsync(ProjectTask projectTask);
        Task DeleteAsync(int id);
    }
}
