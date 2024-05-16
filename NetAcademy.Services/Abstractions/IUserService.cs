namespace NetAcademy.Services.Abstractions;

public interface IUserService
{
    public Task<bool> CheckIsUserWithEmailExists(string email);

    public Task<int> RegisterUser(string email, string password, Guid roleId);
    public Task<bool> CheckPassword(string email, string password);

    //rest of jwt staff there 
    string GenerateJWT(Guid userId);
    public Task RevokeTokenAsync(Guid tokenId);
    public Task<(string, Guid)> RefreshToken(Guid tokenId);
}