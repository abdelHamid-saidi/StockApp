using StockApp.Models;
using Microsoft.Data.SqlClient;

namespace StockApp.DataAccess
{
    internal class SousCategoryAccess
    {
        public static SqlConnection connection = new SqlConnection(StockApp.Var.connectionString);
        public static SousCategory? GetSousCategory(int sousCategoryId)
        {
            var query = "SELECT * FROM SousCategories WHERE sous_category_id = @SousCategoryId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SousCategoryId", sousCategoryId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category category = CategoryAccess.GetCategory((int)reader["category_id"])!;

                            SousCategory? sousCategory = new SousCategory
                            (
                                (int)reader["sous_category_id"],
                                reader["name"]?.ToString() ?? string.Empty,
                                category
                            );

                            return sousCategory;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("An error occurred while getting the sous category.", ex);
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
