using Microsoft.Data.SqlClient;
using StockApp.Models;

namespace StockApp.DataAccess
{
    public static class ProductAccess
    {
        public static SqlConnection connection = new SqlConnection(StockApp.Var.connectionString);
        public static Product[] GetProducts(int page, int pageSize)
        { 
            var query = @"SELECT * 
            FROM (
                SELECT ROW_NUMBER() OVER (ORDER BY product_id DESC) AS RowNum, *
                FROM Products
            ) AS Paged
            WHERE RowNum BETWEEN @StartRow AND @EndRow;
            ";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    int startRow = (page - 1) * pageSize + 1;
                    int endRow = page * pageSize;

                    command.Parameters.AddWithValue("@StartRow", startRow);
                    command.Parameters.AddWithValue("@EndRow", endRow);

                    using (var reader = command.ExecuteReader())
                    {
                        var products = new List<Product>();
                        while (reader.Read())
                        {
                            SousCategory? sousCategory = SousCategoryAccess.GetSousCategory((int)reader["sous_category_id"]);

                            Product product = new Product
                            (
                                (int)reader["product_id"],
                                reader["name"]?.ToString() ?? string.Empty,
                                (decimal)reader["price"],
                                reader["unit"]?.ToString() ?? string.Empty,
                                (decimal)reader["quantity"],
                                sousCategory
                            );
                            products.Add(product);
                        }

                        return products.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion Product: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return Array.Empty<Product>();
        }


        public static int GetProductsLength()
        {
            var query = "SELECT COUNT(*) FROM Products";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar(); // Retourne le nombre total de produits
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du nombre de produits : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return 0; // Retourne 0 en cas d'erreur
        }

        public static void AddProduct(string name, decimal price, string unit, decimal quantity, int sousCategory)
        {
            var query = "INSERT INTO Products (name, price, unit, quantity, sous_category_id) VALUES (@Name, @Price, @Unit, @Quantity, @SousCategoryId)";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Unit", unit); 
                    command.Parameters.AddWithValue("@Quantity", quantity); 
                    command.Parameters.AddWithValue("@SousCategoryId", sousCategory);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du produit : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void UpdateProduct(int productId, string name, decimal price, string unit, decimal quantity, int sousCategory)
        {
            var query = "UPDATE Products SET name = @Name, price = @Price, unit = @Unit, sous_category_id = @SousCategoryId, quantity = @Quantity  WHERE product_id = @ProductId";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Unit", unit);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@SousCategoryId", sousCategory);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Erreur lors de la mise à jour du produit : {ex.Message}";

                // Copier le message d'erreur dans le presse-papiers
                Clipboard.SetText(errorMessage);

                // Afficher le message dans une MessageBox
                MessageBox.Show($"{errorMessage}\n\nLe message a été copié dans le presse-papiers.", "Erreur");

            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void DeleteProduct(int productId)
        {
            var query = "DELETE FROM Products WHERE product_id = @ProductId";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du produit : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void BuyProduct(int productId, int quantity, decimal price)
        {
            var query = "UPDATE Products SET quantity = @Quantity WHERE product_id = @ProductId";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'achat du produit : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void SellProduct(int productId, int quantity)
        {
            var query = "UPDATE Products SET quantity = @Quantity WHERE product_id = @ProductId";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la vente du produit : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public static Product? GetProduct(int productId)
        {
            var query = "SELECT * FROM Products WHERE product_id = @ProductId";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SousCategory? sousCategory = SousCategoryAccess.GetSousCategory((int)reader["sous_category_id"]);

                            return new Product
                            (
                                (int)reader["product_id"],
                                reader["name"]?.ToString() ?? string.Empty,
                                (decimal)reader["price"],
                                reader["unit"]?.ToString() ?? string.Empty,
                                (decimal)reader["quantity"],
                                sousCategory
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du produit : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return null;
        }
    }
}
