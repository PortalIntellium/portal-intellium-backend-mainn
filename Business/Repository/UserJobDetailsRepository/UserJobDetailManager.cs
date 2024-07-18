using Business.Repository.UserJobDetailsRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserJobDetailRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserJobDetailsRepository
{
    public class UserJobDetailManager : IUserJobDetailService
    {
        private readonly IUserJobDetailDal _userJobDetailDal;

        public UserJobDetailManager(IUserJobDetailDal userJobDetailDal)
        {
            _userJobDetailDal = userJobDetailDal;
        }

        public IResult Add(UserJobDetail userJobDetails)
        {
            _userJobDetailDal.Add(userJobDetails);
            return new SuccessResult(UserJobDetailsMessages.AddedUserJobDetail);
        }

        public IResult Delete(long id)
        {
            var result = _userJobDetailDal.Get(x => x.Id == id);
            if(result == null)
            {

                return new ErrorDataResult<UserJobDetail>("UserJobDetail not found");
            }
            _userJobDetailDal.Delete(result);
            return new SuccessResult(UserJobDetailsMessages.DeletedUserJobDetail);
        }

        public IDataResult<List<UserJobDetail>> GetAll()
        {
            return new SuccessDataResult<List<UserJobDetail>>(_userJobDetailDal.GetAll());
        }

        public IDataResult<UserJobDetail> GetByUserId(long userId)
        {
            var userJobDetail = _userJobDetailDal.Get(u => u.UserId.Equals(userId));
            if (userJobDetail == null)
            {
                return new ErrorDataResult<UserJobDetail>("UserId not found");
            }
            return new SuccessDataResult<UserJobDetail>(userJobDetail);
        }

        public IResult Update(UserJobDetail userJobDetails)
        {
            _userJobDetailDal.Update(userJobDetails);
            return new SuccessResult(UserJobDetailsMessages.UpdatedUserJobDetail);
        }
    }
}
