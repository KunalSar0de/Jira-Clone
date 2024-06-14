using HashidsNet;
using Jira;
using Jira.EFCore;
using Jira.Exceptions;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Jira.Validators;
using FluentValidation;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Jira.Filters;
using JiraAPI.Middleware;
using Jira.Services;
using Jira.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHashids>(_ => new Hashids("SECURE-SALT", 5));
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
builder.Services.AddTransient<CustomExceptionMiddleware>();


// fluent validation filter
FluentValidationExceptionFilter(builder);

AddDbContext(builder);

ServiceRegistration.RegisterServices(builder.Services);
builder.Services.AddAuthorization();
builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<CustomExceptionMiddleware>();

app.MapControllers();

app.Run();

static void AddDbContext(WebApplicationBuilder builder)
{
    string connectionString = builder.Configuration.GetConnectionString("JiraDatabase");
    builder.Services.AddDbContext<JiraDbContext>
    (options => options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        x => x.MigrationsAssembly("jiraAPI"))
    );
}

static void FluentValidationExceptionFilter(WebApplicationBuilder builder)
{
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddMvc(options =>
    {
        options.Filters.Add(typeof(FluentValidationFilter));
    })
    .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Program>());

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}