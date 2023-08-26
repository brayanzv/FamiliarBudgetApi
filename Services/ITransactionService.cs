using Data.DTOs;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITransactionService
    {
        public OperationResult<TransactionResponseDto> Createtransaction(int userId, int walletId, TransactionCreationDto transactionCreation);
        public OperationResult<List<TransactionResponseDto>> GetAllWalletTransactionsByDate(int userId, int walletId, TransactionByDateRequestDto transactionByDateRequestDto);
        public OperationResult<List<TransactionResponseDto>> GetAllWalletTransactionsByDateSearch(int userId, int walletId, TransactionByDateRequestDto transactionByDateRequestDto);
        public OperationResult<string> DeleteTransaction(int userId, int transactionId, int walletId);
        public OperationResult<TransactionResponseDto> UpdateTransaction(int userId, int walletId, int transactionId, TransactionUpdateDto transactionUpdate);
    }
}
