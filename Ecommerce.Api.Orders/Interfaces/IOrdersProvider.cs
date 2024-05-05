namespace Ecommerce.Api.Orders.Interfaces;

public interface IOrdersProvider
{
	Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();
	Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id);
}
