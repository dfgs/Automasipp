using Automasipp.backend.DataSources;
using Automasipp.Backend;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NReco.Logging.File;
using System;





var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Services.AddLogging(loggingBuilder => {
    var loggingSection = builder.Configuration.GetSection("Logging");
    loggingBuilder.AddFile(loggingSection);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string scenariosFolder = builder.Configuration.GetValue<string>("ScenariosFolder") ?? "/opt/sipp/Scenarios";
string sippFolder = builder.Configuration.GetValue<string>("SippFolder") ?? "/opt/sipp";
string sessionsFolder = builder.Configuration.GetValue<string>("SessionsFolder") ?? "/opt/sipp/Sessions";
string reportsFolder = builder.Configuration.GetValue<string>("ReportsFolder") ?? "/opt/sipp/Reports";


builder.Services.AddSingleton<IScenarioDataSource, ScenarioDataSource>((serviceProvider) => new ScenarioDataSource(builder.CreateLogger<ScenarioDataSource>(), scenariosFolder));
builder.Services.AddSingleton<ISessionDataSource, SessionDataSource>((serviceProvider) => new SessionDataSource(builder.CreateLogger<SessionDataSource>(), sippFolder,sessionsFolder, scenariosFolder,reportsFolder));


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
