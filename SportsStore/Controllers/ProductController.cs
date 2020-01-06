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

        // Konstruktor
        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult List() => View(_repository.Products);


    }
}