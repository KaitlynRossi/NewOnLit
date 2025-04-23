using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASPProject.Models;
public class CheckoutViewModel
{
    public List<ShoppingCart> CartItems { get; set; }
    public decimal Total { get; set; }
    public decimal Tax { get; set; }
    public decimal Shipping { get; set; }
    public decimal GrandTotal { get; set; }
}