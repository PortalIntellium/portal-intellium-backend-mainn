using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserProfileDetailRepository
{
    public interface IUserProfileDetailService
    {
        IResult Add(UserProfileDetails userProfileDetails);
        IResult Update(UserProfileDetails userProfileDetails);
        IResult Delete(long id);
        IDataResult<List<UserProfileDetails>> GetAll();
        IDataResult<UserProfileDetails> GetUserProfileDetailsByUserId(long userId);
    }
}
