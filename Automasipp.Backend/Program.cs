using Automasipp.backend.DataSources;
using Automasipp.Backend;
using Microsoft.Extensions.Hosting;
using System;





var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string scenariosFolder= builder.Configuration.GetValue<string>("ScenariosFolder")??"/opt/sipp";


builder.Services.AddSingleton<IScenarioDataSource, ScenarioDataSource>((serviceProvider)=> new ScenarioDataSource(builder.CreateLogger<ScenarioDataSource>(), scenariosFolder));


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
