using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.ProjectDtos;
using Entities.DTOs.ProjectTeamDtos;

namespace DataAccess.Repository.ProjectRepository
{
    public class EfProjectDal : EfEntityRepositoryBase<Project, PortalContext>, IProjectDal
    {
        public List<GetAllProjectDto> GetAllByCustomerId(long customerId)
        {
            using var context = new PortalContext();
            var result = context.Projects.Where(p => p.CustomerId.Equals(customerId)).Select(project => new GetAllProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                ProjectType = context.ProjectTypes.Where(type => type.Id.Equals(project.ProjectTypeId)).Single(),
                ProjectTeams = context.ProjectTeams.Where(team => team.ProjectId.Equals(project.Id)).ToList(),
                IsActive = project.IsActive,
                FinishDate = project.FinishDate,
                StartDate = project.StartDate,
            }).ToList();
            return result;
        }

        public GetProjectDto GetById(long projectId)
        {
            using var context = new PortalContext();
            var result = context.Projects.Where(p => p.Id.Equals(projectId)).Select(project => new GetProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                ProjectType = context.ProjectTypes.Where(type => type.Id.Equals(project.ProjectTypeId)).Single(),
                ProjectTeams = context.ProjectTeams.Where(t => t.ProjectId.Equals(projectId)).Select(team => new ProjectTeamForProjectDetailDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    ProjectId = project.Id,
                    Members = context.ProjectTeamMembers
                                                .Where(member => member.ProjectTeamId == team.Id)
                                                .Join(context.Users, member => member.UserId, user => user.Id, (member, user) => new ProjectTeamMemberDto
                                                {
                                                    Id = user.Id,
                                                    Name = user.Name,
                                                    ImageUrl = user.ImageUrl,
                                                    IsActive = user.IsActive,
                                                    BirthDate = user.BirthDate,
                                                    UserRole = context.RolesForUsers.Where(ru => ru.UserId.Equals(user.Id))
                                                     .Join(context.UserRoles, ru => ru.RoleId, role => role.Id, (ru, role) => new UserRole
                                                     {
                                                         Id = role.Id,
                                                         RoleName = role.RoleName,
                                                     }).SingleOrDefault()

                                                }).ToList(),
                   
                }).ToList(),
                ProjectLeader = context.Users.Where(u => u.Id.Equals(project.UserId)).Select(user => new ProjectMemberDto
                {
                    Id= user.Id,
                    Name = user.Name,
                    ImageUrl= user.ImageUrl,
                    IsActive = user.IsActive,
                }).Single(),
                Customer = context.Customers.Where(c => c.CustomerId.Equals(project.CustomerId)).Select(customer => new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                }).Single(),
                IsActive = project.IsActive,
                StartDate = project.StartDate,
                FinishDate = project.FinishDate

            }).SingleOrDefault();

            return result;
        }
    }
}
