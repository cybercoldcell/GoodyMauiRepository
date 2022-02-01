using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
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
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsers() 
        {
            var user = await _context.Users.ToListAsync();
            return user;
        }

        public async Task<AppUser> Login(LoginDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToUpper().Equals(dto.UserName.ToUpper()));
            return user;
        }

        public async void Register(AppUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UserExists(string user)
        {
            bool result = await _context.Users.AnyAsync(x => x.UserName.ToUpper().Equals(user.ToUpper()));
            return result;
        }
    }
}