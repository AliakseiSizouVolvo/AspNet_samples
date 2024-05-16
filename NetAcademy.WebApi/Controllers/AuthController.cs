using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetAcademy.DataBase.Entities;
using NetAcademy.Services.Abstractions;
using NetAcademy.WebApi.RequestModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace NetAcademy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserService _userService;
        //private IRoleService _roleService;

        public AuthController(IConfiguration configuration)//,
            //IUserService userService,
            //IRoleService roleService)
        {
            _configuration = configuration;
            //_userService = userService;
            //_roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Auth")]

        public async Task<IActionResult> Auth([FromBody] LoginRequestModel model)
        {
            if (model != null &&
                model.Email.Equals("test@email.com")
                && model.Password.Equals("12345678"))
            {
                var user = new User();// _userService.GetUser();
                var jwt = _userService.GenerateJWT(user.Id);
                return Ok(jwt);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]

        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel model)
        {
           //check is token exists in DB & not expired
           var result = await _userService.RefreshToken(model.TokenId);
            return Ok( new
            {
                Jwt =result.Item1,
                RefreshToken = result.Item2,
            });
        }

        [Authorize]
        [HttpPost]
        [Route("Revoke")]

        public async Task<IActionResult> Revoke([FromBody] RevokeTokenModel model)
        {
            //check is token exists in DB & token and user are same
            await _userService.RevokeTokenAsync(model.TokenId);
            return NoContent();
        }
    }
}
