using Business.Repository.UserJobDetailsRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserCertificateDetailRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserCertificateDetailRepository
{
    public class UserCertificateDetailManager : IUserCertificateDetailService
    {
        private readonly IUserCertificateDetailDal _userCertificateDetailDal;

        public UserCertificateDetailManager(IUserCertificateDetailDal userCertificateDetailDal)
        {
            _userCertificateDetailDal = userCertificateDetailDal;
        }

        public IResult Add(UserCertificateDetail userCertificateDetail)
        {
            _userCertificateDetailDal.Add(userCertificateDetail);
            return new SuccessResult(UserCertificateDetailMessages.AddedUserCertificateDetail);
        }

        public IResult Delete(long id)
        {
            var result = _userCertificateDetailDal.Get(x => x.Id == id);
            _userCertificateDetailDal.Delete(result);
            return new SuccessResult(UserCertificateDetailMessages.DeletedUserCertificateDetail);

        }

        public IDataResult<List<UserCertificateDetail>> GetAll()
        {
            return new SuccessDataResult<List<UserCertificateDetail>>(_userCertificateDetailDal.GetAll());
        }

        public IDataResult<List<UserCertificateDetail>> GetUserCertificateDetailByUserId(long userId)
        {
            var userCertificateDetail = _userCertificateDetailDal.GetUserCertificateDetailByUserId(userId);
            if(userCertificateDetail == null)
            {
                return new ErrorDataResult<List<UserCertificateDetail>>("User certificate detail not found");
            }
            return new SuccessDataResult<List<UserCertificateDetail>>(userCertificateDetail);
        }

        public IResult Update(UserCertificateDetail userCertificateDetail)
        {
            _userCertificateDetailDal.Update(userCertificateDetail);
            return new SuccessResult(UserCertificateDetailMessages.UpdatedUserCertificateDetail);
        }
    }
}
