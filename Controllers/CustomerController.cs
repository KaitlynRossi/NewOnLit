using Microsoft.AspNetCore.Mvc;
using ASPProject.Models;

namespace ASPProject.Controllers
{
    public class CustomerController : Controller
    {
        private static List<Customer> customers = new();

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customers.Add(customer); // Simulate saving the customer
                return RedirectToAction("Profile", new { id = customer.id });
            }

            return View(customer);
        }

        public IActionResult Profile(int id)
        {
            var customer = customers.FirstOrDefault(c => c.id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}
