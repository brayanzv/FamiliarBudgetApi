using Data.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/user/{userid:int}/wallet/{walletid:int}/transaction")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TransactionResponseDto>> CreateTransaction(int userId, int walletId, TransactionCreationDto transaction)
        {
            var transactionResponse = transactionService.Createtransaction(userId, walletId, transaction);

            if (!transactionResponse.IsSuccess)
            {
                return BadRequest(transactionResponse.ErrorMessage);
            }
            return transactionResponse.Data;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TransactionResponseDto>>> GetAllTransactionsByDate(int userId, int walletId, [FromQuery] TransactionByDateRequestDto transactionByDateRequestDto)
        {

            var transactionResponse = transactionService.GetAllWalletTransactionsByDate(userId, walletId, transactionByDateRequestDto);
            if (!transactionResponse.IsSuccess)
            {
                return BadRequest(transactionResponse.ErrorMessage);
            }
            return transactionResponse.Data;
        }

        [HttpPut("{transactionid:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TransactionResponseDto>> UpdateTransaction(int userId, int walletId, int transactionId, TransactionUpdateDto transactionUpdate)
        {
            var updateTransaction = transactionService.UpdateTransaction(userId, walletId, transactionId, transactionUpdate);

            if (!updateTransaction.IsSuccess)
            {
                return BadRequest(updateTransaction.ErrorMessage);
            }

            return updateTransaction.Data;
        }

        [HttpDelete("{transactionid:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> DeleteTransaction(int userId, int walletId, int transactionId)
        {
            var response = transactionService.DeleteTransaction(userId, transactionId, walletId);
            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            return response.Data;
        }
    }
}
