using LinkShortener.API;
using LinkShortener.API.DAL;
using LinkShortener.API.Entities;
using LinkShortener.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        /*.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500/")*/;
    });
});
var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<LinkShortenerDbContext>();
if (dbContext.Database.GetPendingMigrations() is not null)
{
    await dbContext.Database.MigrateAsync();
}
app.UseCors("FrontEnd");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.RegisterShortenUrlRequests();

app.Run();
