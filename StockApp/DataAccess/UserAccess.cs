using System.Text;
using Microsoft.Data.SqlClient;
using StockApp.Models;

namespace StockApp.DataAccess
{
    public static class UserAccess
    {
        public static SqlConnection connection = new SqlConnection(StockApp.Var.connectionString);

        public static User[] GetUsers(int page, int pageSize)
        {
            var query = @"SELECT * 
            FROM (
                SELECT ROW_NUMBER() OVER (ORDER BY username ASC) AS RowNum, *
                FROM Users
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
                        var users = new List<User>();
                        while (reader.Read())
                        {
                            User user = new User
                            (
                                (int)reader["user_id"],
                                reader["username"]?.ToString() ?? string.Empty,
                                reader["firstname"]?.ToString() ?? string.Empty,
                                reader["lastname"]?.ToString() ?? string.Empty,
                                reader["poste"]?.ToString() ?? string.Empty,
                                reader["email"]?.ToString() ?? string.Empty,
                                reader["password"]?.ToString() ?? string.Empty,
                                reader["phone_number"]?.ToString() ?? string.Empty
                            );
                            user.AddRoles(RoleAccess.GetUserRoles(user.UserId));
                            users.Add(user);
                        }
                        return users.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion User: {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return Array.Empty<User>();
        }
        public static User? GetUser(string username)
        {
            var query = "SELECT * FROM [Users] WHERE username = @Username";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User
                            (
                                (int)reader["user_id"],
                                reader["username"]?.ToString() ?? string.Empty,
                                reader["firstname"]?.ToString() ?? string.Empty,
                                reader["lastname"]?.ToString() ?? string.Empty,
                                reader["poste"]?.ToString() ?? string.Empty,
                                reader["email"]?.ToString() ?? string.Empty,
                                reader["password"]?.ToString() ?? string.Empty,
                                reader["phone_number"]?.ToString() ?? string.Empty
                            );
                            user.AddRoles(RoleAccess.GetUserRoles(user.UserId));
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return null;
        }
        public static int GetUsersLength()
        {
            var query = "SELECT COUNT(*) FROM [Users]";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return 0;
        }
        public static void DeleteUser(int userId)
        {
            var query = "DELETE FROM Users WHERE user_id = @UserId";
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public static void AddUser(string username, string firstname, string lastname, string poste, string email, string phoneNumber)
        {
            var query = @"INSERT INTO Users (username, firstname, lastname, poste, email, password, phone_number) 
                          VALUES (@Username, @Firstname, @Lastname, @Poste, @Email, @Password, @PhoneNumber);";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Firstname", firstname);
                    command.Parameters.AddWithValue("@Lastname", lastname);
                    command.Parameters.AddWithValue("@Poste", poste);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", "4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2");
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de l'utilisateur : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public static void UpdateUser(int user_id, string username, string firstname, string lastname, string poste, string email, string phoneNumber)
        {
            var query = @"UPDATE Users 
                          SET  username = @Username, firstname = @Firstname, lastname = @Lastname, poste = @Poste, email = @Email, phone_number = @PhoneNumber 
                          WHERE user_id = @UserId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user_id);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Firstname", firstname);
                    command.Parameters.AddWithValue("@Lastname", lastname);
                    command.Parameters.AddWithValue("@Poste", poste);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour de l'utilisateur : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public static void UpdateUserInfos(int id, string username, string firstname, string lastname, string email, string phoneNumber)
        {
            var query = @"UPDATE Users 
                          SET username = @Username, firstname = @Firstname, lastname = @Lastname, email = @Email, phone_number = @PhoneNumber 
                          WHERE user_id = @UserId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Firstname", firstname);
                    command.Parameters.AddWithValue("@Lastname", lastname);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour des informations de l'utilisateur : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public static void UpdateUserPassword(int id, string password)
        {
            var query = @"UPDATE Users 
                          SET password = @Password 
                          WHERE user_id = @UserId";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du mot de passe de l'utilisateur : {ex.Message}", "Erreur");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
 