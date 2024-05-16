using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetAcademy.DataBase;
using NetAcademy.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetAcademy.Data.CQS.Commands.Tokens;
using NetAcademy.Data.CQS.Queries.Tokens;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Services.Implementation;

public class UserService : IUserService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserService> _logger;
    public UserService(BookStoreDbContext dbContext, 
        ILogger<UserService> logger, 
        IConfiguration configuration, IMediator mediator)
    {
        _dbContext = dbContext;
        _logger = logger;
        _configuration = configuration;
        _mediator = mediator;
    }

   
    public async Task<bool> CheckIsUserWithEmailExists(string email)
    {
        return (await _dbContext.Users.AnyAsync(user => user.Email.Equals(email)));
    }

    public async Task<int> RegisterUser(string email, string password, Guid roleId)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = email,
            RoleId = roleId,
            PasswordHash = GetSha256Hash($"{password}{_configuration["AppSetting:PasswordSalt"]}")
        };
        await _dbContext.Users.AddAsync(user);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckPassword(string email, string password)
    {
        var dbPasswordHash = (await _dbContext.Users.SingleOrDefaultAsync(user => user.Email.Equals(email)))?
            .PasswordHash;
        
        var providedPasswordHash = GetSha256Hash(password);

        return dbPasswordHash != null && dbPasswordHash.Equals(providedPasswordHash);
    }

    public async Task RevokeTokenAsync(Guid tokenId)
    {
        await _mediator.Send(new RemoveTokenCommand()
        {
            TokenId = tokenId
        });
        
    }

    public string GenerateJWT(Guid userId)
    {
        var email = "test@email.com";//take user from db
        var issuer = _configuration["Jwt:Issuer"];
        var aud = _configuration["Jwt:Audience"];
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var lifetime = int.Parse(_configuration["Jwt:LifeTime"]);

        var signInCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var subject = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString("D")),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, "Admin")
        });

        var exp = DateTime.UtcNow.AddMinutes(lifetime);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = exp,
            Issuer = issuer,
            Audience = aud,
            SigningCredentials = signInCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
    
    public async Task<(string, Guid)> RefreshToken(Guid tokenId)
    {
        var user = await _mediator.Send(new GetUserByRefreshTokenQuery() { TokenId = tokenId });//create handler
        if (user != null)
        {
            var jwt = GenerateJWT(user.Id);
            await _mediator.Send(new RemoveTokenCommand() { TokenId = tokenId });
            var refreshToken = await _mediator.Send(new CreateRefreshTokenCommand() 
                { UserId = user.Id });

            return (jwt, refreshToken);
        }

        throw new AggregateException();//todo
    }

    private string GetSha256Hash(string password)
    {
        var sb = new StringBuilder();
        using (var hash = SHA256.Create())
        {
           var result = hash
               .ComputeHash(Encoding.UTF8.GetBytes(password))
               .Select(b => b.ToString("x2"));
           sb.AppendJoin("", result);
        }

        return sb.ToString();
    }
    
}