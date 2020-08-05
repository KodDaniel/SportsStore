//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace SportsStore.Models
//{
//    // Klassen AppIdentityDbContext ärver från klassen IdentityDbContext
//    // Vi typbestämmer den generiska klassen IdentityDbContext,
//    // så att klassen tar typen IdentityUser
//    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
//    {
//        // Constructor Chaining med Base 
//        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
//            : base(options) { }

//    }
//}
