using FuelGo.Inerfaces;
using FuelGo.Models;
using FuelGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuelGo.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserInfoService _userInfoService;
        protected readonly IUserRepository _userRepository;
        public BaseController(UserInfoService userInfoService, IUserRepository userRepository)
        {
            _userInfoService = userInfoService;
            _userRepository = userRepository;
        }

        protected User? GetActiveUser()
        {
            var phone = _userInfoService.GetUserIdFromToken();
            var user = _userRepository.GetUserByPhone(phone);
            return user;
        }
    }
}
