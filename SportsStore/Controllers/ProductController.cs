using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        // Privat fält
        private IProductRepository _repository;

        //Publikt fält
        public int PageSize = 4;

        // Konstruktor
        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        // Sortera efter stigande ordning på ProductID
        public ViewResult List(int productPage = 1) =>
            View(_repository.Products.OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize));

    }
}