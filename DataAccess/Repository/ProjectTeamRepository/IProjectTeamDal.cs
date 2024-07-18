using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ProjectTeamDtos;

namespace DataAccess.Repository.ProjectTeamRepository
{
    public interface IProjectTeamDal : IEntityRepository<ProjectTeam>
    {
        List<GetAllProjectTeamDto> GetAllWithMembers();
        List<GetAllProjectTeamDto> GetAllByCustomerWithMembers(long customerId);
        GetProjectTeamDto GetById(long id);
    }
}
