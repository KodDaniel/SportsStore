using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        // Egenskap som endast har en get, och
        //därför endast kan sättas i denna klass konstruktor
        public IConfiguration Configuration { get; }

        // Vilket vi gör här (sätter egenskapen i en konstruktor)
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddScoped<IOrderRepository, EfOrderRepository>();
            //Notera att vi förenklar raden nedan för förståelse, i boken använder författaren Lambdauttryck
            //  Exakt samma slutresultat dock(obviously)
            services.AddScoped(SessionCart.GetCart);
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddServerSideBlazor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}",
                    new {Controller = "Product", action = "List"});

                endpoints.MapControllerRoute("page", "Page{productPage:int}",
                    new {Controller = "Product", action = "List", productPage = 1});

                endpoints.MapControllerRoute("category", "{category}",
                    new {Controller = "Product", action = "List", productPage = 1});

                endpoints.MapControllerRoute("pagination",
                    "Products/Page{productPage}",
                    new {Controller = "Product", action = "List", productPage = 1});

                endpoints.MapControllerRoute(name: "default", 
                    pattern: "{controller=Product}/{action=List}");

                endpoints.MapRazorPages();
                //endpoints.MapBlazorHub();
                //endpoints.MapFallbackToPage("/admin/{*catchall}", "/Management/Index");
            });
           
       
            // Notera att detta är ett anrop till den statiska
            //seed-metoden EnsurePopulated som VI SJÄLVA SKAPAT
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app).Wait();

        }
    }
}