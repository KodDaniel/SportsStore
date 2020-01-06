using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EfProductRepository : IProductRepository
    {
        // Privat fält
        private ApplicationDbContext context;

        // Konstruktor
        public EfProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        // Egenskap 
        public IQueryable<Product> Products => context.Products;
    }
}
