using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Repositories;
using API.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsers _usersRepo;
        public UsersController(IUsers usersRepo,ILogger<UsersController> logger)
        {
            _logger = logger;
            _usersRepo  = usersRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _usersRepo.GetUsers();
            return users.ToList();
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            var users = await _usersRepo.GetUserById(id);
            return users;
        }

    }
}