﻿using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services;

public class SearchService : ISearchService
{
	private readonly IOrdersService _ordersService;
	
	public SearchService(IOrdersService ordersService)
	{
		this._ordersService = ordersService;
	}
	
	public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
	{
		var ordersResult = await _ordersService.GetOrdersAsync(customerId);
		if (ordersResult.IsSuccess)
		{
			var result = new
			{
				Orders = ordersResult.Orders
			};
			return (true, result);
		}
		return (false, null);
	}
}
