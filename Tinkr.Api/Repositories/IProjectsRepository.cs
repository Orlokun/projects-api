using Tinkr.Api.Entities;

namespace Tinkr.Api.Repositories;

public interface IProjectsRepository
{
    Task CreateAsync(Project project);
    Task DeleteAsync(int id);
    Task<Project?> GetAsync(int id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task UpdateAsync(Project updatedProject);
}
