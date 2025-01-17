using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.DataAccess;
using StockApp.Models;

namespace StockApp.Helpers
{
    internal class CategoryHelper
    {
        public Category[] GetCategories()
        {
            return CategoryAccess.GetCategories();
        }

        public void AddCategory(string name)
        {
            CategoryAccess.AddCategory(name);
        }

        public void AddSousCategory(int categoryId, string name)
        {
            CategoryAccess.AddSousCategory(categoryId, name);
        }


        public void DeleteCategory(int categoryId)
        {
            CategoryAccess.DeleteCategory(categoryId);
        }

        public void DeleteSousCategory(int sousCategoryId)
        {
            CategoryAccess.DeleteSousCategory(sousCategoryId);
        }
    }
}
