using Business.Repository.UserJobExperienceRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.CustomerRepository;
using DataAccess.Repository.UserJobExperienceRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;

namespace Business.Repository.UserJobExperienceRepository
{
    public class UserJobExperienceManager : IUserJobExperienceService
    {
        private readonly IUserJobExperienceDal _userJobExperienceDal;
        private readonly IUserDal _userDal;
        private readonly ICustomerDal _customerDal;
        public UserJobExperienceManager(IUserJobExperienceDal userJobExperienceDal, IUserDal userDal, ICustomerDal customerDal)
        {
            _userJobExperienceDal = userJobExperienceDal;
            _userDal = userDal;
            _customerDal = customerDal;
        }

        public IResult Add(UserJobExperience userJobExperience)
        {
            var userExists = _userDal.Get(u => u.Id.Equals(userJobExperience.UserId));
            if (userExists == null) { return new ErrorResult(UserJobExperienceMessages.UserNotFound); }

            _userJobExperienceDal.Add(userJobExperience);
            return new SuccessResult(UserJobExperienceMessages.AddedUserJobExperience);
        }

        public IResult Delete(long id)
        {
            var result = _userJobExperienceDal.Get(u => u.Id.Equals(id));
            if (result == null)
            {
                return new ErrorResult(UserJobExperienceMessages.UserJobExperienceNotFound);
            }
            _userJobExperienceDal.Delete(result);
            return new SuccessResult(UserJobExperienceMessages.DeletedUserJobExperience);

        }

        public IDataResult<List<UserJobExperience>> GetAllByUserId(long userId)
        {
            var result = _userJobExperienceDal.GetAll(u => u.UserId.Equals(userId));
            if (result.Count == 0)
            {
                return new ErrorDataResult<List<UserJobExperience>>(UserJobExperienceMessages.UserHasNoExperience);
            }
            return new SuccessDataResult<List<UserJobExperience>>(result, UserJobExperienceMessages.ListedUserJobExperiences);
        }

        public IResult Update(UserJobExperience userJobExperience)
        {
            var userExists = _userDal.Get(u => u.Id.Equals(userJobExperience.UserId));
            if (userExists == null) { return new ErrorResult(UserJobExperienceMessages.UserNotFound); }

            _userJobExperienceDal.Update(userJobExperience);
            return new SuccessResult(UserJobExperienceMessages.UpdatedUserJobExperience);
        }
    }
}
