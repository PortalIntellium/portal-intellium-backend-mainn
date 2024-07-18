using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserJobDetailsRepository
{
    public interface IUserJobDetailService
    {
        IResult Add(UserJobDetail userJobDetails);
        IResult Update(UserJobDetail userJobDetails);
        IResult Delete(long id);
        IDataResult<List<UserJobDetail>> GetAll();
        IDataResult<UserJobDetail> GetByUserId(long userId);
    }
}
