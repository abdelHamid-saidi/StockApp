using StockApp.Models; 
using StockApp.DataAccess;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics;
using System.Security.Policy;

namespace StockApp.Helpers
{
    internal class ProductHelper
    {
        public int GetTotalProducts()
        {
            int productCount = ProductAccess.GetProductsLength();
            return productCount;
        }

        public Product[] GetProducts(int currentPage ,int pageSize)
        {
            return ProductAccess.GetProducts(currentPage, pageSize);
        }

        public int GetTotalPages(int pageSize)
        {
            int totalProducts = ProductAccess.GetProductsLength();
            return (int)Math.Ceiling((double)totalProducts / pageSize);
        }

        public void AddProduct(string name, float price, string unit, float quantity, int sousCategory)
        {
            ProductAccess.AddProduct(name, (decimal)price, unit, (decimal)quantity, sousCategory);
        }

        public void UpdateProduct(int productId, string name, float price, string unit, float quantity, int sousCategory)
        {
            ProductAccess.UpdateProduct(productId, name, (decimal)price, unit, (decimal)quantity, sousCategory);
        }

        public void DeleteProduct(int productId)
        {
            ProductAccess.DeleteProduct(productId);
        }

        public void BuyProduct(int productId, int quantity, float price)
        {
            ProductAccess.BuyProduct( productId, quantity, (decimal)price);
        }

        public void SellProduct(int product_id, int quantity, float operationQuantity, float operationPrice)
        {
            ProductAccess.SellProduct(product_id, quantity);

            //OperationAccess.AddOperation("sell", (decimal)operationQuantity, (decimal)operationPrice, product_id, null);
        }
    }
}
