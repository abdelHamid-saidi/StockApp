using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.DataAccess;
using StockApp.Models;

namespace StockApp.Helpers
{
    internal class RoleHelper
    {
        public Role[] GetRoles()
        {
            return RoleAccess.GetRoles();
        }

    }
}
