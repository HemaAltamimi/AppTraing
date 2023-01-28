using API.Data;
using API.Data.Repositories;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{ 
    [AllowAnonymous]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository ,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var usersDto = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<MemberDto>> GetUserById(int id)
        {
            return await _userRepository.GetMemberAsync(id);
        }


        [HttpGet("GetUserByUsername/{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
        {
            var result = await _userRepository.GetUsernameByIdAsync(username);
            if (result == null)
                return NotFound();

            var userDto = _mapper.Map<MemberDto>(result);
            return userDto;
        }
        
    }
}