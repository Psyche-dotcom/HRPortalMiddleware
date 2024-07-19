using HRPortal.Core.Seeders;
using HRPortal.Extension;
using Zainnest.Api.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureLibrary(builder.Configuration);
builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddMemoryCache();

var app = builder.Build();

Seeder.SeedData(app).Wait();

// Configure the HTTP request pipeline.
app.UseCors();

/*if (app.Environment.IsDevelopment())
{*/
app.UseSwagger();
app.UseSwaggerUI();
/*}*/

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Bind the host to 0.0.0.0
/*if (app.Environment.IsProduction())
{
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Urls.Add($"http://0.0.0.0:{port}");
}*/
app.Run();
