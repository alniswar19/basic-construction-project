var builder = WebApplication.CreateBuilder(args);

builder.AddSqlServerClient("bciDb");

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/projects", () =>
{

})
.WithName("GetAllProjects")
.WithDescription("Return all construction Project");

app.MapGet("/project/{id}", () =>
{

})
.WithName("GetProjectbyId")
.WithDescription("Get Construction Project by Id");

app.MapPost("/project", () =>
{

})
.WithName("CreateProject")
.WithDescription("Create new constraction project");

app.MapPut("/projects/{id}", () =>
{

})
.WithName("UpdateProjectById")
.WithDescription("Update the project by the given id");

app.MapDefaultEndpoints();

app.Run();
