using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class MothanaController : BaseApiController
    {
       private readonly DataContext _context;
        
        public MothanaController(DataContext context)
        {
            _context = context;
        }  
 
        [HttpGet("auth")]
        [Authorize]
        public ActionResult<string> Secert(){
            return "secert msg";
        }

        [HttpGet("not-found")]
        [AllowAnonymous]
        public ActionResult<AppUser> GetNotFound(){
           var result =_context.Users.Find(-1);
           if(result == null) return NotFound();

           return result;
        }

        [HttpGet("server-error")]
        [AllowAnonymous]
        public ActionResult<string> GetServerError(){
                var result =_context.Users.Find(-1);
                return result.ToString();
        }

        [HttpGet("bad-request")]
        [AllowAnonymous]
        public ActionResult<string> GetBadRequest(){
           return BadRequest("bad request Messages");
        }
    }
}