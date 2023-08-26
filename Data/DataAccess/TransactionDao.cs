using Data.DTOs;
using Data.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class TransactionDao : ITransactionDao
    {
        private readonly ApplicationDbContext context;

        public TransactionDao(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Transaction Createtransaction(Transaction transaction)
        {
            try
            {
                var descriptionParam = new SqlParameter("@Description", transaction.Description);
                var amountParam = new SqlParameter("@Amount", transaction.Amount);
                var dateParam = new SqlParameter("@Date", transaction.Date);
                var typeIdParam = new SqlParameter("@TransactionTypeId", transaction.TransactionTypeId);
                var walletIdParam = new SqlParameter("@WalletId", transaction.WalletId);
                var newTransactionIdParam = new SqlParameter
                {
                    ParameterName = "@NewTransactionId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output // Marcar como parámetro de salida
                };

                var query = @"EXEC CreateTransaction
                      @Description, @Amount, @Date, @TransactionTypeId, @WalletId, @NewTransactionId OUTPUT";

                var parameters = new[]
                {
                    descriptionParam,
                    amountParam,
                    dateParam,
                    typeIdParam,
                    walletIdParam,
                    newTransactionIdParam
                };

                var rowsAffected = context.Database.ExecuteSqlRaw(query, parameters);
                var newTransactionId = (int)newTransactionIdParam.Value;

                context.SaveChanges();

                transaction.TransactionId = newTransactionId;

                return transaction;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while saving the transaction.", ex);
            }
        }

        public void DeleteTransaction(int transactionId, int walletId)
        {
            var query = "EXEC DeleteTransaction @TransactionId, @WalletId";
            context.Database.ExecuteSqlRaw(query, 
                new SqlParameter("@TransactionId", transactionId),
                new SqlParameter("@WalletId", walletId));
            context.SaveChanges();
        }

        public Transaction GetTransaction(int transactionId)
        {
            return context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }

        public List<Transaction> GetAllWalletTransactionsByDate(int walletId, TransactionByDateRequestDto transactionByDateRequestDto)
        {
            try
            {
                var query = "EXEC GetTransactionsByDate {0}, {1}, {2}, {3}, {4}";

                var transactions = context.Transactions.FromSqlRaw(query, walletId, 
                    transactionByDateRequestDto.SDate, 
                    transactionByDateRequestDto.EDate, 
                    transactionByDateRequestDto.Page, 
                    transactionByDateRequestDto.Count
                    ).ToList();

                return transactions;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting the transactions.", ex);
            }
        }

        public List<Transaction> GetAllWalletTransactionsByDateSearch(int walletId, TransactionByDateRequestDto transactionByDateRequestDto)
        {
            try
            {
                var query = "EXEC GetTransactionsByDateAndSearch {0}, {1}, {2}, {3}, {4}, {5}";

                var transactions = context.Transactions.FromSqlRaw(query, walletId,
                    transactionByDateRequestDto.SDate,
                    transactionByDateRequestDto.EDate,
                    transactionByDateRequestDto.Page,
                    transactionByDateRequestDto.Count,
                    transactionByDateRequestDto.Search
                    ).ToList();

                return transactions;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting the transactions.", ex);
            }
        }

        public void UpdateTransaction()
        {
            try
            {
                context.SaveChanges();
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Transaction> GetAllWalletTransactionsByDateTransactionType(int walletId, TransactionByDateRequestDto transactionByDateRequestDto)
        {
            try
            {
                var query = "EXEC GetTransactionsByDateAndTransactionType {0}, {1}, {2}, {3}, {4}, {5}";

                var transactions = context.Transactions.FromSqlRaw(
                    query, 
                    walletId,
                    transactionByDateRequestDto.TransactionTypeId,
                    transactionByDateRequestDto.SDate,
                    transactionByDateRequestDto.EDate,
                    transactionByDateRequestDto.Page,
                    transactionByDateRequestDto.Count
                    ).ToList();

                return transactions;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting the transactions.", ex);
            }
        }

        public List<Transaction> GetAllWalletTransactionsByDateTTypeAndSearch(int walletId, TransactionByDateRequestDto transactionByDateRequestDto)
        {
            try
            {
                var query = "EXEC GetTransactionsByTransactionTypeAndSearchText {0}, {1}, {2}, {3}, {4}, {5}, {6}";

                var transactions = context.Transactions.FromSqlRaw(
                    query, 
                    walletId,
                    transactionByDateRequestDto.TransactionTypeId,
                    transactionByDateRequestDto.SDate,
                    transactionByDateRequestDto.EDate,
                    transactionByDateRequestDto.Page,
                    transactionByDateRequestDto.Count,
                    transactionByDateRequestDto.Search
                    ).ToList();

                return transactions;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting the transactions.", ex);
            }
        }
    }
}
