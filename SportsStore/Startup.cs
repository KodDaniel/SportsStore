using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
           
            services.AddTransient<IProductRepository, EfProductRepository>();
            
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
           
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List"}
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List", productPage = 1}
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new {controller = "Product", action = "List", productPage = 1}
                );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(name: null,
                    template: "{controller}/{action}/{id?}");
            });

            // Notera att detta är ett anrop till den statiska
            //seed-metoden EnsurePopulated som VI SJÄLVA SKAPAT
            SeedData.EnsurePopulated(app);

        }
    }
}

