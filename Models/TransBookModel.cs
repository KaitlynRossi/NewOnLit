using System.Transactions;

public class TransBook
{
    public int transBookID { get; set; }
    public int bookID { get; set; }

    // Optional: reverse navigation
    public required ICollection<Transaction> Transactions { get; set; }
}
