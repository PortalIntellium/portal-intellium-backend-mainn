using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserEducationDetailRepository
{
    public interface IUserEducationDetailService
    {
        IResult Add(UserEducationDetail userEducationDetails);
        IResult Update(UserEducationDetail userEducationDetails);
        IResult Delete(long id);
        IDataResult<List<UserEducationDetail>> GetAll();
        IDataResult<List<UserEducationDetail>> GetUserEducationDetailsByUserId(long userId);
    }
}
