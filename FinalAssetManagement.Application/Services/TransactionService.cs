using AutoMapper;
using FinalAssetManagement.Application.DTOs.Transaction;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Core.Enums;

namespace FinalAssetManagement.Application.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //------------------------------------------------------------------
        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            var transactions = await _unitOfWork.Transactions.GetTransactionsWithAssetAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        //------------------------------------------------------------------

        public async Task<TransactionDto?> GetTransactionAsync(int transactionId)
        {
            var transaction = await _unitOfWork.Transactions.GetTransactionWithAssetAsync(transactionId);
            return _mapper.Map<TransactionDto?>(transaction);
        }

        //------------------------------------------------------------------

        public async Task<TransactionDetailsDto?> GetTransactionDetailsAsync(int transactionId)
        {
            var transaction = await _unitOfWork.Transactions.GetTransactionWithAssetAsync(transactionId);
            return _mapper.Map<TransactionDetailsDto?>(transaction);
        }

        //------------------------------------------------------------------
        public async Task<IEnumerable<TransactionDto>> GetTransactionsByAssetAsync(int assetId)
        {
            var transactions = await _unitOfWork.Transactions.GetTransactionsByAssetIdAsync(assetId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        //------------------------------------------------------------------

        public async Task<TransactionDto> CreateTransactionAsync(int assetId, CreateTransactionDto dto)
        {
            var asset = await _unitOfWork.Assets.GetByIdAsync(assetId);
            if (asset is null)
                throw new InvalidOperationException("Asset Not Found.");

            var createdTransaction = asset.AddTransaction(dto.Description, dto.Amount, dto.TransactionType);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<TransactionDto>(createdTransaction);
        }

        //------------------------------------------------------------------

        public async Task FullUpdateTransactionAsync(int assetId, int transactionId, UpdateTransactionDto dto)
        {
            var transaction = await _unitOfWork.Transactions.GetTransactionWithAssetAsync(transactionId);

            if (transaction is null || transaction.AssetId != assetId)
                throw new InvalidOperationException("Transaction or Asset Not Found.");

            var oldAsset = transaction.Asset;

            // Remove the previous effect of the transaction from the Asset
            if (transaction.Type == TransactionType.Increase)
                oldAsset.ChangePrice(oldAsset.Price - transaction.Amount);
            else
                oldAsset.ChangePrice(oldAsset.Price + transaction.Amount);


            // Update the transaction details
            transaction.ChangeDescription(dto.Description);
            transaction.ChangeAmount(dto.Amount);
            transaction.ChangeType(dto.TransactionType);

            var targetAsset = oldAsset;
            // If the asset has changed
            if (dto.AssetId != transaction.AssetId)
            {
                var newAsset = await _unitOfWork.Assets.GetByIdAsync(dto.AssetId);
                if (newAsset is null)
                    throw new InvalidOperationException("New Asset Not Found.");

                transaction.ChangeAsset(newAsset);
                targetAsset = newAsset;
            }


            // Apply the new effect to the asset
            if (dto.TransactionType == TransactionType.Increase)
                targetAsset.ChangePrice(targetAsset.Price + transaction.Amount);
            else
                targetAsset.ChangePrice(targetAsset.Price - transaction.Amount);


            await _unitOfWork.SaveAsync();
        }

        //------------------------------------------------------------------

        public async Task PartialUpdateTransactionAsync(int assetId, int transactionId, PatchTransactionDto dto)
        {
            var transaction = await _unitOfWork.Transactions.GetTransactionWithAssetAsync(transactionId);

            if (transaction is null || transaction.AssetId != assetId)
                throw new InvalidOperationException("Transaction or Asset Not Found.");

            var oldAsset = transaction.Asset;

            // Remove old effect
            if (transaction.Type == TransactionType.Increase)
                oldAsset.ChangePrice(oldAsset.Price - transaction.Amount);
            else
                oldAsset.ChangePrice(oldAsset.Price + transaction.Amount);


            // Update description
            if (dto.Description is not null)
                transaction.ChangeDescription(dto.Description);

            // Update amount
            if (dto.Amount is not null)
                transaction.ChangeAmount(dto.Amount.Value);

            // Update type
            if (dto.TransactionType is not null)
                transaction.ChangeType(dto.TransactionType.Value);


            var targetAsset = oldAsset;

            // Update asset if changed
            if (dto.AssetId.HasValue && dto.AssetId.Value != transaction.AssetId)
            {
                var newAsset = await _unitOfWork.Assets.GetByIdAsync(dto.AssetId.Value);

                if (newAsset is null)
                    throw new InvalidOperationException("New Asset Not Found.");

                transaction.ChangeAsset(newAsset);
                targetAsset = newAsset;
            }


            // Apply new effect
            if (transaction.Type == TransactionType.Increase)
                targetAsset.ChangePrice(targetAsset.Price + transaction.Amount);
            else
                targetAsset.ChangePrice(targetAsset.Price - transaction.Amount);


            await _unitOfWork.SaveAsync();

        }

        //------------------------------------------------------------------

        public async Task RemoveTransactionAsync(int assetId, int transactionId)
        {

            var transaction = await _unitOfWork.Transactions.GetTransactionWithAssetAsync(transactionId);

            if (transaction is null || transaction.AssetId != assetId)
                throw new InvalidOperationException("Transaction or Asset Not Found.");

            var asset = transaction.Asset;

            // Remove transaction effect from asset
            if (transaction.Type == TransactionType.Increase)
                asset.ChangePrice(asset.Price - transaction.Amount);
            else
                asset.ChangePrice(asset.Price + transaction.Amount);

            // Delete transaction
            _unitOfWork.Transactions.Delete(transaction);

            await _unitOfWork.SaveAsync();
        }
    }
}
