using Microsoft.Data.SqlClient;
using StockApp.Models;


namespace StockApp.DataAccess
{
    internal class OperationAccess
    {
        public static SqlConnection connection = new SqlConnection(StockApp.Var.connectionString);

        public static Operation[] GetOperations(int page, int pageSize)
        {
            var query = @"SELECT * 
                FROM (
                    SELECT ROW_NUMBER() OVER (ORDER BY operation_id DESC) AS RowNum, *
                    FROM Operations
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
                        var operations = new List<Operation>();
                        while (reader.Read())
                        {
                            var operation = new Operation(
                                reader.GetInt32(reader.GetOrdinal("operation_id")),
                                reader.GetString(reader.GetOrdinal("operationType")),
                                reader.GetDateTime(reader.GetOrdinal("operationDate")),
                                reader.GetDecimal(reader.GetOrdinal("operationQuantity")),
                                reader.GetDecimal(reader.GetOrdinal("operationPrice")),
                                reader.GetInt32(reader.GetOrdinal("product_id")),
                                reader.IsDBNull(reader.GetOrdinal("supplier_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("supplier_id"))
                            );
                            operations.Add(operation);
                        }

                        return operations.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion Operation: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return Array.Empty<Operation>();
        }

        public static int GetOperationsLength()
        {
            var query = "SELECT COUNT(*) FROM Operations";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar(); // Retourne le nombre total d'opérations
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du nombre d'opérations : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return 0; // Retourne 0 en cas d'erreur
        }
        public static bool AddOperation(string operationType, decimal operationQuantity, decimal operationPrice, int product_id, int? supplier_id)
        {
            var query = @"INSERT INTO Operations (operationType, operationQuantity, operationPrice, product_id, supplier_id)
                          VALUES (@OperationType, @OperationQuantity, @OperationPrice, @ProductId, @SupplierId)";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OperationType", operationType);
                    command.Parameters.AddWithValue("@OperationQuantity", operationQuantity);
                    command.Parameters.AddWithValue("@OperationPrice", operationPrice);
                    command.Parameters.AddWithValue("@ProductId", product_id);
                    command.Parameters.AddWithValue("@SupplierId",supplier_id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de l'opération : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return false;
        }
        

    }
}
