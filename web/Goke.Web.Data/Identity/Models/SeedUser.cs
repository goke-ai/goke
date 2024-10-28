using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.AspNetCore.Identity.Models
{
    public class SeedUser : ApplicationUser
    {
        public string[]? RoleList { get; set; }
        public string? Password { get; set; }
    }
}
