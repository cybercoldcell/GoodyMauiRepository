using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UsersRepository : IUsers
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<AppUser> GetUserById(int id)
        {
            var model = await _context.Users.FindAsync(id);
            return model;
        }

        public async Task<IEnumerable<AppUser>> GetUsers() 
        {
            var model = await _context.Users.ToListAsync();
            return model;
        }

        
    }
}