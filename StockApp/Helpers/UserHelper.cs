using Microsoft.VisualBasic.ApplicationServices;
using StockApp.DataAccess;
using StockApp.Models;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using User = StockApp.Models.User;

namespace StockApp.Helpers
{
    internal class UserHelper
    {
        private static User? Auth = null;
        public User? GetAuth()
        {
            return Auth;
        }
        public User? Login(string username, string password)
        {
            User? user = UserAccess.GetUser(username);
            if (user != null && user.Password == password)
            {
                Auth = user;
                return user;
            }
            return null;
        }

        public void Logout()
        {
            Auth = null;
        }

        public void UpdateUserInfos(int user_id, string username, string firstname, string lastname, string email, string phoneNumber)
        {
            UserAccess.UpdateUserInfos(user_id, username, firstname, lastname, email, phoneNumber);
            Auth =  UserAccess.GetUser(username);
        }

        public void UpdateUserPassword(int id, string password)
        {
            UserAccess.UpdateUserPassword(id, password);
            Auth = UserAccess.GetUser(Auth.Username);
        }
        public User[] GetUsers(int currentPage, int pageSize)
        {
            return UserAccess.GetUsers(currentPage, pageSize);
        }

        public int GetTotalPages(int pageSize)
        {
            int totalUsers = UserAccess.GetUsersLength();
            return (int)Math.Ceiling((double)totalUsers / pageSize);
        }

        public void DeleteUser(int userId)
        {
            UserAccess.DeleteUser(userId);
        }

        public void AddUser(string username, string firstname, string lastname, string poste, string email, string phoneNumber, string[] Roles)
        {
            UserAccess.AddUser(username, firstname, lastname, poste, email, phoneNumber);
            User? user = UserAccess.GetUser(username);

            if (user != null)
            {
                foreach (string role in Roles)
                {
                    int roleId;
                    if (int.TryParse(role, out roleId))
                    {
                        RoleAccess.SetRole(user.UserId, roleId);
                    }
                }
            }
        }

        public void UpdateUser(int user_id, string username, string firstname, string lastname, string poste, string email, string phoneNumber, string[] Roles)
        {
            UserAccess.UpdateUser(user_id, username, firstname, lastname, poste, email, phoneNumber);
            User? user = UserAccess.GetUser(username);

            if (user != null)
            {
                RoleAccess.ResetRoles(user.UserId);
                foreach (string role in Roles)
                {
                    int roleId;
                    if (int.TryParse(role, out roleId))
                    {
                        RoleAccess.SetRole(user.UserId, roleId);
                    }
                }
            }
        }

    }
}
