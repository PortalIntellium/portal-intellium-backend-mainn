using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserProfileDetailRepository
{
    public class EfUserProfileDetailDal : EfEntityRepositoryBase<UserProfileDetails, PortalContext>, IUserProfileDetailDal
    {
        public UserProfileDetails GetUserProfileDetailByUserId(long userId)
        {
            using (var context = new PortalContext())
            {
                var result = (from userProfileDetail in context.UserProfileDetails where userProfileDetail.UserId == userId select userProfileDetail).SingleOrDefault();

                return result;
            }
        }
    }
}
