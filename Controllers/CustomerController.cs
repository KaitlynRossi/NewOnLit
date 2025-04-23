using Microsoft.AspNetCore.Mvc;
using ASPProject.Models;

namespace ASPProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UsersDbContext _users;
        public CustomerController(UsersDbContext users) => _users = users;

        [HttpGet]
        public IActionResult CreateAccount() => View();

        [HttpPost]
        public IActionResult CreateAccount(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _users.Customers.Add(customer);
            _users.SaveChanges();
            return RedirectToAction("Profile", new { id = customer.UserID });
        }


        public IActionResult Profile(int id)
        {
            var customer = _users.Customers.Find(id);
            if (customer == null) return NotFound();
            return View(customer);
        }
    }
}
