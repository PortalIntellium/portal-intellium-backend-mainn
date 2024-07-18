using Core.Utilities.Results.Abstract;
using Entities.DTOs.ProjectTeamDtos;

namespace Business.Repository.ProjectTeamRepository
{
    public interface IProjectTeamService
    {
        IResult Add(AddProjectTeamDto addProjectTeam);
        IDataResult<List<GetAllProjectTeamDto>> GetAll();
        IDataResult<List<GetAllProjectTeamDto>> GetAllByCustomer(long customerId);
        IDataResult<GetProjectTeamDto> GetById(long id);
        IResult Delete(long teamId);
        IResult DeleteAllByProject(long projectId);
        IResult Update(EditProjectTeamDto editProjectTeam);
    }
}
