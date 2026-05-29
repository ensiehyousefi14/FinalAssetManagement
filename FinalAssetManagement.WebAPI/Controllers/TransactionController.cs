using FinalAssetManagement.Application.DTOs.Transaction;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssetManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(new ApiResponse<IEnumerable<TransactionDto>>(transactions, "Transactions Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("{transactionId:int}")]
        public async Task<IActionResult> GetTransaction(int transactionId)
        {
            var transaction = await _transactionService.GetTransactionAsync(transactionId);
            if (transaction is null)
                return NotFound(new ApiResponse<TransactionDto>(null, $"Transaction With Id {transactionId} Not Found.", false));

            return Ok(new ApiResponse<TransactionDto>(transaction, "Transaction Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("details/{TransactionId:int}")]
        public async Task<IActionResult> GetTransactionDetails(int transactionId)
        {
            var transactionDetails = await _transactionService.GetTransactionDetailsAsync(transactionId);
            if (transactionDetails is null)
                return NotFound(new ApiResponse<TransactionDetailsDto>(null, $"TransactionDetails With Id {transactionId} Not Found.", false));

            return Ok(new ApiResponse<TransactionDetailsDto>(transactionDetails, "TransactionDetails Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("asset/{assetId:int}")]
        public async Task<IActionResult> GetTransactionsByAsset(int assetId)
        {
            var transactions = await _transactionService.GetTransactionsByAssetAsync(assetId);
            return Ok(new ApiResponse<IEnumerable<TransactionDto>>(transactions, "Transactions Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost("asset/{assetId:int}")]
        public async Task<IActionResult> CreateTransaction(int assetId, CreateTransactionDto dto)
        {
            try
            {
                var createdTransaction = await _transactionService.CreateTransactionAsync(assetId, dto);
                return CreatedAtAction(nameof(GetTransaction),
                                       new { transactionId= createdTransaction.Id},
                                       new ApiResponse<TransactionDto>(createdTransaction, $"Transaction for assetId {assetId} Created Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TransactionDto>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPut("asset/{assetId:int}/transaction/{transactionId:int}")]
        public async Task<IActionResult> UpdateTransaction(int assetId, int transactionId, UpdateTransactionDto dto)
        {
            try
            {
                await _transactionService.FullUpdateTransactionAsync(assetId, transactionId, dto);
                return Ok(new ApiResponse<string>(null, "Transaction Fully Updated Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPatch("asset/{assetId:int}/transaction/{transactionId:int}")]
        public async Task<IActionResult> PartialUpdateTransaction(int assetId, int transactionId, PatchTransactionDto dto)
        {
            try
            {
                await _transactionService.PartialUpdateTransactionAsync(assetId, transactionId, dto);
                return Ok(new ApiResponse<string>(null, "Transaction Partially Updated successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpDelete("asset/{assetId:int}/transaction/{transactionId:int}")]
        public async Task<IActionResult> RemoveTransaction(int assetId, int transactionId)
        {
            try
            {
                await _transactionService.RemoveTransactionAsync(assetId, transactionId);
                return Ok(new ApiResponse<string>(null, "Transaction Deleted successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<string>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------
    }
}
