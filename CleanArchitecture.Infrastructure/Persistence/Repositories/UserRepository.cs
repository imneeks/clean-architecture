using CleanArchitecture.Core.Entity;
using CleanArchitecture.Core.Interface;
using CleanArchitecture.Core.Interface.Core.Interfaces;
using CleanArchitecture.Infrastructure.Persistence.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class UserRepository :  IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly DbSet<UserRole> _userRoles;
        public UserRepository(AppDbContext context)
        {
            _context = context; 
            _dbSet = _context.Set<User>();
            _userRoles = _context.Set<UserRole>();

        }

        public async Task<List<string>> GetRoles(int userId)
        {
           var userRoles = await _userRoles.Where(ur => ur.UserId == userId)
            .Include(ur => ur.Role)
            .ToListAsync();

            return userRoles.Select(ur => ur.Role.RoleName).ToList();
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(u => EF.Functions.Like(u.UserName, userName) && u.Password == password);
        }
    }
}
