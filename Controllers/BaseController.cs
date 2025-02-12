using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserInfoService _userInfoService;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(UserInfoService userInfoService, IUnitOfWork unitOfWork)
        {
            _userInfoService = userInfoService;
            _unitOfWork = unitOfWork;
        }

        protected User? GetActiveUser()
        {
            var phone = _userInfoService.GetUserIdFromToken();
            var user = _unitOfWork._userRepository.GetUserByPhone(phone);
            return user;
        }
    }
}
