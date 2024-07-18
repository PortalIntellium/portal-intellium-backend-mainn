using Business.Repository.UserPermissionRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserPermissionRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserPermissionRepository
{
    public class UserPermissionManager : IUserPermissionService
    {
        private readonly IUserPermissionDal _userPermissionDal;

        public UserPermissionManager(IUserPermissionDal userPermissionDal)
        {
            _userPermissionDal = userPermissionDal;
        }

        public IResult Add(UserPermission userPermission)
        {
            _userPermissionDal.Add(userPermission);
            return new SuccessResult(UserPermissionMessages.AddedUserPermission);
        }

        public IResult Delete(UserPermission userPermission)
        {
            _userPermissionDal.Delete(userPermission);
            return new SuccessResult(UserPermissionMessages.DeletedUserPermission);
        }

        public IDataResult<List<UserPermission>> GetAll()
        {
            return new SuccessDataResult<List<UserPermission>>(_userPermissionDal.GetAll());
        }

        public IResult GetUserPermissionById(int id)
        {
            return new SuccessDataResult<UserPermission>(_userPermissionDal.GetUserPermissionByUserId(id));
        }

        public IResult Update(UserPermission userPermission)
        {
            _userPermissionDal.Update(userPermission);
            return new SuccessResult(UserPermissionMessages.UpdatedUserPermission);
        }

        
    }
}
