using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnLit.Data;
using OnLit.Models;

public class HomeController : Controller
{
  private readonly ApplicationDbContext _context;
  private readonly CommunityDbContext _communityContext;

  public HomeController(ApplicationDbContext context, CommunityDbContext communityContext)
  {
    _context = context;
    _communityContext = communityContext;
  }

  public IActionResult Index()
  {
    return View();
  }

  public async Task<IActionResult> MyView(string search)
  {
    var books = from b in _context.Books
                select b;

    if (!string.IsNullOrEmpty(search))
    {
      books = books.Where(b => b.Title.Contains(search) || b.Author.Contains(search) || b.Genre.Contains(search));
    }

    return View(await books.ToListAsync());
  }

  public async Task<IActionResult> Community()
  {
    var posts = await _communityContext.CommunityPosts
                    .OrderBy(p => p.PostID)
                    .ToListAsync();
    return View(posts);
  }

  public async Task<IActionResult> Details(int id)
  {
    var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

    if (book == null)
    {
      return NotFound($"Book with ID {id} not found.");
    }

    return View(book);
  }

  public IActionResult About()
  {
    return View();
  }
}
