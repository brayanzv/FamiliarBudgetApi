using Data.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/user/{userId:int}/wallet")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletsController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<WalletResponseDto>> CreateWallet(int userId,WalletCreationDto walletCreationDto)
        {
            var walletResponseDto = walletService.CreateWallet(userId, walletCreationDto);

            if (walletResponseDto == null)
            {
                return BadRequest("Could not Create the wallet");
            }

            return walletResponseDto;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<WalletResponseDto>>> GetAllWallets(int userId)
        {
            var walletList = walletService.GetAllWallet(userId);

            if (walletList == null)
            {
                return BadRequest("Wallets could not be obtained");
            }
            return walletList;
        }

        [HttpPut("{walletId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<WalletResponseDto>> UpdateWallet(int userId, int walletId, WalletUpdateDto walletUpdate)
        {
            var walletUpdated = walletService.UpdateWallet(userId, walletId, walletUpdate);

            if (!walletUpdated.IsSuccess)
            {
                return BadRequest(walletUpdated.ErrorMessage);
            }

            return walletUpdated.Data;
        }

        [HttpDelete("{walletId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> DeleteWallet(int userId, int walletId)
        {
            var walletDeleted = walletService.DeleteWallet(userId, walletId);

            if (!walletDeleted.IsSuccess)
            {
                return NotFound(walletDeleted.ErrorMessage);
            }

            return walletDeleted.Data;
        }

        [HttpGet("{walletId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<WalletResponseDto>> GetWallet(int userId, int walletId)
        {
            var wallet = walletService.GetWallet(userId, walletId);

            if (!wallet.IsSuccess)
            {
                return NotFound(wallet.ErrorMessage);
            }

            return wallet.Data;
        }
    }
}
