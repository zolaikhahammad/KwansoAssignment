using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kwanso.Core.Models;
using Kwanso.Core.Contracts.Request.Users;
using Kwanso.Core.Contracts.Response.Users;
using Kwanso.Core;
using AutoMapper;
using Kwanso.Core.Contracts.ViewModels;
using Kwanso.Core.Contracts.Response;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace KwansoApiDotNet5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KwansoContext _context;
        private readonly IMapper _mapper;

        public UsersController(KwansoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequest userloginrequest)
        {
            UserLoginResponse usersReponse = new UserLoginResponse();           
            usersReponse.jwt = new JwtToken();
            try
            {
                var checkEmail = _context.Users.Any(x => x.Email == userloginrequest.email);
                if (checkEmail == false)
                {
                    usersReponse.status_code = 404;
                    usersReponse.error = "User not found";
                    return NotFound(new Response<UserLoginResponse>(usersReponse));
                }
                string hashedPassword = PasswordHasher.HashPassword(userloginrequest.password);
                Users _user = await _context.Users.Where(h => h.Email == userloginrequest.email && h.Password == hashedPassword).SingleOrDefaultAsync();
                if (_user == null)
                {
                    usersReponse.status_code = 400;
                    usersReponse.error = "email or password is incorrect";
                    return BadRequest(new Response<UserLoginResponse>(usersReponse));
                }


                usersReponse.status_code = 200;
                usersReponse.jwt = await TokenServices.GenerateTokenAsync(_user.Email, _user.Id.ToString());
                var user= _mapper.Map<UsersViewModel>(_user);                    
                return Ok(new Response<UserLoginResponse>(usersReponse));

            }
            catch (Exception ex)
            {
                usersReponse.error = ex.Message;
                usersReponse.status_code = 400;
                return BadRequest(new Response<UserLoginResponse>(usersReponse));
            }

        }


       
        [HttpGet("/user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser()
        {
            UsersReponse usersReponse = new UsersReponse();
            usersReponse.user = new Kwanso.Core.Contracts.ViewModels.UsersViewModel();

            try
            {

                var userEmail = User.FindFirstValue(ClaimTypes.Email); 
                usersReponse.status_code = 200;
                usersReponse.user.Email = userEmail;
                usersReponse.user.Id= Convert.ToInt32(User.FindFirst("Id")?.Value);
                return Ok(new Response<UsersReponse>(usersReponse));

            }
            catch (Exception ex)
            {
                usersReponse.error = ex.Message;
                usersReponse.status_code = 400;
                return BadRequest(new Response<UsersReponse>(usersReponse));
            }

        }





        [AllowAnonymous]
        [HttpPost("/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersReponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUsers(CreateUserRequest request)
        {
            UsersReponse usersReponse = new UsersReponse();
            usersReponse.user = new Kwanso.Core.Contracts.ViewModels.UsersViewModel();
            try
            {
                var checkEmail = _context.Users.Any(x => x.Email == request.Email);
                if (checkEmail == true)
                {
                    usersReponse.status_code = 409;
                    usersReponse.error = "Email already exist";                    
                    return Conflict(new Response<UsersReponse>(usersReponse));
                }
                Users user = new Users();
                user.Email = request.Email;
                user.Password = PasswordHasher.HashPassword(request.Password);
                user.CreatedAt = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                usersReponse.status_code = 200;      
                usersReponse.user = _mapper.Map<UsersViewModel>(user);
                return Ok(new Response<UsersReponse>(usersReponse));

            }
            catch(Exception ex)
            {
                usersReponse.error = ex.Message;
                usersReponse.status_code = 400;
               return BadRequest(new Response<UsersReponse>(usersReponse));
            }
            
        }

       
    }
}
