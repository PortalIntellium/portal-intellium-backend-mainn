using Business.BusinessAspects;
using Business.Repository.UserOperationClaimRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserOperationClaimRepository;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserOperationClaimRepository
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }
        //[SecuredOperation("Admin,UserOperationClaim.Add")]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessages.Added);
        }
        //[SecuredOperation("Admin,UserOperationClaim.Delete")]

        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessages.Deleted);
        }
        //[SecuredOperation("Admin,UserOperationClaim.GetList")]

        public IDataResult<List<UserOperationClaim>> GetAll(long userId, long customerId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(p=>p.UserId==userId && p.CustomerId== customerId),UserOperationClaimMessages.Listed);
        }
        //[SecuredOperation("Admin,UserOperationClaim.Get")]

        public IDataResult<UserOperationClaim> GetById(long id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(i=>i.Id == id),UserOperationClaimMessages.GetUserClaim);
        }

        public IDataResult<List<UserOperationClaimDto>> GetListDto(long userId, long customerId)
        {
            return new SuccessDataResult<List<UserOperationClaimDto>>(_userOperationClaimDal.GetListDto(userId, customerId));
        }

        //[SecuredOperation("Admin,UserOperationClaim.Update")]

        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessages.Updated);
        }
    }
}
