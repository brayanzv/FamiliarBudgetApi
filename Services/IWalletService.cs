using Data.DTOs;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWalletService
    {
        public WalletResponseDto CreateWallet(int id, WalletCreationDto walletCreationDto);
        public List<WalletResponseDto> GetAllWallet(int userId);
        public OperationResult<WalletResponseDto> GetWallet(int userId, int walletId);
        public OperationResult<WalletResponseDto> UpdateWallet(int userId, int walletId, WalletUpdateDto walletUpdateDto);
        public OperationResult<string> DeleteWallet(int userId, int walletId);
    }
}
