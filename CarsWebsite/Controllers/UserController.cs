namespace API.Controllers
{
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

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly AppSettings appSettings;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            this.userService = userService;
            this.roleService = roleService;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = this.mapper.Map<User>(model);
            try
            {
                this.userService.Register(user, model.Password);
                var roleId = this.roleService.GetRoleIdByRoleName("User");
                this.roleService.AssignRoleToUser(user.UserId, roleId);
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

            var user = this.userService.Login(model.Username, model.Password);
            if (user == null)
            {
                return this.BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
            var role = this.roleService.GetRoleForUserByUserId(user.UserId);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString(new CultureInfo("en-US"))),
                    new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return this.Ok(new
            {
                Id = user.UserId,
                Token = tokenString,
                Role = role,
            });
        }
    }
}