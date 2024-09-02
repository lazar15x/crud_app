using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace server.Controllers;

public static class Endpoints
{
    public static void MapProgramEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Program").WithTags(nameof(Program));

        group.MapGet("/", () =>
        {
            return new [] { new Program() };
        })
        .WithName("GetAllPrograms")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Program { ID = id };
        })
        .WithName("GetProgramById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Program input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateProgram")
        .WithOpenApi();

        group.MapPost("/", (Program model) =>
        {
            //return TypedResults.Created($"/api/Programs/{model.ID}", model);
        })
        .WithName("CreateProgram")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Program { ID = id });
        })
        .WithName("DeleteProgram")
        .WithOpenApi();
    }
}
