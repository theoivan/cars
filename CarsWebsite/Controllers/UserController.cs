using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            if (appSettings == null)
                throw new ArgumentNullException(nameof(appSettings));

            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var user = _mapper.Map<User>(model);

            try
            {
                _userService.Register(user, model.Password);
                var roleId = _roleService.GetRoleIdByRoleName("User");
                _roleService.AssignRoleToUser(user.UserId, roleId);
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]LoginDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            var user = _userService.Login(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var role = _roleService.GetRoleForUserByUserId(user.UserId);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString(new CultureInfo("en-US"))),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Token = tokenString,
                Role = role
            });
        }
    }
}