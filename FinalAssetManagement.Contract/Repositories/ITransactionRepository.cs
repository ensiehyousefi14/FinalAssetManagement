using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Contract.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsByAssetIdAsync(int assetId);
    }
}
