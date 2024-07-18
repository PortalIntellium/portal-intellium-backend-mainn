using Business.Repository.UserLanguageDetailRepository.Constans;
using Business.Repository.UserRoleRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserRoleRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserRoleRepository
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public IResult Add(UserRole userRole)
        {
            _userRoleDal.Add(userRole);
            return new SuccessResult(UserRoleMessages.AddedUserRole);
        }

        public IResult Delete(long id)
        {
            var result = _userRoleDal.Get(x => x.Id == id);
            _userRoleDal.Delete(result);
            return new SuccessResult(UserRoleMessages.DeletedUserRole);
        }

        public IDataResult<List<UserRole>> GetAll()
        {
           return new SuccessDataResult<List<UserRole>>(_userRoleDal.GetAll());
        }

        public IResult Update(UserRole userRole)
        {
            _userRoleDal.Update(userRole);
            return new SuccessResult(UserRoleMessages.UpdatedUserRole);
        }
    }
}
