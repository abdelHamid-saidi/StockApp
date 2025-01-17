using Microsoft.Data.SqlClient;
using StockApp.Models;

namespace StockApp.DataAccess
{

    public static class CategoryAccess
    {
        public static SqlConnection connection = new SqlConnection(StockApp.Var.connectionString);

        public static Category? GetCategory(int categoryId)
        {
            var query = "SELECT * FROM Categories WHERE category_id = @CategoryId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category? category = new Category
                            (
                                (int)reader["category_id"],
                                reader["name"]?.ToString() ?? string.Empty
                            );

                            return category;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while getting the category.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return null;
        }


        public static Category[] GetCategories()
        {
            var query = @"
            SELECT c.category_id, c.name as category_name, sc.sous_category_id, sc.name as sous_category_name
            FROM Categories c
            LEFT JOIN SousCategories sc ON c.category_id = sc.category_id";

            var categories = new List<Category>();

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var categoryId = (int)reader["category_id"];
                            var category = categories.FirstOrDefault(c => c.CategoryId == categoryId);

                            if (category == null)
                            {
                                category = new Category(
                                    categoryId,
                                    reader["category_name"]?.ToString() ?? string.Empty
                                );
                                categories.Add(category);
                            }

                            if (reader["sous_category_id"] != DBNull.Value)
                            {
                                var sousCategory = new SousCategory(
                                    (int)reader["sous_category_id"],
                                    reader["sous_category_name"]?.ToString() ?? string.Empty,
                                    category
                                );
                                category.SousCategories.Add(sousCategory);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while getting the categories with sous categories.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return categories.ToArray();
        }
        
        public static void AddCategory(string name)
        {
            var query = "INSERT INTO Categories (name) VALUES (@Name)";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while adding the category.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void AddSousCategory(int categoryId, string name)
        {
            var query = "INSERT INTO SousCategories (name, category_id) VALUES (@Name, @CategoryId)";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while adding the sous category.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void DeleteCategory(int categoryId)
        {
            var query = "DELETE FROM Categories WHERE category_id = @CategoryId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while deleting the category.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public static void DeleteSousCategory(int sousCategoryId)
        {
            var query = "DELETE FROM SousCategories WHERE sous_category_id = @SousCategoryId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SousCategoryId", sousCategoryId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while deleting the sous category.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
