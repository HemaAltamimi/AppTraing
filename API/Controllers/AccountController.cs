using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Controllers
{ 
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private  ITokenService _tokenService;
        public AccountController(DataContext context ,ITokenService tokenService )
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
           if(await UserExists(registerDto.UserName))
            return BadRequest("Username Is Found");
           
           using var hamc= new HMACSHA512();
           var user =new AppUser{
            UserName=registerDto.UserName,
            PasswordHash= hamc.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt=hamc.Key
           };
           _context.Users.Add(user);
           await _context.SaveChangesAsync();
           
           return new UserDto{
                Username=user.UserName,
                Token=_tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto){
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == loginDto.Username);

            if(user == null) return BadRequest("Invalid  username");

            using var hamc= new HMACSHA512(user.PasswordSalt);
            var  computehash= hamc.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i=0; i<computehash.Length;i++)
                if(computehash[i] !=user.PasswordHash[i])return BadRequest("Invalid  Password"); 


            return new UserDto{
                Username=user.UserName,
                Token=_tokenService.CreateToken(user)
            };
        }

  
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x=> x.UserName.ToLower() == username.ToLower());
        }

    }
}