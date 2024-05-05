namespace Ecommerce.Api.Search.Models;

public class Oder
{
	public int id { get; set; }
	public DateTime orderDate { get; set; }
	public string orderName { get; set; }
	public int customerId { get; set; }
}
