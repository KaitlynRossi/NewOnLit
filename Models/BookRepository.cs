using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ASPProject.Models
{
    public class BookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
    }
}
