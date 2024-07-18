using Business.Repository.UserLanguageDetailRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserLanguageDetailRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserLanguageDetailRepository
{
    public class UserLanguageDetailManager : IUserLanguageDetailService
    {
        private readonly IUserLanguageDetailDal _userLanguageDetailDal;

        public UserLanguageDetailManager(IUserLanguageDetailDal userLanguageDetailDal)
        {
            _userLanguageDetailDal = userLanguageDetailDal;
        }

        public IResult Add(UserLanguageDetail userLanguageDetail)
        {
            _userLanguageDetailDal.Add(userLanguageDetail);
            return new SuccessResult(UserLanguageDetailMessages.AddedUserLanguageDetail);
        }

        public IResult Delete(long id)
        {
            var result = _userLanguageDetailDal.Get(x => x.Id == id);
            _userLanguageDetailDal.Delete(result);
            return new SuccessResult(UserLanguageDetailMessages.DeletedUserLanguageDetail);
        }

        public IDataResult<List<UserLanguageDetail>> GetAll()
        {
            return new SuccessDataResult<List<UserLanguageDetail>>(_userLanguageDetailDal.GetAll());
        }

        public IDataResult<List<UserLanguageDetail>> GetUserLanguageByUserId(long userId)
        {
            var userLanguageDetail = _userLanguageDetailDal.GetById(userId);
            if(userLanguageDetail == null)
            {
                return new ErrorDataResult<List<UserLanguageDetail>>("User Id Not Found");
            }
            return new SuccessDataResult<List<UserLanguageDetail>>(userLanguageDetail);
        }

        public IResult Update(UserLanguageDetail userLanguageDetail)
        {
            _userLanguageDetailDal.Update(userLanguageDetail);
            return new SuccessResult(UserLanguageDetailMessages.UpdatedUserLanguageDetail);
        }
    }
}
