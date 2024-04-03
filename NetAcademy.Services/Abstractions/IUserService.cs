namespace NetAcademy.Services.Abstractions;

public interface IUserService
{
    public Task<bool> CheckIsUserWithEmailExists(string email);

    public Task<int> RegisterUser(string email, string password, Guid roleId);
    public Task<bool> CheckPassword(string email, string password);
}