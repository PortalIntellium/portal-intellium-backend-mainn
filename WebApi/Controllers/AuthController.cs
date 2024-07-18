using Business.Authentication;
using Business.Repository.ForgotPasswordRepository;
using Core.Utilities.Hashing;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IForgotPasswordService _forgotPasswordService;

        public AuthController(IAuthService authService, IForgotPasswordService forgotPasswordService)
        {
            _authService = authService;
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CustomerId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            //if (registerResult.Success)
            //{
            //    return Ok(registerResult);
            //}
            return BadRequest(registerResult.Message);
        }

        [HttpPost("registerSecondAccount")]
        public IActionResult RegisterSecondAccount(UserForRegisterDto userForRegister)
        {
            var userExist = _authService.UserExists(userForRegister.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerResult = _authService.RegisterForSecondAccount(userForRegister, userForRegister.Password, userForRegister.CustomerId);
            var result = _authService.CreateAccessToken(registerResult.Data, userForRegister.CustomerId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("RegisterForCustomerAccount")]
        public IActionResult RegisterForCustomerAccount(UserForCustomerRegisterDto userForCustomerRegister)
        {
            var userExist=_authService.UserExists(userForCustomerRegister.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerResult=_authService.RegisterForCustomerAccount(userForCustomerRegister, userForCustomerRegister.Password, userForCustomerRegister.CustomerId);
            var result=_authService.CreateAccessToken(registerResult.Data,userForCustomerRegister.CustomerId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            if (userToLogin.Data.IsActive)
            {
                var userCustomer = _authService.GetCustomer(userToLogin.Data.Id).Data;
                var result = _authService.CreateAccessToken(userToLogin.Data, userCustomer.CustomerId);

                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Kullanıcı pasif durumda olduğu için giriş yapamaz. Aktif etmek için yöneticinize başvurunuz.");

        }

        [HttpGet("confirmuser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            var result = _authService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("sendConfirmEmail")]
        public IActionResult SendConfirmEmail(long id)
        {
            var user = _authService.GetById(id).Data;
            var result = _authService.SendConfirmEmail(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("forgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var user=_authService.GetByEmail(email).Data;
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı!");
            }
            var lists = _forgotPasswordService.GetListByUserId(user.Id).Data;
            lists.ForEach(list =>
            {
                list.IsActive = false;
                _forgotPasswordService.Update(list);
            });

            var forgotPassword = _forgotPasswordService.CreateForgotPassword(user).Data;
            var result = _authService.SendForgotPasswordEmail(user, forgotPassword.Value);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);



        }

        [HttpGet("forgotPasswordLinkCheck")]
        public IActionResult ForgotPasswordLinkCheck(string value)
        {
            var result = _forgotPasswordService.GetForgotPassword(value);
            if (result == null)
            {
                return BadRequest("Geçersiz bağlantı!");
            }
            if (result.IsActive==true)
            {
                DateTime sendDate=DateTime.Now.AddHours(-1);
                DateTime nowDate = DateTime.Now;
                if (result.SendDate >=sendDate && result.SendDate<=nowDate)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Geçersiz bağlantı!");
                }
            }
            else
            {
                return BadRequest("Geçersiz bağlantı!");
            }
        }

        [HttpPost("changePasswordToForgotPassword")]
        public IActionResult ChangePasswordToForgotPassword(ForgotPasswordDto passwordDto)
        {
            var forgotPasswordResult = _forgotPasswordService.GetForgotPassword(passwordDto.Value);
            forgotPasswordResult.IsActive = false;//link ikinci defa kullanılamaz
            var userResult=_authService.GetById(forgotPasswordResult.UserId).Data;

            var result=_authService.ChangePassword(userResult, passwordDto.Password);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);

        }
    }
}
