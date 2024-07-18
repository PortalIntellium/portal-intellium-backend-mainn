using Business.File;
using Business.Helpers;
using Business.Repository.RolesForUsersRepository;
using Business.Repository.UserCustomerRepository;
using Business.Repository.UserRepository.Constants;
using Business.Repository.UserRepository.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserPermissionRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.UserRepository
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserPermissionDal _userPermissionDal;
        private readonly IUserCustomerService _userCustomerService;
        private readonly IRolesForUsersService _rolesForUsersService;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IUserPermissionDal userPermissionDal, IUserCustomerService userCustomerService, IRolesForUsersService rolesForUsersService, IFileService fileService)
        {
            _userDal = userDal;
            _userPermissionDal = userPermissionDal;
            _userCustomerService = userCustomerService;
            _rolesForUsersService = rolesForUsersService;
            _fileService = fileService;
        }

        //[SecuredOperation("admin,user.add")]//yetki kontrolü
        [ValidationAspect(typeof(UserValidator))]//validation işlemi
        public IResult Add(User user)
        {

            _userDal.Add(user);
            int day = UserPermissionCalculate.CalculateTotalLeave(user.AddetAt, user.BirthDate);
            UserPermission userPermission = new()
            {
                UserId = user.Id,
                TotalLeave = day,
                ReaminingLeave = day,
            };

            _userPermissionDal.Add(userPermission);
            return new SuccessResult(UserMessages.AddedUser);
        }

        public IDataResult<List<UserDto>> GetAll()
        {
            return new SuccessDataResult<List<UserDto>>(_userDal.GetAllForUserList());
        }

        public User GetByConfirmValue(string value)
        {
            return _userDal.Get(p => p.ConfirmValue == value);
        }

        public User GetByMail(string email)
        {
            var result = _userDal.Get(p => p.Email == email);
            return result;
        }

        public IDataResult<List<UserDto>> GetByName(string name)
        {
            return new SuccessDataResult<List<UserDto>>(_userDal.GetByName(name));
        }

        public IDataResult<List<OperationClaim>> GetOperationClaims(User user, long customerId)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user, customerId));
        }

        public IDataResult<UserDto> GetUserAsDtoById(long id)
        {
            var result = _userDal.GetUserAsDtoById(id);
            return new SuccessDataResult<UserDto>(result);
        }

        public IDataResult<User> GetById(long id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == id), UserMessages.GetUser);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public IResult UserExists(string email)
        {
            var result = GetByMail(email);

            if (result != null)
            {
                return new ErrorResult(UserMessages.UserAlreadyExist);
            }
            return new SuccessResult();
        }

        List<OperationClaim> IUserService.GetOperationClaims(User user, long customerId)
        {
            return _userDal.GetClaims(user, customerId);
        }

        public IResult UpdateAsDto(EditUserDto editUser)
        {
            var updatedUser = _userDal.Get(user => user.Id.Equals(editUser.Id));
            if (updatedUser == null) return new ErrorResult(UserMessages.UserNotFound);

            updatedUser.Name = editUser.Name;
            updatedUser.Email = editUser.Email;
            updatedUser.Language = editUser.Language;
            updatedUser.IsActive = editUser.IsActive;
            updatedUser.BirthDate = editUser.BirthDate;

            if (!editUser.NewPassword.IsNullOrEmpty())
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(editUser.NewPassword!, out passwordHash, out passwordSalt);

                updatedUser.PasswordHash = passwordHash;
                updatedUser.PasswordSalt = passwordSalt;
            }


            var userCustomer = _userCustomerService.GetByUserId(updatedUser.Id).Data;
            userCustomer.CustomerId = editUser.CustomerId;
            _userCustomerService.Update(userCustomer);

            var roleForUser = _rolesForUsersService.GetRolesForUsersByUserId(updatedUser.Id).Data;
            roleForUser.RoleId = editUser.UserRoleId;
            _rolesForUsersService.Update(roleForUser);

            _userDal.Update(updatedUser);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public async Task<IResult> ChangeImage(IFormFile image, long userId)
        {
            var user = _userDal.Get(u => u.Id.Equals(userId));
            if (user == null) return new ErrorResult(UserMessages.UserNotFound);

            if (!user.ImageUrl.IsNullOrEmpty())
            {
                _fileService.Delete(user.ImageUrl!, FileType.PROFILE_IMAGE);
            }

            var result = await _fileService.Save(image, FileType.PROFILE_IMAGE);
            user.ImageUrl = result.Data.FilePath;

            _userDal.Update(user);

            return new SuccessResult(UserMessages.ChangedProfileImage);
        }

        public IResult RemoveImage(long userId)
        {
            var user = _userDal.Get(u => u.Id.Equals(userId));
            if (user == null) return new ErrorResult(UserMessages.UserNotFound);

            if (!user.ImageUrl.IsNullOrEmpty())
            {
                _fileService.Delete(user.ImageUrl!, FileType.PROFILE_IMAGE);
            }

            user.ImageUrl= null;
            _userDal.Update(user);

            return new SuccessResult(UserMessages.RemovedImage);
        }
    }
}
