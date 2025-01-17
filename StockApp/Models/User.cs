using StockApp.Models;
using System.Collections.Generic;

namespace StockApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Poste { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Role> UserRoles { get; set; }
        public User(int userId, string username, string firstname, string lastname, string poste, string email, string password, string phoneNumber)
        {
            UserId = userId;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Poste = poste;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            UserRoles = new List<Role>();
        }

        public void AddRoles(Role[] roles)
        {
            foreach (var role in roles)
            {
                UserRoles.Add(role);
            }
        }

        public void AddRole(int roleId, string role)
        {
            UserRoles.Add(new Role(roleId, role));
        }
    }
}
