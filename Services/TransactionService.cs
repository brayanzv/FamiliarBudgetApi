using AutoMapper;
using Data.DataAccess;
using Data.DTOs;
using Data.Entity;
using Microsoft.IdentityModel.Tokens;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IWalletService walletService;
        private readonly IMapper mapper;
        private readonly ITransactionDao transactionDao;
        private readonly GeneralValidator validator;

        public TransactionService(IWalletService walletService, IMapper mapper, ITransactionDao transactionDao, GeneralValidator validator)
        {
            this.walletService = walletService;
            this.mapper = mapper;
            this.transactionDao = transactionDao;
            this.validator = validator;
        }

        public OperationResult<TransactionResponseDto> Createtransaction(int userId, int walletId, TransactionCreationDto transactionCreation)
        {
            var wallet = walletService.GetWallet(userId, walletId);
            if (wallet.Data == null)
            {
                return OperationResult<TransactionResponseDto>.Failure("This wallet don't exist");
            }

            var transaction = mapper.Map<Transaction>(transactionCreation);
            transaction.WalletId = wallet.Data.WalletId;

            var transactionDb = transactionDao.Createtransaction(transaction);

            var transactionResponse = mapper.Map<TransactionResponseDto>(transactionDb);

            return OperationResult<TransactionResponseDto>.Success(transactionResponse);
        }

        public OperationResult<string> DeleteTransaction(int userId, int transactionId, int walletId)
        {
            var wallet = walletService.GetWallet(userId, walletId);
            if (wallet.Data == null)
            {
                return OperationResult<string>.Failure("This wallet don't exist");
            }

            if (transactionDao.GetTransaction(transactionId) == null)
            {
                return OperationResult<string>.Failure("This transaction don't exist");
            }

            transactionDao.DeleteTransaction(transactionId, walletId);
            return OperationResult<String>.Success("Transaction deleted.");
        }

        public OperationResult<List<TransactionResponseDto>> GetAllWalletTransactionsByDate(int userId, int walletId, TransactionByDateRequestDto transactionByDateRequestDto)
        {
            var wallet = walletService.GetWallet(userId, walletId);
            var transactions = new List<Transaction>();

            if (wallet.Data == null)
            {
                return OperationResult<List<TransactionResponseDto>>.Failure("This wallet don't exist");
            }

            if (transactionByDateRequestDto.TransactionTypeId == null)
            {
                if (transactionByDateRequestDto.Search.IsNullOrEmpty())
                {
                    transactions = transactionDao.GetAllWalletTransactionsByDate(walletId, transactionByDateRequestDto);
                }
                else
                {
                    transactions = transactionDao.GetAllWalletTransactionsByDateSearch(walletId, transactionByDateRequestDto);
                }
            }
            else
            {
                if (transactionByDateRequestDto.Search.IsNullOrEmpty())
                {
                    transactions = transactionDao.GetAllWalletTransactionsByDateTransactionType(walletId, transactionByDateRequestDto);
                }
                else
                {
                    transactions = transactionDao.GetAllWalletTransactionsByDateTTypeAndSearch(walletId, transactionByDateRequestDto);
                }
            }

            var transactionsResponse = mapper.Map<List<TransactionResponseDto>>(transactions);
            return OperationResult<List<TransactionResponseDto>>.Success(transactionsResponse);
        }

        public OperationResult<List<TransactionResponseDto>> GetAllWalletTransactionsByDateSearch(int userId, int walletId, TransactionByDateRequestDto transactionByDateRequestDto)
        {
            return null;
        }

        public OperationResult<TransactionResponseDto> UpdateTransaction(int userId, int walletId, int transactionId, TransactionUpdateDto transactionUpdate)
        {
            if(!validator.existWallet(userId, transactionUpdate.WalletId))
            {
                return OperationResult<TransactionResponseDto>.Failure("There is no wallet with that id or it does not belong to this user");
            }

            var transactionDB = transactionDao.GetTransaction(transactionId);

            var transactionUpdated = mapper.Map(transactionUpdate, transactionDB);

            transactionDao.UpdateTransaction();

            var transactionResponse = mapper.Map<TransactionResponseDto>(transactionUpdated);

            return OperationResult<TransactionResponseDto>.Success(transactionResponse);
        }
    }
}
