﻿using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Products;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
	private readonly IProductsProvider _productsProvider;
	public ProductsController(IProductsProvider productsProvider)
	{
		_productsProvider = productsProvider;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetProductsAsync()
	{
		var result = await _productsProvider.GetProductsAsync();
		if (result.IsSuccess)
		{
			return Ok(result.Products);
		}
		return NotFound();
	}
	
	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductAsync(int id)
	{
		var result = await _productsProvider.GetProductAsync(id);
		if (result.IsSuccess)
		{
			return Ok(result.Product);
		}
		return NotFound();
	}
}
