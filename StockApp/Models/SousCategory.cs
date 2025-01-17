using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class SousCategory
    {
        public int SousCategoryId { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }

        //public ICollection<Product> Products { get; set; }
        public SousCategory(int sousCategoryId, string name, Category category)
        {
            SousCategoryId = sousCategoryId;
            Name = name;
            Category = category;
        }

        public SousCategory(int sousCategoryId, string name)
        {
            SousCategoryId = sousCategoryId;
            Name = name;
        }
    }

}
