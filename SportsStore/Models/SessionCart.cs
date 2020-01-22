using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Models
{
    public class SessionCart : Cart 
    {
       // Statisk metod (INTE en konstruktor!) 
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        
        [JsonIgnore] 
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            // Vi använder base-nyckelordet för att använda AddItem i basklassen Cart
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            // Vi använder base-nyckelordet för att använda RemoveItem i basklassen Cart
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            // Vi använder base-nyckelordet för att använda Clear basklassen Cart
            base.Clear();
            Session.Remove("Cart");
        }
    }
}