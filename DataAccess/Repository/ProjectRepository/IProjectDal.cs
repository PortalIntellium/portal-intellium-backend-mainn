using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ProjectDtos;

namespace DataAccess.Repository.ProjectRepository
{
    public interface IProjectDal : IEntityRepository<Project>
    {
        List<GetAllProjectDto> GetAllByCustomerId(long customerId);
        GetProjectDto GetById(long projectId);
    }
}
