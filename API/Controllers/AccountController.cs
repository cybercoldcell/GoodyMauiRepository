using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUsers _userRepo;
        private readonly IToken _token;
        private readonly IMapper _mapper;

        public AccountController(IUsers usersRepo, IToken token, IMapper mapper, ILogger<AccountController> logger)
        {
     
            _userRepo = usersRepo;
            _token = token;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _userRepo.Login(dto);
            if(user == null) return Unauthorized("Invalid user");

            using var oHmac = new HMACSHA512(user.PasswordSalt);
            var oPassHash = oHmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            for(int i =0; i < oPassHash.Length; i++)
            {
                if(oPassHash[i] != user.PasswordHash[i]) 
                    return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _token.CreateToken(user)
            };

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            if(await UserExists(dto.UserName)) return BadRequest("User name is already exists.");

            using var oHmac = new HMACSHA512();
            var oUser = new AppUser
            {
                UserName = dto.UserName,
                PasswordHash = oHmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = oHmac.Key
            };

            _userRepo.Register(oUser);

            return new UserDto
            {
                UserName = oUser.UserName,
                Token = _token.CreateToken(oUser)
            };

        }

        private async Task<bool> UserExists(string user)
        {
            return await _userRepo.UserExists(user);
        }
        
    }
}