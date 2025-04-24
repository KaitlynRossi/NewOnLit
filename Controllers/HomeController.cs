using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnLit.Models;


public class HomeController : Controller
{
  private readonly ApplicationDbContext _context;

  public HomeController(
      ApplicationDbContext context
      )   // ← add this
  {
    _context = context;
  }

  public IActionResult Index()
  {
    return View();
  }

   public IActionResult Privacy()
  {
    return View();
  }

   public IActionResult FAQ()
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
    var posts = await _context.Community
    .OrderBy(p => p.PostID)
    .ToListAsync();

    var ids = posts.Select(p => p.userID).Distinct().ToList();

    var users = await _context.User
        .Where(u => ids.Contains(u.UserID))
        .ToListAsync();

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

 [HttpPost]
  public async Task<IActionResult> CreateCommunityPost([FromBody] CommunityPost post)
  {
    if (ModelState.IsValid)
    {
      _context.Community.Add(post);
      await _context.SaveChangesAsync();
      return Json(new { success = true });
    }
    return Json(new { success = false });
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
