using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Repository.ProjectTeamMemberRepository
{
    public interface IProjectTeamMemberService
    {
        IResult Add(List<long> userIds, long projectTeamId);
        IResult DeleteMembers(List<long> userIds, long projectTeamId);
        IResult DeleteAllByProjectTeam(long projectTeamId);
        IDataResult<List<ProjectTeamMember>> GetAllByProjectTeam(long projectTeamId);

    }
}
