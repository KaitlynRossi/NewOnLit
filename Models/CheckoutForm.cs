using System.ComponentModel.DataAnnotations;

public class CheckoutFormModel
{
    [Required]
    public required string FullName { get; set; }

    [Required]
    [CreditCard]
    public required string CreditCardNumber { get; set; }

    [Required]
    public required string ExpirationDate { get; set; }

    [Required]
    public required string CVV { get; set; }
}