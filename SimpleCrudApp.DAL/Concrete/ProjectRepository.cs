
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleCrudApp.DAL.Abstract;
using SimpleCrudApp.Models.DTO;
using SimpleCrudApp.Models.Entities;
using System.Diagnostics.Metrics;

namespace SimpleCrudApp.DAL.Concrete
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;


        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        

        public async Task<List<Project>> GetAllAsync(string UserId)
        {
            var projects = await _context.Projects
                               .Where(p => p.UserId == UserId) 
                               .ToListAsync();

            return projects;
        }

        public async Task<Project> GetAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            return project;
        }

        public async Task<int> SaveAsync(Project project, string userId)
        {
            if (project.ProjectId > 0)
            {
                var exists = await _context.Projects.AnyAsync(p => p.ProjectId == project.ProjectId);
                if (!exists)
                {
                    throw new KeyNotFoundException($"Project with ID {project.ProjectId} not found.");
                }
            }

            project.UserId = userId;


            _context.Entry(project).State = project.ProjectId > 0 ? EntityState.Modified : EntityState.Added;
            await _context.SaveChangesAsync();

            return project.ProjectId;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id)
                ?? throw new KeyNotFoundException($"Project with ID {id} not found.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
