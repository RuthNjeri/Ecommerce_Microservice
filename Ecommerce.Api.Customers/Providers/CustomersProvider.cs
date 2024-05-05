using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers;

public class CustomersProvider : ICustomersProvider
{
	private readonly CustomersDbContext dbContext;
	private  readonly ILogger<CustomersProvider> logger;
	private readonly IMapper mapper;

	public CustomersProvider(CustomersDbContext dbContext, IMapper mapper, ILogger<CustomersProvider> logger)
	{
		this.dbContext = dbContext;
		this.logger = logger;
		this.mapper = mapper;
		
		SeedData();
	}
	
	private void SeedData()
	{
		if (!dbContext.Customers.Any())
		{
			dbContext.Customers.Add(new Db.Customer () { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });
			dbContext.Customers.Add(new Db.Customer () { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" });
			dbContext.Customers.Add(new Db.Customer () { Id = 3, Name = "Mike Johnson", Email = "mike.johnson@example.com" });

			dbContext.SaveChanges();
		}
	}

	public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
	{
		try
		{
			var customers = dbContext.Customers.ToList();
			
			if(customers != null && customers.Any())
			{
				var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
				return (true, result, null);
			}
			
			return (false, null, "Not Found");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while getting customers.");
			throw;
		}
	}

	public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
	{
	  	try 
		{
			var customer = await dbContext.Customers.FirstOrDefaultAsync(p=> p.Id == id);
			
			if(customer!= null)
			{
				var result = mapper.Map<Db.Customer, Models.Customer>(customer);
				return (true, result, null);
			}
			return (false, null, "Not Found");
		}
		catch(Exception ex)
		{
			logger?.LogError(ex.ToString());
			return (false, null, ex.Message);
		}
	}
	
}
