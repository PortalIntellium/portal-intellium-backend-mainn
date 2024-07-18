using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserRoleRepository
{
    public interface IUserRoleService
    {
        IResult Add(UserRole userRole);
        IResult Update(UserRole userRole);
        IResult Delete(long id);
        IDataResult<List<UserRole>> GetAll();
    }
}
