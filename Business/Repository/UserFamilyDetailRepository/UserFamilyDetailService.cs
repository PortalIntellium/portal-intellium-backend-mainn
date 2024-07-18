using Business.Repository.UserFamilyDetailRepository.Constans;
using Business.Repository.UserProfileDetailRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserFamilyDetailRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserFamilyDetailRepository
{
    public class UserFamilyDetailService : IUserFamilyDetailService
    {
        private readonly IUserFamilyDetailDal _userFamilyDetailDal;

        public UserFamilyDetailService(IUserFamilyDetailDal userFamilyDetailDal)
        {
            _userFamilyDetailDal = userFamilyDetailDal;
        }

        public IResult Add(UserFamilyDetail userFamilyDetail)
        {
            _userFamilyDetailDal.Add(userFamilyDetail);
            return new SuccessResult(UserFamilyDetailMessages.AddedUserFamilyDetail);
        }

        public IResult Delete(long id)
        {
            var result = _userFamilyDetailDal.Get(x=>x.Id == id);

            _userFamilyDetailDal.Delete(result);
            return new SuccessResult(UserFamilyDetailMessages.DeletedUserFamilyDetail);
        }

        public IDataResult<List<UserFamilyDetail>> GetAll()
        {
            return new SuccessDataResult<List<UserFamilyDetail>>(_userFamilyDetailDal.GetAll());
        }

        public IDataResult<List<UserFamilyDetail>> GetUserFamilyDetailsByUserId(long userId)
        {
            var userFamilyDetails = _userFamilyDetailDal.GetUserFamilyDetailByUserId(userId);
            if (userFamilyDetails == null)
            {
                return new ErrorDataResult<List<UserFamilyDetail>>("User not found");
            }
            return new SuccessDataResult<List<UserFamilyDetail>>(userFamilyDetails);
        }

        public IResult Update(UserFamilyDetail userFamilyDetail)
        {
            _userFamilyDetailDal.Update(userFamilyDetail);
            return new SuccessResult(UserFamilyDetailMessages.UpdatedUserFamilyDetail);
        }
    }
}
