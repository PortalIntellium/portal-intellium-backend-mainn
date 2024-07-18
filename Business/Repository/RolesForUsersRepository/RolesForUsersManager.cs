using Business.Repository.RolesForUsersRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.RolesForUsersRepository;
using Entities.Concrete;

namespace Business.Repository.RolesForUsersRepository
{
    public class RolesForUsersManager : IRolesForUsersService
    {

        private readonly IRolesForUsersDal _rolesForUsersDal;

        public RolesForUsersManager(IRolesForUsersDal rolesForUsersDal)
        {
            _rolesForUsersDal = rolesForUsersDal;
        }

        public IResult Add(RolesForUsers rolesForUsers)
        {
            _rolesForUsersDal.Add(rolesForUsers);
            return new SuccessResult(RolesForUsersMessages.AddedRolesForUsers);

        }

        public IResult Delete(long id)
        {
            var result = _rolesForUsersDal.Get(x => x.Id == id);
            _rolesForUsersDal.Delete(result);
            return new SuccessResult(RolesForUsersMessages.DeletedRolesForUsers);

        }

        public IDataResult<List<RolesForUsers>> GetAll()
        {
            return new SuccessDataResult<List<RolesForUsers>>(_rolesForUsersDal.GetAll());
        }

        public IDataResult<List<RolesForUsers>> GetRolesForUsersByRoleId(long roleId)
        {
            var result = _rolesForUsersDal.GetAll(ru => ru.RoleId.Equals(roleId));
            if (result == null)
            {
                return new ErrorDataResult<List<RolesForUsers>>("RolesForUsers id not found");
            }
            return new SuccessDataResult<List<RolesForUsers>>(result);

        }

        public IDataResult<RolesForUsers> GetRolesForUsersByUserId(long userId)
        {
            var result = _rolesForUsersDal.Get(ru => ru.UserId.Equals(userId));
            if (result == null)
            {
                return new ErrorDataResult<RolesForUsers>("RolesForUsers id not found");
            }
            return new SuccessDataResult<RolesForUsers>(result);

        }

        public IResult Update(RolesForUsers rolesForUsers)
        {
            _rolesForUsersDal.Update(rolesForUsers);
            return new SuccessResult(RolesForUsersMessages.UpdatedRolesForUsers);

        }
    }
}
