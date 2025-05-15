using System.Diagnostics;
using Tinkr.Api.Authorization;
using Tinkr.Api.Dtos;
using Tinkr.Api.Entities;
using Tinkr.Api.Repositories;

namespace Tinkr.Api.TinkrEndPoints;
public static class ProjectsEndpoints
{
    const string GetProjectEndpointName = "GetProject";

    public static RouteGroupBuilder MapProjectsEndpoints(this IEndpointRouteBuilder routes)
    {
        var projectsGroup = routes.MapGroup("/projects")
                        .WithParameterValidation();


        projectsGroup.MapGet("/", async (IProjectsRepository projectsRepository, ILoggerFactory loggerFactory) =>
        {
            try
            {
                var logger = loggerFactory.CreateLogger("ProjectsLogger");
                logger.LogInformation(10, "Retrieving all projects in machine: {MachineName}. TraceId: {TraceId}", Environment.MachineName, Activity.Current?.TraceId);
                return (await projectsRepository.GetAllAsync()).Select(project => project.AsDto());
            }

            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger("ProjectsLogger");
                logger.LogError(ex, "Error retrieving projects in machine: {MachineName}. TraceId: {TraceId}", Environment.MachineName, Activity.Current?.TraceId);
                throw;
            }
        });

        projectsGroup.MapGet("/{id}", async (IProjectsRepository projectsRepository, int id, ILoggerFactory lFactory) =>
        {
            try
            {
                Project? project = await projectsRepository.GetAsync(id);
                return project is not null ? Results.Ok(project.AsDto()) : Results.NotFound();
            }
            catch (Exception ex)
            {
                var logger = lFactory.CreateLogger("ProjectsLogger");
                logger.LogError(ex, "Error retrieving project with id: {Id} in machine: {MachineName}. TraceId: {TraceId}",
                 id, Environment.MachineName, Activity.Current?.TraceId);

                return Results.Problem(title: "An error occurred while retrieving the project.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        { "traceId", Activity.Current?.TraceId.ToString()}
                    });

            }

        }).WithName(GetProjectEndpointName)
        .RequireAuthorization(Policies.ReadAccess);


        projectsGroup.MapPost("/", async (IProjectsRepository projectsRepository, CreateProjectDto projectDto) =>
        {
            Project project = new()
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                Price = projectDto.Price,
                InitDate = projectDto.InitDate,
                ImageUri = projectDto.ImageUrl
            };
            await projectsRepository.CreateAsync(project);
            return Results.CreatedAtRoute(GetProjectEndpointName, new { id = project.Id }, project);
        }).RequireAuthorization(Policies.WriteAccess);

        projectsGroup.MapPut("/{id}", async (IProjectsRepository projectsRepository, int id, UpdateProjectDto updatedProjectDto) =>
        {
            Project? existingProject = await projectsRepository.GetAsync(id);
            if (existingProject == null)
            {
                return Results.NotFound();
            }
            existingProject.Name = updatedProjectDto.Name;
            existingProject.Price = updatedProjectDto.Price;
            existingProject.Description = updatedProjectDto.Description;
            existingProject.InitDate = updatedProjectDto.InitDate;
            existingProject.ImageUri = updatedProjectDto.ImageUrl;
            await projectsRepository.UpdateAsync(existingProject);

            return Results.NoContent();
        }).RequireAuthorization(Policies.WriteAccess);

        projectsGroup.MapDelete("/{id}", async (IProjectsRepository projectsRepository, int id) =>
        {
            Project? removedProject = await projectsRepository.GetAsync(id);
            if (removedProject != null)
            {
                await projectsRepository.DeleteAsync(id);
                return Results.Ok(removedProject);
            }
            return Results.NoContent();
        }).RequireAuthorization();
        return projectsGroup;
    }
}