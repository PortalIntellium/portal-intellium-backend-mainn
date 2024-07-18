using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Authentication
{
    public interface IAuthService
    {
        IDataResult<UserCustomerDto> Register(UserForRegisterDto userForRegister);
        IDataResult<User> RegisterForSecondAccount(UserForRegisterDto userForRegister, string password, long customerId);
        IDataResult<User> RegisterForCustomerAccount(UserForCustomerRegisterDto userForCustomerRegister, string password, long customerId);
        IDataResult<User> Login(UserForLoginDto userForLogin);
        IDataResult<User> GetByMailConfirmValue(string value);
        IDataResult<User> GetById(long id);
        IResult UserExists(string email);
        IResult Update(User user);
        IResult CustomerExists(Customer customer);
        IResult SendConfirmEmail(User user);
        IDataResult<Token> CreateAccessToken(User user, long customerId);
        IDataResult<UserCustomer> GetCustomer(long userId);
        IDataResult<User> GetByEmail(string email);
        IResult SendForgotPasswordEmail(User user, string value);
        IResult ChangePassword(User user, string newPassword);

    }
}
