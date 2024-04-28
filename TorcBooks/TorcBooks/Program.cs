using Microsoft.EntityFrameworkCore;
using System;
using TorcBooks.Configuration;
using TorcBooks.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TorcDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString(nameof(TorcDbContext))));
builder.Services.RegisterRepositories();

var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.Equals("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TorcDbContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Tests purpose
public partial class Program { }