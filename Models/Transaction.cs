using System;
using System.ComponentModel.DataAnnotations;

namespace ASPProject.Models
{
    public class Transaction
    {
        [Key]
        public int transID { get; set; }
        public int userID { get; set; }
        public int transBookID { get; set; }
        public int qty { get; set; }
        public decimal saleAmount { get; set; }

        public required Book TransBook { get; set; }
    }
}