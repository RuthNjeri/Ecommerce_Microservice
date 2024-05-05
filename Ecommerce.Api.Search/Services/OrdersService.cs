using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;
using System.Text;

namespace Ecommerce.Api.Search.Services;

public class OrdersService : IOrdersService
{
	
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<OrdersService> _logger;
	public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
	{
		_httpClientFactory = httpClientFactory;
		_logger = logger;
	}
	
	public async Task<(bool IsSuccess, Oder Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
	{
		try
		{
			var client = _httpClientFactory.CreateClient("OrdersService");
			var response = await client.GetAsync($"api/orders/{customerId}");
			if(response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsByteArrayAsync();
				string contentString = Encoding.UTF8.GetString(content);
				Console.WriteLine(contentString);
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
				var result = JsonSerializer.Deserialize<Oder>(content, options);
				return (true, result, null);
			}
			return (false, null, response.ReasonPhrase);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.ToString(), "An error occurred while getting orders.");
			return (false, null, ex.Message);
		}
	}
}
