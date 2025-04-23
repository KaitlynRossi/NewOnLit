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
    }
}
