using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUsers
    {
         Task<IEnumerable<AppUser>> GetUsers();
         Task<AppUser> GetUserById(int id);
    }
}