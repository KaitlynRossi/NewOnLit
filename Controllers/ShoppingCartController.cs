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
