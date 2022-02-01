using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUsers
    {
         Task<IEnumerable<AppUser>> GetUsers();
         Task<AppUser> GetUserById(int id);
         Task<AppUser> Login(LoginDto user);
         void Register(AppUser user);
         Task<bool> UserExists(string user);
         
    }
}