using Ecommerce.Api.Search.Models;
namespace Ecommerce.Api.Search.Interfaces;


public interface IOrdersService
{
	Task<(bool IsSuccess, Oder Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
}
