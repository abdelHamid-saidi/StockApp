using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public Role(int roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }
        
    }

}
