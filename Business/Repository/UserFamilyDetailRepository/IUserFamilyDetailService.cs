using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserFamilyDetailRepository
{
    public interface IUserFamilyDetailService
    {
        IResult Add(UserFamilyDetail userFamilyDetail);
        IResult Update(UserFamilyDetail userFamilyDetail);
        IResult Delete(long id);
        IDataResult<List<UserFamilyDetail>> GetAll();
        IDataResult<List<UserFamilyDetail>> GetUserFamilyDetailsByUserId(long userId);
    }
}
