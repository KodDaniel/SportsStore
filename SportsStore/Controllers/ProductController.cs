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
        public int PageSize = 4;

        // Konstruktor
        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        // Det som skickas till viewn är ett objekt av typen ProductsListViewModel
        public ViewResult List(int productPage = 1) =>
            View(
            new ProductsListViewModel
            {
                Products = _repository.Products.OrderBy(p => p.ProductId).
                    Skip((productPage - 1) * PageSize)
                    .Take(PageSize), 
               
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }
            }
    );


        //// Vi använder String Interpolation
        //public ContentResult DisplayUrlValue(int id) => 
        //    Content($"The id in the Url is: {id}");



    }
}