namespace Ecommerce.Api.Orders.Db;

public class Order
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public double TotalAmount { get; set; }
	
}
