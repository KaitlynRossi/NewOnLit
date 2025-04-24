using Microsoft.AspNetCore.Mvc;
using ASPProject.Models;

namespace ASPProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateAccount() => View();

        [HttpPost]
        public IActionResult CreateAccount(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _context.User.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = customer.UserID });
        }


        public IActionResult Profile(int id)
        {
            var customer = _context.User.Find(id);
            if (customer == null) return NotFound();
            return View(customer);
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var customer = _context.User.FirstOrDefault(c => c.Email == email && c.Password == password);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            return RedirectToAction("Profile", new { id = customer.UserID });
        }

    }
}