﻿namespace Ecommerce.Api.Orders.Models;

public class Order
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public DateTime OrderDate { get; set; }
}