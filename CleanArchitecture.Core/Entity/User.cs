using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public string ? Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
