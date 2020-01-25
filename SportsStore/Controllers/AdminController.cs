﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repo) => _repository = repo;

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productId) =>
            View(_repository.Products.FirstOrDefault(p => p.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                //Lägger till den redigerade produkten i databasen
                _repository.SaveProduct(product);

                // Lagrar ett meddelande om en lyckad uppdatering för användaren
                // Genom TempData och String Interpolation
                TempData["message"] = $"{product.Name}  has been saved";

                // Återgår till Index
                return RedirectToAction(nameof(Index));
            }
            else
            {  // Validering har smällt 
                return View(product);
            }

        }
    }
}