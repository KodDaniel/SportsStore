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
        private List<CartLine> lineCollection = new List<CartLine>();

        // Virutell metod
        public virtual void AddItem(Product product, int quantity)
        {
            //Returns the first element of the sequence that satisfies a condition or
            //a default value if no such element is found.
            CartLine line = lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (line == null)
            {
                lineCollection.Add(
                new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                // samma som line.Quantity = line.Quantity + quantity;
                line.Quantity += quantity;
            }
        }

        // Tar bort alla objekt från en lista som uppfyller villkoret
        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);

        // Räknar ut totalvärdet av alla element i en lista
        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.Price * e.Quantity);

        //Tömmer hela listan
        public virtual void Clear() => lineCollection.Clear();

        // Egenskap som accessar vår privata lista (Expression Body-syntax)
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }


}
