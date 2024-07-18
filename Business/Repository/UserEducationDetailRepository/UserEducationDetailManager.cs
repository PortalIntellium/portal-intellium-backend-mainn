using Business.Repository.UserJobDetailsRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.UserCertificateDetailRepository;
using DataAccess.Repository.UserEducationDetailRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserEducationDetailRepository
{
    public class UserEducationDetailManager : IUserEducationDetailService
    {
        private readonly IUserEducationDetailDal _userEducationDetailDal;

        public UserEducationDetailManager(IUserEducationDetailDal userEducationDetailDal)
        {
            _userEducationDetailDal = userEducationDetailDal;
        }

        public IResult Add(UserEducationDetail userEducationDetails)
        {
            _userEducationDetailDal.Add(userEducationDetails);
            return new SuccessResult(UserEducationDetailMessages.AddedUserEducationDetail);
        }

        public IResult Delete(long id)
        {
            var result = _userEducationDetailDal.Get(x=>x.Id == id);
            _userEducationDetailDal.Delete(result);
            return new SuccessResult(UserEducationDetailMessages.DeletedUserEducationDetail);
        }

        public IDataResult<List<UserEducationDetail>> GetAll()
        {
            return new SuccessDataResult<List<UserEducationDetail>>(_userEducationDetailDal.GetAll());
        }

        public IDataResult<List<UserEducationDetail>> GetUserEducationDetailsByUserId(long userId)
        {
            var userEducationDetails = _userEducationDetailDal.GetUserEducationDetailByUserId(userId);
            if(userEducationDetails == null)
            {
                return new ErrorDataResult<List<UserEducationDetail>>("User Education Details not found");
            }
            return new SuccessDataResult<List<UserEducationDetail>>(userEducationDetails);
        }

        public IResult Update(UserEducationDetail userEducationDetails)
        {
            _userEducationDetailDal.Update(userEducationDetails);
            return new SuccessResult(UserEducationDetailMessages.UpdatedUserEducationDetail);
        }
    }
}
