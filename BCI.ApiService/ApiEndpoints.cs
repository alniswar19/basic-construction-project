using BCI.Domain.Entities;
using BCI.Domain.QueryFilters;
using BCI.Infrastructure.Persistances;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

namespace BCI.ApiService;

public static class ApiEndpoints
{
    public static WebApplication MapConstructionApi(this WebApplication app)
    {
        app.MapGet("/projects", FindAllProject)
        .WithName("GetAllProjects")
        .WithDescription("Return all construction Project");

        app.MapGet("/project/{id}", FindById)
        .WithName("GetProjectbyId")
        .WithDescription("Get Construction Project by Id");

        app.MapPost("/project", CreateNewProject)
        .Produces<ConstructionProject>(StatusCodes.Status200OK)
        .WithName("CreateProject")
        .WithDescription("Create new constraction project");

        app.MapPut("/projects/{id}", UpdateProject)
        .WithName("UpdateProjectById")
        .WithDescription("Update the project by the given id");

        return app;
    }

    private static async Task<IResult> FindAllProject([FromServices] ConstructionProjectRepository service, [AsParameters] ConstructionProjectFilter filter)
    {
        var result = await service.FindAll(filter);

        return TypedResults.Ok(new
        {
            data = result.Item1,
            totalCount = result.Item2,
        });
    }

    private static async Task<IResult> FindById([FromServices] ConstructionProjectRepository service, int id)
    {
        return await service.Get(id) is ConstructionProject result ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    private static async Task<IResult> CreateNewProject([FromServices] ConstructionProjectRepository service, [FromBody] ConstructionProject data)
    {
        if (!MiniValidator.TryValidate(data, out var errors))
        {
            return Results.ValidationProblem(errors);
        }
        else
        {
            var result = await service.Add(data);
            return TypedResults.Created($"/project/{result.Id}", result);
        }
    }

    private static async Task<IResult> UpdateProject([FromServices] ConstructionProjectRepository service, int id, ConstructionProject data)
    {
        return TypedResults.Ok(await service.Update(data with { Id = id }));
    }
}
