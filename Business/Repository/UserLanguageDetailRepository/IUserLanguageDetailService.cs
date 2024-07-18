using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserLanguageDetailRepository
{
    public interface IUserLanguageDetailService
    {
        IResult Add(UserLanguageDetail userLanguageDetail);
        IResult Update(UserLanguageDetail userLanguageDetail);
        IResult Delete(long id);
        IDataResult<List<UserLanguageDetail>> GetAll();
        IDataResult<List<UserLanguageDetail>> GetUserLanguageByUserId(long userId);
    }
}
