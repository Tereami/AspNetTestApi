using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DomainModel.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrators = "Administrators";
        public const string Users = "Users";
    }
}
