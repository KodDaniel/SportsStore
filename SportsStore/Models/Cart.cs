using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        // Virutell metod
        public virtual void AddItem(Product product, int quantity)
        {
            //Returns the first element of the sequence that satisfies a condition or
            //a default value if no such element is found.
            CartLine line = Lines.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            // Om det inte existerar en orderlinje med denna produkt, lägg till linje
            if (line == null)
            {
                Lines.Add(
                new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                // Annars = Öka antalet på den existerande orderlinje som finns för denna produkt
                line.Quantity += quantity;
            }
        }

        // Tar bort alla objekt från en lista som uppfyller villkoret
        public virtual void RemoveLine(Product product) =>
            Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

        // Räknar ut totalvärdet av alla element i en lista
        public virtual decimal ComputeTotalValue() =>
            Lines.Sum(e => e.Product.Price * e.Quantity);

        //Tömmer hela listan
        public virtual void Clear() => Lines.Clear();

        // Egenskap som accessar vår privata lista (Expression Body-syntax)
    }


}
