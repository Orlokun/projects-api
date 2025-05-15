using System.ComponentModel.DataAnnotations;

namespace Tinkr.Api.Dtos;

public record ProjectDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    DateTime InitDate,
    string ImageUrl
);

public record CreateProjectDto(
    [Required][StringLength(50)] string Name,
    string Description,
    decimal Price,
    DateTime InitDate,
    string ImageUrl
);

public record UpdateProjectDto(
    [Required][StringLength(50)] string Name,
    string Description,
    decimal Price,
    DateTime InitDate,
    string ImageUrl
);