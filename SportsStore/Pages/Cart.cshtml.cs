using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private readonly IProductRepository repository;

        //Kom ihåg att vi genom dependency injection i startup.cs...
        //ser till att en instans av SessionCart instansieras när vi sätter en instans av Cart
        public CartModel(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl) => ReturnUrl = returnUrl ?? "/";

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            Cart.AddItem(product, 1);
            return RedirectToPage(new {returnUrl });
        }
    }
}
