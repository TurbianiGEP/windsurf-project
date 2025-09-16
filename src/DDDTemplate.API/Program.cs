using Microsoft.EntityFrameworkCore;
using DDDTemplate.Application.Commands;
using DDDTemplate.Application.Queries;
using DDDTemplate.Application.DTOs;
using DDDTemplate.Domain.Entities;
using DDDTemplate.Domain.Interfaces;
using DDDTemplate.Infrastructure.Data;
using DDDTemplate.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("DDDTemplateDb")); // Using InMemory for demo purposes

// Register repositories
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();

// Register command handlers
builder.Services.AddScoped<ICommandHandler<CreateProductCommand, ProductDto>, CreateProductCommandHandler>();

// Register query handlers
builder.Services.AddScoped<IQueryHandler<GetProductByIdQuery, ProductDto>, GetProductByIdQueryHandler>();

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
