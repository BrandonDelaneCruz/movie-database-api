using Microsoft.EntityFrameworkCore;
using MovieDatabaseApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var myDatabaseName = typeof(Program).Assembly.GetName().Name;
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer($"Server=localhost;Database={myDatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
