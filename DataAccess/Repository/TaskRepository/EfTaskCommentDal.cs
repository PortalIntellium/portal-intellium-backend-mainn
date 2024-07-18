using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.TaskDtos;

namespace DataAccess.Repository.TaskRepository
{
    public class EfTaskCommentDal : EfEntityRepositoryBase<TaskComment, PortalContext>, ITaskCommentDal
    {
        public List<TaskCommentDto> GetAllByTaskId(int taskId)
        {
            using (var context = new PortalContext())
            {
                var result = (from taskComment in context.TaskComments
                              where taskComment.TaskId == taskId
                              join user in context.Users on taskComment.UserId equals user.Id
                              orderby taskComment.CreatedDate descending
                              select new TaskCommentDto
                              {
                                  Id = taskComment.Id,
                                  User = new UserDto
                                  {
                                      Id = user.Id,
                                      Name = user.Name,
                                      Email = user.Email,
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
                                  },
                                  Content = taskComment.Content,
                                  CreatedDate = taskComment.CreatedDate,
                              }).ToList();
                return result;
            }
        }
    }
}
