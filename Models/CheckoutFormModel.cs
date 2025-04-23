using System.ComponentModel.DataAnnotations;

public class CheckoutFormModel
{
    [Required]
    public string FullName { get; set; }

    [Required]
    [CreditCard]
    public string CreditCardNumber { get; set; }

    [Required]
    public string ExpirationDate { get; set; }

    [Required]
    public string CVV { get; set; }
}