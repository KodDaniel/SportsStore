        using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    { 
        private IProductRepository _repository;

        // Konstruktor
        public CartController(IProductRepository repo) => _repository = repo;

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product!= null)
            {
                var cart = GetCart();
                
                // Lägger till denna produkt, 1 till antalet (argument två nedan)
                cart.AddItem(product,1);
                
                SaveCart(cart);

            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product!= null)
            {
                var cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);

            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

            return cart;
        }

        private void SaveCart(Cart cart)
        {
            // Sparar vår kundvagn i en session med nyckeln "Cart"
            // Notera att vi gör detta genom vår extension metod "SetJson"
            HttpContext.Session.SetJson("Cart", cart);
        }




    }
}
