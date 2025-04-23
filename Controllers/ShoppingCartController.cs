using Microsoft.AspNetCore.Mvc;
using ASPProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class ShoppingCartController : Controller
{
    private readonly ApplicationDbContext _context;
    private const string CartSessionKey = "ShoppingCart";
    
    public ShoppingCartController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = GetCartFromSession();

        foreach (var item in cart)
        {
            item.Book = _context.Books.FirstOrDefault(b => b.Id == item.BookId) ?? new Book { Title = "Unknown", Author = "Unknown" };
        }

        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int bookId)
    {
        var cart = GetCartFromSession();

        var cartItem = cart.FirstOrDefault(c => c.BookId == bookId);
        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            cart.Add(new ShoppingCart
            {
                Id = cart.Count + 1,
                BookId = bookId,
                Quantity = 1
            });
        }

        SaveCartToSession(cart);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int bookId)
    {
        var cart = GetCartFromSession();

        var cartItem = cart.FirstOrDefault(c => c.BookId == bookId);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
            SaveCartToSession(cart);
        }

        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = GetCartFromSession();
        foreach (var item in cart)
        {
            if (item.Book == null)
            {
                item.Book = _context.Books.FirstOrDefault(b => b.Id == item.BookId);
            }
        }
        // Calculate totals
        var total = cart.Sum(item => item.Book.Price * item.Quantity);
        var tax = total * 0.1m; // Example: 10% tax
        var shipping = 5.99m; // Flat shipping rate
        var grandTotal = total + tax + shipping;

        // Create a view model to pass data to the view
        var model = new CheckoutViewModel
        {
            CartItems = cart,
            Total = total,
            Tax = tax,
            Shipping = shipping,
            GrandTotal = grandTotal
        };

        return View(model);
    }
    public IActionResult OrderConfirmation()
    {
        var cart = GetCartFromSession();
        if (cart != null && cart.Any())
        {
            foreach (var item in cart)
            {
                if (item.Book == null)
                {
                    item.Book = _context.Books.FirstOrDefault(b => b.Id == item.BookId);
                }
            }
            // Calculate final amount
            var total = cart.Sum(item => item.Book.Price * item.Quantity);
            var transaction = new Transaction
            {
                userID = 1, //implement user tracking
                bookID = cart.First().BookId, // For single book orders
                qty = cart.Sum(item => item.Quantity),
                saleAmount = total
            };
            // Save to database
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            // Clear the cart
            SaveCartToSession(new List<ShoppingCart>());
        }
        return View();
    }
    private List<ShoppingCart> GetCartFromSession()
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
    #pragma warning disable CS8603 
        return cartJson != null ? JsonConvert.DeserializeObject<List<ShoppingCart>>(cartJson) : new List<ShoppingCart>();
    #pragma warning restore CS8603 
    }

    private void SaveCartToSession(List<ShoppingCart> cart)
    {
        var cartJson = JsonConvert.SerializeObject(cart);
        HttpContext.Session.SetString(CartSessionKey, cartJson);
    }
    
}
