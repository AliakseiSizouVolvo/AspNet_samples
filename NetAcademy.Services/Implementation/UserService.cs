using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetAcademy.DataBase;
using NetAcademy.Services.Abstractions;
using Microsoft.Extensions.Logging;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Services.Implementation;

public class UserService : IUserService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserService> _logger;
    public UserService(BookStoreDbContext dbContext, 
        ILogger<UserService> logger, 
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _logger = logger;
        _configuration = configuration;
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