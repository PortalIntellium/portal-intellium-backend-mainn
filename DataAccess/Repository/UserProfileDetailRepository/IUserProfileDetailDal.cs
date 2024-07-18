using Core.DataAccess;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserProfileDetailRepository
{
    public interface IUserProfileDetailDal: IEntityRepository<UserProfileDetails>
    {
        UserProfileDetails GetUserProfileDetailByUserId(long userId);
    }
}
