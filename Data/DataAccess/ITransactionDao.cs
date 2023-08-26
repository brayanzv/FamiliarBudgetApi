using Data.DTOs;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public interface ITransactionDao
    {
        public Transaction Createtransaction(Transaction transaction);
        public List<Transaction> GetAllWalletTransactionsByDate(int walletId, TransactionByDateRequestDto transactionByDateRequestDto);
        public List<Transaction> GetAllWalletTransactionsByDateSearch(int walletId, TransactionByDateRequestDto transactionByDateRequestDto);
        public List<Transaction> GetAllWalletTransactionsByDateTransactionType(int walletId, TransactionByDateRequestDto transactionByDateRequestDto);
        public List<Transaction> GetAllWalletTransactionsByDateTTypeAndSearch(int walletId, TransactionByDateRequestDto transactionByDateRequestDto);

        public void DeleteTransaction(int transactionId, int walletId);
        public Transaction GetTransaction(int transactionId);
        public void UpdateTransaction();
    }
}
