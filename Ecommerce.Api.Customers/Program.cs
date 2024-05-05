using AutoMapper;
using Ecommerce.Api.Customers;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<CustomersDbContext>(options => options.UseInMemoryDatabase("Customers"));


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
