using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Models;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Api.Orders;

public class OrdersProvider : IOrdersProvider
{
	private readonly OrdersDbContext dbContext;
	private readonly ILogger<OrdersProvider> logger;
	private readonly IMapper mapper;
	public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.logger = logger;
		this.mapper = mapper;
		
		SeedData();
	}
	private void SeedData()
	{
		if (!dbContext.Orders.Any())
		{
			dbContext.Add(new Db.Order() { Id = 1, CustomerId = 2, TotalAmount = 100.00 } );
			dbContext.Add(new Db.Order() { Id = 2, CustomerId = 3, TotalAmount = 100.00 } );
			dbContext.Add(new Db.Order() { Id = 3, CustomerId = 4, TotalAmount = 100.00 } );


			dbContext.SaveChanges();
			logger.LogInformation("Seed data for orders table has been added.");
		}
	}
	
	
		public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
	{
		try
		{
			var orders = dbContext.Orders.ToList();
			
			if(orders != null && orders.Any())
			{
				var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
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

	public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int customerId)
	{
	  	try 
		{
			var order = await dbContext.Orders.FirstOrDefaultAsync(p=> p.CustomerId == customerId);
			
			if(order!= null)
			{
				var result = mapper.Map<Db.Order, Models.Order>(order);
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
