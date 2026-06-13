namespace FinalAssetManagement.Contract.Repositories
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string userName);
    }
}
