using FinalAssetManagement.Application.DTOs.Transaction;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.WebAPI.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalAssetManagement.WebAPI.Controllers
{
    [Authorize]
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
        // RouteSample = api/transactions 

        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(new ApiResponse<IEnumerable<TransactionDto>>(transactions, "Transactions Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("{transactionId:int}")]
        // RouteSample = api/transactions/5

        public async Task<IActionResult> GetTransaction([FromRoute] int transactionId)
        {
            var transaction = await _transactionService.GetTransactionAsync(transactionId);
            if (transaction is null)
                return NotFound(new ApiResponse<TransactionDto>(null, $"Transaction With Id {transactionId} Not Found.", false));

            return Ok(new ApiResponse<TransactionDto>(transaction, "Transaction Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("details/{TransactionId:int}")]
        // RouteSample = api/transactions/details/5

        public async Task<IActionResult> GetTransactionDetails([FromRoute] int transactionId)
        {
            var transactionDetails = await _transactionService.GetTransactionDetailsAsync(transactionId);
            if (transactionDetails is null)
                return NotFound(new ApiResponse<TransactionDetailsDto>(null, $"TransactionDetails With Id {transactionId} Not Found.", false));

            return Ok(new ApiResponse<TransactionDetailsDto>(transactionDetails, "TransactionDetails Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpGet("asset/{assetId:int}")]
        // RouteSample = api/transactions/asset/4

        public async Task<IActionResult> GetTransactionsByAsset([FromRoute] int assetId)
        {
            var transactions = await _transactionService.GetTransactionsByAssetAsync(assetId);
            return Ok(new ApiResponse<IEnumerable<TransactionDto>>(transactions, "Transactions Retrieved Successfully.", true));
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost("asset/{assetId:int}")]
        // RouteSample = api/transactions/asset/3

        public async Task<IActionResult> CreateTransaction([FromRoute] int assetId,
                                                           [FromBody] CreateTransactionDto dto)
        {
            try
            {
                var createdTransaction = await _transactionService.CreateTransactionAsync(assetId, dto);
                return CreatedAtAction(nameof(GetTransaction),
                                       new { transactionId = createdTransaction.Id },
                                       new ApiResponse<TransactionDto>(createdTransaction, $"Transaction for assetId {assetId} Created Successfully.", true));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse<TransactionDto>(null, ex.Message, false));
            }
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPut("asset/{assetId:int}/transaction/{transactionId:int}")]
        // RouteSample = api/transactions/asset/6/transaction/8

        public async Task<IActionResult> UpdateTransaction([FromRoute] int assetId,
                                                           [FromRoute] int transactionId,
                                                           [FromBody] UpdateTransactionDto dto)
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
        // RouteSample = api/transactions/asset/3/transaction/7

        public async Task<IActionResult> PartialUpdateTransaction([FromRoute] int assetId,
                                                                  [FromRoute] int transactionId,
                                                                  [FromBody] PatchTransactionDto dto)
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
        // RouteSample = api/transactions/asset/9/transaction/11

        public async Task<IActionResult> RemoveTransaction([FromRoute] int assetId,
                                                           [FromRoute] int transactionId)
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
