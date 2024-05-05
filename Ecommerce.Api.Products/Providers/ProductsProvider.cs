﻿using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products;

public class ProductsProvider : IProductsProvider
{
	private readonly ProductsDbcontext dbContext;
	private readonly ILogger<ProductsProvider> logger;
	private readonly IMapper mapper;
	public ProductsProvider(ProductsDbcontext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.logger = logger;
		this.mapper = mapper;
		
		SeedData();
	}

	private void SeedData()
	{
		if(!dbContext.Products.Any())
		{
			dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 300, Inventory = 30});
			dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 200, Inventory = 20});
			dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 500, Inventory = 40});
			dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 700, Inventory = 50});
			dbContext.SaveChanges();
		}
	}

	public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
	{
		try 
		{
			var products = dbContext.Products.ToList();
			if(products != null && products.Any())
			{
				var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
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

	public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
	{
	  	try 
		{
			var product = await dbContext.Products.FirstOrDefaultAsync(p=> p.Id == id);
			
			if(product!= null)
			{
				var result = mapper.Map<Db.Product, Models.Product>(product);
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
