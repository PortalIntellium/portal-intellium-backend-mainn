using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Repository.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, PortalContext>, IUserDal
    {
        public List<UserDto> GetAllForUserList()
        {
            using (var context = new PortalContext())
            {
                var result = from user in context.Users
                             select new UserDto
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Email = user.Email,
                                 Language = user.Language,
                                 IsActive = user.IsActive,
                                 BirthDate = user.BirthDate,
                                 AddetAt = user.AddetAt,
                                 ImageUrl = user.ImageUrl,
                                 Customer = context.UserCustomers.Where(uc => uc.UserId.Equals(user.Id))
                                 .Join(context.Customers, uc => uc.CustomerId, customer => customer.CustomerId, (uc, customer) => new CustomerDto
                                 {
                                     CustomerId = customer.CustomerId,
                                     CustomerName = customer.CustomerName,
                                 }).SingleOrDefault(),
                                 UserRole = context.RolesForUsers.Where(ru => ru.UserId.Equals(user.Id))
                                 .Join(context.UserRoles, ru => ru.RoleId, role => role.Id, (ru, role) => new UserRole
                                 {
                                     Id = role.Id,
                                     RoleName = role.RoleName,
                                 }).SingleOrDefault(),
                             };
                return result.OrderBy(user => user.Id).ToList();
            }
        }

        public UserDto GetUserAsDtoById(long id)
        {
            using (var context = new PortalContext())
            {
                var result = from user in context.Users
                             where user.Id == id
                             select new UserDto
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Email = user.Email,
                                 Language = user.Language,
                                 IsActive = user.IsActive,
                                 BirthDate = user.BirthDate,
                                 AddetAt = user.AddetAt,
                                 ImageUrl = user.ImageUrl,
                                 Customer = context.UserCustomers.Where(uc => uc.UserId.Equals(user.Id))
                                 .Join(context.Customers, uc => uc.CustomerId, customer => customer.CustomerId, (uc, customer) => new CustomerDto
                                 {
                                     CustomerId = customer.CustomerId,
                                     CustomerName = customer.CustomerName,
                                 }).SingleOrDefault(),
                                 UserRole = context.RolesForUsers.Where(ru => ru.UserId.Equals(user.Id))
                                 .Join(context.UserRoles, ru => ru.RoleId, role => role.Id, (ru, role) => new UserRole
                                 {
                                     Id = role.Id,
                                     RoleName = role.RoleName,
                                 }).SingleOrDefault(),
                             };
                return result.SingleOrDefault();
            }
        }

        public List<UserDto> GetByName(string name)
        {
            using (var context = new PortalContext())
            {
                var result = from user in context.Users
                             where user.Name.ToLower().Contains(name.ToLower())
                             select new UserDto
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Email = user.Email,
                                 ImageUrl = user.ImageUrl

                             };
                return result.ToList();
            }
        }

        public List<OperationClaim> GetClaims(User user, long customerId)
        {
            using (var context = new PortalContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == operationClaim.Id && userOperationClaim.CustomerId == customerId
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name

                             };
                return result.OrderBy(p => p.Name).ToList();
            }

        }
    }
}
