using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class Operation
    {
        public int OperationId { get; set; } // Correspond à operation_id
        public string OperationType { get; set; } = string.Empty; // Correspond à operationType
        public DateTime OperationDate { get; set; } // Correspond à operationDate
        public decimal OperationQuantity { get; set; } // Correspond à operationQuantity
        public decimal OperationPrice { get; set; } // Correspond à operationQuantity
        public int ProduitId { get; set; } // Clé étrangère vers Produit
        public int? SupplierId { get; set; } // Clé étrangère vers Supplier (nullable)

        // Propriétés de navigation
        public Product? Product { get; set; } // Référence au produit associé
        public Supplier? Supplier { get; set; } // Référence au fournisseur associé

        public Operation(int operationId, string operationType, DateTime operationDate, decimal operationQuantity, decimal operationPrice, int produitId, int? supplierId)
        {
            OperationId = operationId;
            OperationType = operationType;
            OperationDate = operationDate;
            OperationQuantity = operationQuantity;
            OperationPrice = operationPrice;
            ProduitId = produitId;
            SupplierId = supplierId;
        }
    }

}
