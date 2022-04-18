using CountriesStructure.API.Data;
using CountriesStructure.API.Services.Implementations;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    opt.SerializerSettings.Formatting = Formatting.Indented;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CountryContext>(op => op.UseInMemoryDatabase("movies"));
builder.Services.AddSingleton<IContinentStructure, ContinentStructure>();
builder.Services.AddScoped<IContinentRepository, ContinentRepository>();



var app = builder.Build();
using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<CountryContext>();
CountriesData.SeedCountriesInMemoryData(context);


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
