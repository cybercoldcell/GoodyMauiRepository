using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.DTOs;


namespace API.Controllers
{
    
    public class UsersController : BaseApiController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsers _usersRepo;
        private readonly IMapper _mapper;
        public UsersController(IUsers usersRepo, IMapper mapper,ILogger<UsersController> logger)
        {
            _logger = logger;
            _usersRepo  = usersRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _usersRepo.GetUsers();
            var result = _mapper.Map<IEnumerable<UserDto>>(users);
            return result.ToList();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var users = await _usersRepo.GetUserById(id);
            var result = _mapper.Map<UserDto>(users);
            return result;
        }

    }
}