using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.DataAccess;
using StockApp.Models;

namespace StockApp.Helpers
{
    internal class OperationHelper
    {
        public int GetTotalOperations()
        {
            int operationCount = OperationAccess.GetOperationsLength();
            return operationCount;
        }

        public Operation[] GetOperations(int currentPage, int pageSize)
        {
            Operation[] op = OperationAccess.GetOperations(currentPage, pageSize);
            foreach (var item in op)
            {
                item.Product = ProductAccess.GetProduct(item.ProduitId);
            }

            return op;
        }

        public int GetTotalPages(int pageSize)
        {
            int totalOperations = OperationAccess.GetOperationsLength();
            return (int)Math.Ceiling((double)totalOperations / pageSize);
        }
    }
}
