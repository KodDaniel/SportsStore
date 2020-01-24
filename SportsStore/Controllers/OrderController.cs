using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
            private IOrderRepository _repository;
            private Cart cart;

            public OrderController(IOrderRepository repoService, Cart cartService)
            {
                _repository = repoService;
                cart = cartService;
            }

            public ViewResult CheckOut() => View(/*new Order()*/);

            [HttpPost]
            public IActionResult Checkout(Order order)
            {
                // Kontrollerar att vi har minst en line i kundvagnen
                // Om inte skickas felmeddelande
                if (!cart.Lines.Any())
                {
                   ModelState.AddModelError("","Sorry, your cart is empty");
                }

                if (ModelState.IsValid)
                {
                    // För över lines från kundvagnen till ordern
                    // (Dvs från ett Cart-object till ett Order-objekt) 
                    order.Lines = cart.Lines.ToList();
                    // Lägger in ordern i databasen
                    _repository.SaveOrder(order);
                // Rensar Kundvagnen och returnerar ett meddelande...
                // som tackar för orden

                //return RedirectToAction(nameof(Completed));

                return RedirectToAction("Completed");
                }
                else
                {  // Hit kommer vi om valideringen smäller
                    return View(order);
                }

            }
            public ViewResult Completed()
            {
                cart.Clear();
                return View();
            }
    }
}