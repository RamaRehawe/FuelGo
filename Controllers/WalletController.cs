using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : BaseController
    {
        public WalletController(UserInfoService userInfoService, IUnitOfWork unitOfWork) : base(userInfoService, unitOfWork)
        {
        }

        [HttpGet("get-my-wallet")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200)]
        public IActionResult GetMyWallet()
        {
            var user = base.GetActiveUser()!;
            var wallet = _unitOfWork._walletRepository.GetWalletByUserId(user.Id);
            if(wallet == null)
            {
                Wallet userWallet = new Wallet
                {
                    UserId = user.Id,
                    Amount = 0
                };
                _unitOfWork._walletRepository.AddWallet(userWallet);
                return Ok(userWallet.Amount);
            }
            return Ok(wallet.Amount);

        }

    }
}
