using System.Threading.Tasks;
using Tinkr.Api.Entities;

namespace Tinkr.Api.Repositories;

public class InMemProjectsRepository : IProjectsRepository
{
    private readonly List<Project> Projects = new List<Project>()
    { new Project()
        {
            Id = 1,
            Name = "Solar System",
            Description = "Project to build a mechanical solar system",
            Price = 9.990M,
            InitDate = new DateTime(2025, 05, 10),
            ImageUri = "https://placehold.co/100"
        },
        new Project()
        {
            Id = 2,
            Name = "Opera House",
            Description = "Project to recreate an opera",
            Price = 9.990M,
            InitDate = new DateTime(2025, 05, 15),
            ImageUri = "https://placehold.co/100"
        },

        new Project()
        {
            Id = 3,
            Name = "Master of Puppets",
            Description = "Create a Metallica album cover, with puppets",
            Price = 9.990M,
            InitDate = new DateTime(2025, 05, 15),
            ImageUri = "https://placehold.co/100"
        }
    };

    public Task<IEnumerable<Project>> GetAllAsync()
    {
        return Task.FromResult(Projects.AsEnumerable());
    }

    public async Task<Project?> GetAsync(int id)
    {
        return await Task.FromResult(Projects.Find(project => project.Id == id));
    }
    public async Task CreateAsync(Project project)
    {
        project.Id = Projects.Max(project => project.Id) + 1;
        Projects.Add(project);
        await Task.CompletedTask;
    }
    public async Task UpdateAsync(Project updatedProject)
    {
        var index = Projects.FindIndex(p => p.Id == updatedProject.Id);
        Projects[index] = updatedProject;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = Projects.FindIndex(p => p.Id == id);
        Projects.RemoveAt(index);
        await Task.CompletedTask;
    }
}