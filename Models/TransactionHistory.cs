using System.ComponentModel.DataAnnotations;

namespace TestApp1.Models;

public class TransactionHistory
{
    public int transId { get; set; }
    public int bookId { get; set; }
    public int userId { get; set; }

    [Required]
    public string ?bookName { get; set; }
    public string ?transDate { get; set; }
}