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
        public IActionResult Login()
        {
            // Clear any existing session when accessing login page
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
                return View(loginModel);

            var customer = _context.User.FirstOrDefault(c => 
                c.UserName == loginModel.UserName && 
                c.Password == loginModel.Password);

            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginModel);
            }

            // Store user information in session
            HttpContext.Session.SetInt32("UserId", customer.UserID);
            HttpContext.Session.SetString("UserName", customer.UserName);

            return RedirectToAction("Profile", new { id = customer.UserID });
        }
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            // Redirect to login page
            return RedirectToAction("Login");
        }

         public IActionResult OrderHistory()
        {
            var transaction = _context.Transactions.ToList();
            return View(transaction);
        }

    }
}