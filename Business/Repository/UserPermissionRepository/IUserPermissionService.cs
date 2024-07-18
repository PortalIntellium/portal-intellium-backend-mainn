using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserPermissionRepository
{
    public interface IUserPermissionService
    {
        IResult Add(UserPermission userPermission);
        IResult Update(UserPermission userPermission);
        IDataResult<List<UserPermission>> GetAll();
        IResult Delete(UserPermission userPermission);
        IResult GetUserPermissionById(int id);

    }
}
