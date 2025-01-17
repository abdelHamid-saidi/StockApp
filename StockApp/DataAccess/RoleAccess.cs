using System.Text;
using Microsoft.Data.SqlClient;
using StockApp.Models;

namespace StockApp.DataAccess
{
    public static class RoleAccess
    {
        public static SqlConnection connection = new SqlConnection(StockApp.Var.connectionString);
        public static Role[] GetUserRoles(int UserId)
        {
            var query = "SELECT r.role_id, r.name FROM UsersRoles ur JOIN Roles r ON ur.role_id = r.role_id WHERE ur.user_id = @UserId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);

                    using (var reader = command.ExecuteReader())
                    {
                        var roles = new List<Role>();
                        while (reader.Read())
                        {
                            Role role = new Role
                            (
                                (int)reader["role_id"],
                                reader["name"]?.ToString() ?? string.Empty
                            );
                            roles.Add(role);
                        }

                        return roles.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion ROle: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return Array.Empty<Role>();
        }

        public static Role[] GetRoles()
        {
            var query = "SELECT role_id, name FROM Roles";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var roles = new List<Role>();
                        while (reader.Read())
                        {
                            Role role = new Role
                            (
                                (int)reader["role_id"],
                                reader["name"]?.ToString() ?? string.Empty
                            );
                            roles.Add(role);
                        }

                        return roles.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des rôles: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return Array.Empty<Role>();
        }

        public static void SetRole(int UserId, int RoleId)
        {
            var query = "INSERT INTO UsersRoles (user_id, role_id, assigned_date) VALUES (@UserId, @RoleId, @AssignedDate); SELECT SCOPE_IDENTITY();";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@RoleId", RoleId);
                    command.Parameters.AddWithValue("@AssignedDate", DateTime.Now);

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'attribution du rôle: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }


        }

        public static void ResetRoles(int UserId)
        {
            var query = "DELETE FROM UsersRoles WHERE user_id = @UserId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la réinitialisation des rôles: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
