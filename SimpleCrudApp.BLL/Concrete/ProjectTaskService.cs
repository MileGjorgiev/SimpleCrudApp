using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.DAL.Abstract;
using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.BLL.Concrete
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<List<ProjectTask>> GetAllAsync()
        {
            return await _projectTaskRepository.GetAllAsync();
        }

        public async Task<List<ProjectTask>> GetAllByProjectIdAsync(int projectId)
        {
            return await _projectTaskRepository.GetAllByProjectIdAsync(projectId);
        }

        public async Task<ProjectTask> GetAsync(int id)
        {
            return await _projectTaskRepository.GetAsync(id);
        }

        public async Task<int> SaveAsync(ProjectTask projectTask)
        {
            return await _projectTaskRepository.SaveAsync(projectTask);
        }

        public async Task DeleteAsync(int id)
        {
            await _projectTaskRepository.DeleteAsync(id);
        }

        
    }
}
