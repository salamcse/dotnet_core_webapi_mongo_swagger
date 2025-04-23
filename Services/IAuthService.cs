namespace CoreDotNetToken.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(string username, string password);
        string ValidateUserAndGenerateToken(string username, string password);
    }
}
