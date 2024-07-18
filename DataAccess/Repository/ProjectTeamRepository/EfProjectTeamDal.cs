using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs.ProjectDtos;
using Entities.DTOs.ProjectTeamDtos;

namespace DataAccess.Repository.ProjectTeamRepository
{
    public class EfProjectTeamDal : EfEntityRepositoryBase<ProjectTeam, PortalContext>, IProjectTeamDal
    {
        public List<GetAllProjectTeamDto> GetAllByCustomerWithMembers(long customerId)
        {
            using (var context = new PortalContext())
            {
                var result = context.Projects
                                .Where(project => project.CustomerId == customerId)
                                .SelectMany(project => context.ProjectTeams
                                                        .Where(team => team.ProjectId == project.Id)
                                                        .Select(team => new GetAllProjectTeamDto
                                                        {
                                                            Id = team.Id,
                                                            Name = team.Name,
                                                            ProjectName = context.Projects.Where(p => p.Id == team.ProjectId).Single().ProjectName,
                                                            Members = context.ProjectTeamMembers
                                                                        .Where(member => member.ProjectTeamId == team.Id)
                                                                        .Join(context.Users, member => member.UserId, user => user.Id, (member, user) => new ProjectTeamMemberDto
                                                                        {
                                                                            Id = user.Id,
                                                                            Name = user.Name,
                                                                            ImageUrl = user.ImageUrl,
                                                                            IsActive = user.IsActive,
                                                                            BirthDate = user.BirthDate,
                                                                            UserRole = context.RolesForUsers
                                                                                            .Where(ru => ru.UserId.Equals(user.Id))
                                                                                            .Join(context.UserRoles, ru => ru.RoleId, role => role.Id, (ru, role) => new UserRole
                                                                                            {
                                                                                                Id = role.Id,
                                                                                                RoleName = role.RoleName,
                                                                                            }).SingleOrDefault()
                                                                        }).ToList()
                                                        })).ToList();
                return result;
            }
        }


        public List<GetAllProjectTeamDto> GetAllWithMembers()
        {
            using (var context = new PortalContext())
            {
                var result = context.ProjectTeams
                                .Select(team => new GetAllProjectTeamDto
                                {
                                    Id = team.Id,
                                    Name = team.Name,
                                    ProjectName = context.Projects.Where(project => project.Id == team.ProjectId).Single().ProjectName,
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

                                                }).ToList()

                                }).ToList();
                return result;
            }
        }

        public GetProjectTeamDto GetById(long id)
        {
            using (var context = new PortalContext())
            {
                var result = context.ProjectTeams
                    .Where(team => team.Id.Equals(id))
                                .Select(team => new GetProjectTeamDto
                                {
                                    Id = team.Id,
                                    Name = team.Name,
                                    Project = context.Projects.Where(project => project.Id == team.ProjectId).Select(p => new ProjectForProjectTeamDetailDto
                                    {
                                        Id = p.Id,
                                        ProjectName = p.ProjectName,
                                        Description = p.Description,
                                        Leader = context.Users.Where(u => u.Id == p.UserId).Select(leader => new ProjectLeaderDto
                                        {
                                            Id = leader.Id,
                                            Name = leader.Name,
                                            ImageUrl = leader.ImageUrl,
                                            IsActive = leader.IsActive
                                        }).Single(),
                                        StartDate = p.StartDate,
                                        FinishDate = p.FinishDate,
                                        IsActive = p.IsActive


                                    }).Single(),
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

                                                }).ToList()

                                }).SingleOrDefault();
                return result;
            }
        }
    }
}
