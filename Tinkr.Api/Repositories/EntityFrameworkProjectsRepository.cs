using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tinkr.Api.Data;
using Tinkr.Api.Entities;

namespace Tinkr.Api.Repositories;

public class EntityFrameworkProjectsRepository : IProjectsRepository
{
    private readonly TinkrProjectsContext _dbContext;
    private readonly ILogger<EntityFrameworkProjectsRepository> _logger;
    public EntityFrameworkProjectsRepository(TinkrProjectsContext dbContext, ILogger<EntityFrameworkProjectsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbContext.Projects.AsNoTracking().ToListAsync();
    }

    public async Task<Project?> GetAsync(int id)
    {
        return await _dbContext.Projects.FindAsync(id);
    }

    public async Task CreateAsync(Project project)
    {
        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Project Id:{Id}, Name: {Name} CREATED", project.Id, project.Name);
    }

    public async Task UpdateAsync(Project updatedProject)
    {
        _dbContext.Update(updatedProject);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Project Id:{Id}, Name: {Name} UPDATED", updatedProject.Id, updatedProject.Name);
    }
    public async Task DeleteAsync(int id)
    {
        _dbContext.Projects.Where(p => p.Id == id).ExecuteDelete();
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Project Id:{id} DELETED", id);
    }
}