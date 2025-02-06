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
        protected readonly IUnitOfWork _UOF;
        private UserInfoService userInfoService;
        private IUserRepository userRepository;

        public BaseController(UserInfoService userInfoService, IUserRepository userRepository)
        {
            this.userInfoService = userInfoService;
            this.userRepository = userRepository;
        }

        public BaseController(UserInfoService userInfoService, IUserRepository userRepository, IUnitOfWork uof)
        {
            _userInfoService = userInfoService;
            _userRepository = userRepository;
            _UOF = uof;
        }

        protected User? GetActiveUser()
        {
            var phone = _userInfoService.GetUserIdFromToken();
            var user = _userRepository.GetUserByPhone(phone);
            return user;
        }
    }
}
