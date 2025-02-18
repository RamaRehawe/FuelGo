using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

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
            var customerId = _unitOfWork._orderRepository.GetCustomerId(base.GetActiveUser()!.Id);
            var wallet = _unitOfWork._walletRepository.GetWalletByUserId(customerId);
            if(wallet == null)
            {
                Wallet userWallet = new Wallet
                {
                    UserId = customerId,
                    Amount = 0
                };
                _unitOfWork._walletRepository.AddWallet(userWallet);
                return Ok(userWallet.Amount);
            }
            return Ok(wallet.Amount);

        }

        [HttpPost("request-otp")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200)]
        public IActionResult RequestOtp()
        {
            var customerId = _unitOfWork._orderRepository.GetCustomerId(base.GetActiveUser()!.Id);
            var wallet = _unitOfWork._walletRepository.GetWalletByCustomerId(customerId);
            if (wallet == null)
            {
                Wallet userWallet = new Wallet
                {
                    UserId = customerId,
                    Amount = 0
                };
                _unitOfWork._walletRepository.AddWallet(userWallet);
            }
            var otp = GenerateRandomCode(8);
            wallet.OTP = otp;
            _unitOfWork.Commit();
            return Ok(wallet.OTP);
        }

        [HttpPost("charge-wallet")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public IActionResult ChargeWallet(ReqChargeWalletDto request)
        {
            var user = _unitOfWork._userRepository.GetUserByPhone(request.Phone);
            if(user == null)
            {
                return BadRequest("Phone number is wrong");
            }
            var customerId = _unitOfWork._orderRepository.GetCustomerId(user.Id);
            var wallet = _unitOfWork._walletRepository.GetWalletByCustomerId(customerId);
            if(wallet.OTP != request.OTP)
            {
                return BadRequest("Invalid OTP");
            }
            var transaction = new WalletTransaction
            {
                WalletId = wallet.Id,
                MadeBy = customerId,
                Amount = request.Amount,
                CreatedAt = DateTime.Now
            };
            wallet.Amount += request.Amount;
            wallet.OTP = null;
            _unitOfWork.Commit();
            return Ok(new { message = "Wallet charged successfully.", wallet_balance = wallet.Amount });

        }

    }
}
