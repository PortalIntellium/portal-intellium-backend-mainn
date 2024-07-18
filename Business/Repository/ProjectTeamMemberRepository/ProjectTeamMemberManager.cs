using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.ProjectTeamMemberRepository;
using Entities.Concrete;

namespace Business.Repository.ProjectTeamMemberRepository
{
    public class ProjectTeamMemberManager : IProjectTeamMemberService
    {
        private readonly IProjectTeamMemberDal _projectTeamMemberDal;

        public ProjectTeamMemberManager(IProjectTeamMemberDal projectTeamMemberDal)
        {
            _projectTeamMemberDal = projectTeamMemberDal;
        }

        public IResult Add(List<long> userIds, long projectTeamId)
        {
            foreach (long userId in userIds)
            {
                var result = _projectTeamMemberDal.Get(member => member.UserId.Equals(userId) && member.ProjectTeamId.Equals(projectTeamId));
                if (result == null)
                {
                    ProjectTeamMember teamMember = new()
                    {
                        UserId = userId,
                        ProjectTeamId = projectTeamId
                    };
                    _projectTeamMemberDal.Add(teamMember);
                }
            }
            return new SuccessResult();
        }

        public IResult DeleteAllByProjectTeam(long projectTeamId)
        {
            var members = _projectTeamMemberDal.GetAll(m => m.ProjectTeamId.Equals(projectTeamId));
            if (members == null) return new ErrorResult();
            foreach (var member in members)
            {
                _projectTeamMemberDal.Delete(member);
            }
            return new SuccessResult();
        }

        public IResult DeleteMembers(List<long> userIds, long projectTeamId)
        {
            foreach(long userId in userIds)
            {
                var result = _projectTeamMemberDal.Get(member => member.UserId.Equals(userId) && member.ProjectTeamId.Equals(projectTeamId));
                if (result == null) continue;
                _projectTeamMemberDal.Delete(result);
            }
            return new SuccessResult();
        }

        public IDataResult<List<ProjectTeamMember>> GetAllByProjectTeam(long projectTeamId)
        {
            var result = _projectTeamMemberDal.GetAll(member => member.ProjectTeamId.Equals(projectTeamId));
            return new SuccessDataResult<List<ProjectTeamMember>>(result);
        }
    }
}
