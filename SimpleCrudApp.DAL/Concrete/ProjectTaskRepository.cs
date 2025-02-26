
using Microsoft.EntityFrameworkCore;
using SimpleCrudApp.DAL.Abstract;
using SimpleCrudApp.Models.Entities;
using System.Diagnostics.Metrics;

namespace SimpleCrudApp.DAL.Concrete
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly AppDbContext _context;

        public ProjectTaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProjectTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<List<ProjectTask>> GetAllByProjectIdAsync(int projectId)
        {
            var tasks = await _context.Tasks
                              .Where(t => t.ProjectId == projectId)
                              .ToListAsync();

            return tasks;
        }

        public async Task<ProjectTask> GetAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            return task;
        }

        public async Task<int> SaveAsync(ProjectTask projectTask)
        {
            if (projectTask.TaskId > 0)
            {
                var exists = await _context.Tasks.AnyAsync(t => t.TaskId == projectTask.TaskId);
                if (!exists)
                {
                    throw new KeyNotFoundException($"Task with ID {projectTask.TaskId} not found.");
                }
            }
            _context.Entry(projectTask).State = projectTask.TaskId > 0 ? EntityState.Modified : EntityState.Added;
            await _context.SaveChangesAsync();

            return projectTask.TaskId;
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id)
                ?? throw new KeyNotFoundException($"Task with ID {id} not found.");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

    }
}
