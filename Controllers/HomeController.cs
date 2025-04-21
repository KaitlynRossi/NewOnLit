using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnLit.Data;
using OnLit.Models;
using ASPProject.Models;           // for Customer / UsersDbContext


public class HomeController : Controller
{
  private readonly ApplicationDbContext _context;
  private readonly CommunityDbContext _communityContext;
  private readonly UsersDbContext _usersContext;

  public HomeController(
      ApplicationDbContext context,
      CommunityDbContext communityContext,
      UsersDbContext usersContext)   // ← add this
  {
    _context = context;
    _communityContext = communityContext;
    _usersContext = usersContext;
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
    // 1. grab all posts
    var posts = await _communityContext.CommunityPosts
                    .OrderBy(p => p.PostID)
                    .ToListAsync();

    // 2. grab only the users you actually need
    var ids   = posts.Select(p => p.userID).Distinct().ToList();
    var users = await _usersContext.Customers
                    .Where(u => ids.Contains(u.UserID))
                    .ToListAsync();

    // 3. project into your VM, doing a simple in‑memory lookup
    var vm = posts.Select(p => new CommunityPostViewModel {
        PostID      = p.PostID,
        PostTitle   = p.PostTitle,
        PostRating  = p.PostRating,
        PostContent = p.PostContent,
        UserName    = users
                        .FirstOrDefault(u => u.UserID == p.userID)
                        ?.UserName
                     ?? "Unknown"
    }).ToList();

    return View(vm);
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
