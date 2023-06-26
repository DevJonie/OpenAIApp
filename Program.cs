using OpenAIApp.Configurations;
using OpenAIApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OpenAiConfig>(builder.Configuration.GetSection(nameof(OpenAiConfig)));
builder.Services.AddScoped<IOpenAiService, OpenAiService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("completion", async (IOpenAiService openAiService, string prompt) => 
{
    var response = await openAiService.GetCompletionAsync(prompt);
    app.Logger.LogInformation("Prompt: {prompt} \nCompletion: {completion}", prompt, response);
    return Results.Ok(response);
});

app.MapPost("advanced-completion", async (IOpenAiService openAiService, string prompt) => 
{
    var response = await openAiService.GetAdvancedCompletionAsync(prompt);
    app.Logger.LogInformation("Prompt: {prompt} \nCompletion: {completion}", prompt, response);
    return Results.Ok(response);
});

app.MapPost("check-programming-lang", async (IOpenAiService openAiService, string prompt) => 
{
    var response = await openAiService.CheckProgrammingLanguage(prompt);
    app.Logger.LogInformation("Prompt: {prompt} \nCompletion: {completion}", prompt, response);
    return Results.Ok(response);
});

app.Run();
