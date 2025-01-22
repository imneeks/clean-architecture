﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entity
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
