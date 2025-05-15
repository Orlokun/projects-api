using Tinkr.Api.Dtos;

namespace Tinkr.Api.Entities;

public static class EntityExtensions
{
    public static ProjectDto AsDto(this Project project)
    {
        return new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.Price,
            project.InitDate,
            project.ImageUri
        );
    }
}