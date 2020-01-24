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

        private Cart cart;

        // Konstruktor
        public CartController(IProductRepository repo,Cart cartService)
        {
            _repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product!= null)
            {
                cart.AddItem(product,1);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product!=null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }





    }
}
