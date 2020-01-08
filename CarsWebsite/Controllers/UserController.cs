using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly AppSettings _appSettings;
        private IUserService _userService;
        private IRoleService _roleService;
        private IMapper _mapper;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            this._userService = userService;
            this._roleService = roleService;
            this._mapper = mapper;
            this._appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = this._mapper.Map<User>(model);
            try
            {
                this._userService.Register(user, model.Password);
                var roleId = this._roleService.GetRoleIdByRoleName("User");
                this._roleService.AssignRoleToUser(user.UserId, roleId);
                return this.Ok();
            }
            catch (SqlException)
            {
                throw new Exception();
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
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = this._userService.Login(model.Username, model.Password);
            if (user == null)
            {
                return this.BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
            var role = this._roleService.GetRoleForUserByUserId(user.UserId);
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
            return this.Ok(new
            {
                Id = user.UserId,
                Token = tokenString,
                Role = role
            });
        }
    }
}