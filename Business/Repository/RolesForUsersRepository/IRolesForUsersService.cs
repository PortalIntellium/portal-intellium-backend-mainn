using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Repository.RolesForUsersRepository
{
    public interface IRolesForUsersService
    {
        IResult Add(RolesForUsers rolesForUsers);
        IResult Update(RolesForUsers rolesForUsers);
        IResult Delete(long id);
        IDataResult<List<RolesForUsers>> GetAll();
        IDataResult<RolesForUsers> GetRolesForUsersByUserId(long userId);
        IDataResult<List<RolesForUsers>> GetRolesForUsersByRoleId(long roleId);
    }
}
