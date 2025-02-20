﻿using CleanArchitecture.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interface
{
    public interface IUserRepository
    {
        Task<User> ValidateUser(string userName, string password);

        Task<List<string>> GetRoles(int userId);
    }
}
