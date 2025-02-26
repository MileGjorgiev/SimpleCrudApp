
using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.DAL.Abstract;
using SimpleCrudApp.Models.DTO;
using SimpleCrudApp.Models.Entities;

namespace SimpleCrudApp.BLL.Concrete
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
       
        public async Task<List<Project>> GetAllAsync(string UserId)
        {
            return await _projectRepository.GetAllAsync(UserId);
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _projectRepository.GetAsync(id);
        }

        public async Task<int> SaveAsync(Project project, string userId)
        {
            return await _projectRepository.SaveAsync(project, userId);
        }
        public async Task DeleteAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }

    }
}
