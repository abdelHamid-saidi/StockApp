using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }

        //public int SousCategoryId { get; set; }
        public SousCategory SousCategory { get; set; }

        //public ICollection<DepositProduct> DepositProducts { get; set; }

        public Product(int productId, string name, decimal price, string unit, decimal quantity,  SousCategory sousCategory )
        { 
            ProductId = productId;
            Name = name;
            Price = price;
            Unit = unit;
            Quantity = quantity;
            //SousCategoryId = sousCategoryId;
            SousCategory = sousCategory;
            //DepositProducts = depositProducts;
        }
    }

}
