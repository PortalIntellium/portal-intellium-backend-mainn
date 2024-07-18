using Business.Repository.UserProfileDetailRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserProfileDetailRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserProfileDetailRepository
{
    public class UserProfileDetailsManager : IUserProfileDetailService
    {
        private readonly IUserProfileDetailDal _userProfileDetailDal;
        private readonly IUserDal _userDal;
        public UserProfileDetailsManager(IUserProfileDetailDal userProfileDetailDal, IUserDal userDal)
        {
            _userProfileDetailDal = userProfileDetailDal;
            _userDal = userDal;
        }

        public IResult Add(UserProfileDetails userProfileDetails)
        {
            var user = _userDal.Get(x=>x.Id == userProfileDetails.UserId);
            userProfileDetails.BirthDate = user.BirthDate;
            userProfileDetails.Age = DateTime.Now.Year - user.BirthDate.Year;
            _userProfileDetailDal.Add(userProfileDetails);
            
            return new SuccessResult(UserProfileDetailsMessages.AddedUserProfileDetail);
        }

        public IResult Delete(long id)
        {
            var userprofile = _userProfileDetailDal.Get(x=>x.Id == id);
            if(userprofile == null)
            {
                return new ErrorDataResult<UserProfileDetails>("UserProfile not found");

            }
            _userProfileDetailDal.Delete(userprofile);

            return new SuccessResult(UserProfileDetailsMessages.DeletedUserProfileDetail);
        }

        public IDataResult<List<UserProfileDetails>> GetAll()
        {
            return new SuccessDataResult<List<UserProfileDetails>>(_userProfileDetailDal.GetAll());
        }

        public IDataResult<UserProfileDetails> GetUserProfileDetailsByUserId(long userId)
        {
            var userProfileDetails = _userProfileDetailDal.Get(x => x.UserId == userId);
            if (userProfileDetails == null)
            {
                return new ErrorDataResult<UserProfileDetails>("User not found");
            }
            return new SuccessDataResult<UserProfileDetails>(userProfileDetails);
        }

        public IResult Update(UserProfileDetails userProfileDetails)
        {
            var id = userProfileDetails.Id;
            var result = _userProfileDetailDal.Get(x => x.Id == id);
            if (result == null)
            {
                return new ErrorResult("UserProfileDetail id  not found");

            }
            _userProfileDetailDal.Update(userProfileDetails);
            return new SuccessResult(UserProfileDetailsMessages.UpdatedUserProfileDetail);
        }
    }
}
