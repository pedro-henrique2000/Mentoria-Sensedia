using Application.AdapterInbound.Rest.Filter;
using Application.Configs;
using Domain.Services;
using Domain.Services.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure.Database.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(jsonOpts =>
    {
        jsonOpts.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<ITodoService, TodoService>();
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
}).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
