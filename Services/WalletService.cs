using AutoMapper;
using Data.DataAccess;
using Data.DTOs;
using Data.Entity;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletDao walletDao;
        private readonly IMapper mapper;
        private readonly CurrentUser currentUser;

        public WalletService(IWalletDao walletDao, IMapper mapper, CurrentUser currentUser)
        {
            this.walletDao = walletDao;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }

        public WalletResponseDto CreateWallet(int id, WalletCreationDto walletCreationDto)
        {
            if (currentUser.GetCurrentUser().UserId != id)
            {
                return null;
            } 

            var wallet = mapper.Map<Wallet>(walletCreationDto);

            wallet.UserId = id; 

            var walletEntity = walletDao.CreateWallet(wallet);

            var walletResponseDto = mapper.Map<WalletResponseDto>(walletEntity);

            return walletResponseDto;
        }

        public OperationResult<string> DeleteWallet(int userId, int walletId)
        {
            if (currentUser.GetCurrentUser().UserId != userId)
            {
                return OperationResult<string>.Failure("Review your credentials");
            }

            var wallet = walletDao.GetWallet(walletId);

            if (wallet == null)
            {
                return OperationResult<string>.Failure("There is no wallet with that id");
            }

            walletDao.DeleteWallet(wallet);

            return OperationResult<string>.Success("Wallet and Transactions deleted");
        }

        public List<WalletResponseDto> GetAllWallet(int userId)
        {
            if (currentUser.GetCurrentUser().UserId != userId)
            {
                return null;
            }

            var walletListEntity = walletDao.GetAllWallets(userId);

            var walletList = mapper.Map<List<WalletResponseDto>>(walletListEntity);

            return walletList;
        }

        public OperationResult<WalletResponseDto> GetWallet(int userId, int walletId)
        {
            if (currentUser.GetCurrentUser().UserId != userId)
            {
                return OperationResult<WalletResponseDto>.Failure("Review your credentials");
            }

            var wallet = walletDao.GetWallet(walletId);

            if (wallet == null)
            {
                return OperationResult<WalletResponseDto>.Failure("There is no wallet with that id");
            }

            if (wallet.UserId != userId)
            {
                return OperationResult<WalletResponseDto>.Failure("You can't see this wallet");
            }

            var walletResponse = mapper.Map<WalletResponseDto>(wallet);

            return OperationResult<WalletResponseDto>.Success(walletResponse);
        }

        public OperationResult<WalletResponseDto> UpdateWallet(int userId, int walletId, WalletUpdateDto walletUpdateDto)
        {
            if (userId != currentUser.GetCurrentUser().UserId)
            {
                return OperationResult<WalletResponseDto>.Failure("Review your credentials");
            }

            var wallet  = walletDao.GetWallet(walletId);

            if (wallet == null)
            {
                return OperationResult<WalletResponseDto>.Failure("There is no wallet with that id");
            }

            if (wallet.UserId != userId)
            {
                return OperationResult<WalletResponseDto>.Failure("You can't change this wallet");
            }

            wallet = mapper.Map(walletUpdateDto, wallet);

            walletDao.UpdateWallet();

            var walletUpdate = mapper.Map<WalletResponseDto>(wallet);

            return OperationResult<WalletResponseDto>.Success(walletUpdate);
        }
    }
}
