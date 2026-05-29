using FinalAssetManagement.Application.DTOs.Transaction;

namespace FinalAssetManagement.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDto?> GetTransactionAsync(int transactionId);
        Task<TransactionDetailsDto?> GetTransactionDetailsAsync(int transactionId);
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
        Task<IEnumerable<TransactionDto>> GetTransactionsByAssetAsync(int assetId);


        Task<TransactionDto> CreateTransactionAsync(int assetId, CreateTransactionDto dto);
        Task FullUpdateTransactionAsync(int assetId, int transactionId, UpdateTransactionDto dto);
        Task PartialUpdateTransactionAsync(int assetId, int transactionId, PatchTransactionDto dto);
        Task RemoveTransactionAsync(int assetId, int transactionId);

    }
}
