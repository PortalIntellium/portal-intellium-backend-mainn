using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserCertificateDetailRepository
{
    public interface IUserCertificateDetailService
    {
        IResult Add(UserCertificateDetail userCertificateDetail);
        IResult Update(UserCertificateDetail userCertificateDetail);
        IResult Delete(long  id);
        IDataResult<List<UserCertificateDetail>> GetAll();
        IDataResult<List<UserCertificateDetail>> GetUserCertificateDetailByUserId(long userId);
    }
}
