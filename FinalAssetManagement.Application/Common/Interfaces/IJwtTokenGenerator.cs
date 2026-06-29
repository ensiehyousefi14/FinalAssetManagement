namespace FinalAssetManagement.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string userName);
    }
}
