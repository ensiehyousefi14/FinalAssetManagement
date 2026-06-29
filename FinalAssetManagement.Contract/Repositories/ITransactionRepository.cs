using FinalAssetManagement.Core.Entities;

namespace FinalAssetManagement.Contract.Repositories
{
    // Provides queries for retrieving Transaction entities with, or filtered by their related navigation properties.
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsWithAssetAsync();
        Task<Transaction?> GetTransactionWithAssetAsync(int transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByAssetIdAsync(int assetId);
    }
}
