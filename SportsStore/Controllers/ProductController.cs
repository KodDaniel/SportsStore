using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        // Privat fält
        // Notera att detta fält är beroende av AddTransiet i startup.cs
        private IProductRepository _repository;

        //Publikt fält
        //Bestämmer hur många produkter vi vill ha på varje sida
        public int PageSize = 4;

        // Konstruktor
        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult List(string category, int productPage = 1)
        {
             var pl = new ProductsListViewModel
            {
                // Category == null betyder att användaren ej efterfrågat en SPECIFIK kategori
                // (p=> category == null) ger därför produkter från samtliga kategorier
                // p.Category == category innebär att en specifik kategori blivit efterfrågat, och därför visas endast produkter inom denna kategori
                Products = _repository.Products.Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    // Om ingen kategori är vald an användaren är TotalItems samma sak som alla produkter
                    // Annars är TotalItems alla produkter inom den valda kategorin
                    TotalItems = (category == null) ? _repository.Products.Count() : _repository.Products.Count(p => p.Category == category)

                    //TotalItems = _repository.Products.Count()
                },

                // CurrentCategory är egenskap i ProductsListViewModel
                CurrentCategory = category

            };

            return View(pl);
        }


    }
}