using Business.Repository.CustomerRepository;
using Business.Repository.CustomerRepository.Constants;
using Business.Repository.MailRepository;
using Business.Repository.MailRepository.Constans;
using Business.Repository.MailTemplatesRepository;
using Business.Repository.RolesForUsersRepository;
using Business.Repository.UserCustomerRepository;
using Business.Repository.UserRepository;
using Business.Repository.UserRepository.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly ICustomerService _customerService;
        private readonly IUserCustomerService _userCustomerService;
        private readonly IRolesForUsersService _rolesForUsersService;
        private readonly IMailService _mailService;
        private readonly IMailParameterService _mailParameter;
        private readonly IMailTemplatesService _mailTemplatesService;
        private readonly IConfiguration _configuration;

        public AuthManager(IUserService userService, IUserCustomerService userCustomerService, ITokenHandler tokenHandler, IMailService mailService, IMailParameterService mailParameter, IMailTemplatesService mailTemplatesService, ICustomerService customerService, IRolesForUsersService rolesForUsersService, IConfiguration configuration)
        {
            _userService = userService;
            _userCustomerService = userCustomerService;
            _tokenHandler = tokenHandler;
            _mailService = mailService;
            _mailParameter = mailParameter;
            _mailTemplatesService = mailTemplatesService;
            _customerService = customerService;
            _rolesForUsersService = rolesForUsersService;
            _configuration = configuration;
        }

        public IDataResult<Token> CreateAccessToken(User user, long customerId)
        {
            var claims = _userService.GetOperationClaims(user, customerId);
            var customer = _customerService.GetById(customerId).Data;
            var accessToken = _tokenHandler.CreateToken(user, claims, customerId, customer.CustomerName);
            accessToken.CustomerId = customer.CustomerId;
            return new SuccessDataResult<Token>(accessToken);
        }

        public IDataResult<User> GetById(long id)
        {
            var result = _userService.GetById(id).Data;
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> GetByMailConfirmValue(string value)
        {
            return new SuccessDataResult<User>(_userService.GetByConfirmValue(value));
        }

        void SendConfirmEmail(User user)
        {
            //daha sonra dinamik hale getirilecektir.
            string subject = "Kullanıcı Kayıt Onay Maili";
            string body = "Kullanıcı sisteme kayıt oldu. Kaydınızı tamamlamak için aşağıdaki linke tıklayınız.";
            //string link = "https://localhost:44373/api/Auth/confirmuser?value=" + user.ConfirmValue;
            // Confirm linki appsetting.json dan çekildi
            string link = _configuration["Links:ConfirmEmailLink"] + user.ConfirmValue;

            string linkDescription = "Kayıt Onaylamak İçin Tıklayın";

            var mailTemplate = _mailTemplatesService.GetByTemplateName("Kayıt", 1);//Varsayılan şirket id si

            //templateBody kullanılacak şablona göre değiştirilecektir.
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);

            var parameter = _mailParameter.GetParameters(1);
            SendMailDto sendMailDto = new SendMailDto()
            {
                mailParameters = parameter.Data,
                email = user.Email,
                subject = subject,
                body = templateBody
            };
            _mailService.SendMail(sendMailDto);//mail gönder

            user.MailConfirmDate = DateTime.Now;//son gönderilen mail tarihini tutar
            _userService.Update(user);
        }

        public IResult Update(User user)
        {
            _userService.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.GetByMail(email);

            if (result != null)
            {
                return new ErrorResult(UserMessages.UserAlreadyExist);
            }
            return new SuccessResult();
        }

        IResult IAuthService.SendConfirmEmail(User user)
        {
            if (user.MailConfirm == true)
            {
                return new ErrorResult(MailMessages.MailAlreadyConfirm);
            }

            DateTime confirmMailDate = (DateTime)user.MailConfirmDate;
            DateTime now = DateTime.Now;
            if (confirmMailDate.ToShortDateString() == now.ToShortDateString())
            {
                if (confirmMailDate.Hour == now.Hour && confirmMailDate.AddMinutes(5).Minute <= now.Minute)
                {
                    SendConfirmEmail(user);
                    return new SuccessResult(MailMessages.MailConfirmSendSuccesfull);
                }
                else
                {
                    return new ErrorResult(MailMessages.MailConfirmTimeHasNotExpired);
                }
            }
            SendConfirmEmail(user);
            return new SuccessResult(MailMessages.MailConfirmSendSuccesfull);



        }

        public IResult CustomerExists(Customer customer)
        {
            var result = _customerService.CustomerExists(customer);
            if (!result.Success)
            {
                return new ErrorResult(CustomerMessages.CustomerAlreadyExist);
            }
            return new SuccessResult(CustomerMessages.AddedCustomer);
        }

        public IDataResult<UserCustomer> GetCustomer(long userId)
        {
            return new SuccessDataResult<UserCustomer>(_customerService.GetCustomer(userId).Data);
        }

        public IDataResult<User> Login(UserForLoginDto userForLogin)
        {
            var userToCheck = _userService.GetByMail(userForLogin.Email);
            if (userToCheck == null)//kullanıcı doğrulama
            {
                return new ErrorDataResult<User>(UserMessages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(UserMessages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, UserMessages.SuccessfulLogin);
        }



        [TransactionScopeAspect]//burada hem kullanıcı hemde şirket kaydı yapıldığı için buraya konuldu
        public IDataResult<UserCustomerDto> Register(UserForRegisterDto userForRegister)
        {

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                BirthDate = userForRegister.BirthDate,
                Language = userForRegister.Language,
                Email = userForRegister.Email,
                AddetAt = DateTime.UtcNow,
                IsConfirm = true,
                MailConfirm = false,
                IsActive = true,
                MailConfirmDate = DateTime.UtcNow,
                ConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name,

            };

            //ValidationTool.Validate(new UserValidator(), user);//user vakidation kontrolü
            //ValidationTool.Validate(new CompanyValidator(), company);//company validation kontrolü

            //_companyService.Add(company);//şirket kaydet

            _userService.Add(user);//kullanıcı kaydet

            UserCustomer userCustomer = new()
            {
                UserId = user.Id,
                CustomerId = userForRegister.CustomerId
            };
            _userCustomerService.Add(userCustomer);
            //_userCustomerService.UserCustomerAdd(userForRegister.CustomerId, user.Id);//kullanıcı ve müşteri arasındaki ilişkiyi kaydet

            RolesForUsers rolesForUsers = new()
            {
                RoleId = userForRegister.UserRoleId,
                UserId = user.Id
            };
            _rolesForUsersService.Add(rolesForUsers);



            UserCustomerDto userCustomerDto = new UserCustomerDto()
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = userForRegister.BirthDate,
                Language = userForRegister.Language,
                Email = user.Email,
                AddetAt = user.AddetAt,
                CustomerId = userForRegister.CustomerId,
                IsActive = true,
                MailConfirm = user.MailConfirm,
                MailConfirmDate = user.MailConfirmDate,
                ConfirmValue = user.ConfirmValue,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };
            //SendConfirmEmail(user);

            return new SuccessDataResult<UserCustomerDto>(userCustomerDto, UserMessages.UserRegistered);
        }
        public IDataResult<User> RegisterForSecondAccount(UserForRegisterDto userForRegister, string password, long customerId)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                BirthDate = userForRegister.BirthDate,
                Language = userForRegister.Language,
                Email = userForRegister.Email,
                AddetAt = DateTime.UtcNow,
                IsConfirm = true,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                ConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name,
            };
            _userService.Add(user);

            UserCustomer userCustomer = new()
            {
                UserId = user.Id,
                CustomerId = customerId
            };
            _userCustomerService.Add(userCustomer);
            SendConfirmEmail(user);


            return new SuccessDataResult<User>(user, UserMessages.UserRegistered);
        }

        [TransactionScopeAspect]//burada hem kullanıcı hemde şirket kaydı yapıldığı için buraya konuldu
        public IDataResult<User> RegisterForCustomerAccount(UserForCustomerRegisterDto userForCustomerRegister, string password, long customerId)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForCustomerRegister.Password, out passwordHash, out passwordSalt);
            var user = new User()
            {

                Email = userForCustomerRegister.Email,
                AddetAt = DateTime.UtcNow,
                IsConfirm = true,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                ConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForCustomerRegister.Name
            };
            _userService.Add(user);
            UserCustomer userCustomer = new()
            {
                UserId = user.Id,
                CustomerId = customerId
            };
            _userCustomerService.Add(userCustomer);
            SendConfirmEmail(user);
            return new SuccessDataResult<User>(user, UserMessages.UserRegistered);
        }



        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userService.GetByMail(email));
        }

        public IResult SendForgotPasswordEmail(User user, string value)
        {

            string subject = "Şifremi Unuttum";
            string body = "Şifrenizi unuttuğunuzu belirttiniz. Aşağıdaki linke tıklayarak şifrenizi yeniden belirleyebilirsiniz.";
            //string link = "http://localhost:44373/api/Auth/forgotPassword/" + value;
            // Link appsettings üzerinden çekiliyor
            string link = _configuration["Links:ForgotPasswordLink"] + value;
            string liknDescription = "Şifrenizi Tekrar Belirlemek İçin Tıklayınız.";

            var mailTemplate = _mailTemplatesService.GetByTemplateName("Kayıt", 1);
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", liknDescription);

            var mailParameter = _mailParameter.GetParameters(1);
            SendMailDto sendMailDto = new SendMailDto()
            {
                mailParameters = mailParameter.Data,
                email = user.Email,
                subject = subject,
                body = body

            };
            _mailService.SendMail(sendMailDto);
            return new SuccessResult(MailMessages.MailSendSuccesfull);
        }

        public IResult ChangePassword(User user, string newPassword)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userService.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);
        }
    }
}
