using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {

        // Repetera Construtor Chaining med base...
        //...samt implementation av generiska klasser (i detta fall...
        //..typbestämmer vi den generiska klassen DbContextOptions)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Vi typbestämmer/implementerar den generiska klassen DbSet 
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
