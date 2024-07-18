using Business.Repository.ProjectTeamMemberRepository;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.ProjectTeamRepository;
using Entities.Concrete;
using Entities.DTOs.ProjectTeamDtos;

namespace Business.Repository.ProjectTeamRepository
{
    public class ProjectTeamManager : IProjectTeamService
    {
        private readonly IProjectTeamDal _projectTeamDal;
        private readonly IProjectTeamMemberService _projectTeamMemberService;
        public ProjectTeamManager(IProjectTeamDal projectTeamDal, IProjectTeamMemberService projectTeamMemberService)
        {
            _projectTeamDal = projectTeamDal;
            _projectTeamMemberService = projectTeamMemberService;
        }

        public IResult Add(AddProjectTeamDto addProjectTeam)
        {
            ProjectTeam projectTeam = new()
            {
                Name = addProjectTeam.Name,
                ProjectId = addProjectTeam.ProjectId,
            };

            _projectTeamDal.Add(projectTeam);

            if (addProjectTeam.AddUserIds != null) _projectTeamMemberService.Add(addProjectTeam.AddUserIds, projectTeam.Id);

            return new SuccessResult("Proje takımı eklendi.");

        }

        public IResult Delete(long teamId)
        {
            var result = _projectTeamDal.Get(team => team.Id.Equals(teamId));

            if (result == null)
            {
                return new ErrorResult("Proje takımı bulunamadı.");
            }
            _projectTeamMemberService.DeleteAllByProjectTeam(teamId);
            _projectTeamDal.Delete(result);
            return new SuccessResult("Proje takımı silindi.");
        }

        public IResult DeleteAllByProject(long projectId)
        {
            var result = _projectTeamDal.GetAll(pt => pt.ProjectId.Equals(projectId));
            if (result == null)
            {
                return new ErrorResult();
            }

            foreach (var projectTeam in result)
            {
                _projectTeamMemberService.DeleteAllByProjectTeam(projectTeam.Id);
                _projectTeamDal.Delete(projectTeam);
            }
            return new SuccessResult();

        }

        public IDataResult<List<GetAllProjectTeamDto>> GetAll()
        {
            var result = _projectTeamDal.GetAllWithMembers();
            return new SuccessDataResult<List<GetAllProjectTeamDto>>(result);
        }

        public IDataResult<List<GetAllProjectTeamDto>> GetAllByCustomer(long customerId)
        {
            var result = _projectTeamDal.GetAllByCustomerWithMembers(customerId);
            return new SuccessDataResult<List<GetAllProjectTeamDto>>(result);
        }

        public IDataResult<GetProjectTeamDto> GetById(long id)
        {
            var result = _projectTeamDal.GetById(id);
            return new SuccessDataResult<GetProjectTeamDto>(result);
        }

        public IResult Update(EditProjectTeamDto editProjectTeam)
        {
            var result = _projectTeamDal.Get(p => p.Id.Equals(editProjectTeam.Id));
            if (result == null) { return new ErrorResult("Proje takımı bulunamadı."); }

            result.Name = editProjectTeam.Name;
            result.ProjectId = editProjectTeam.ProjectId;

            if (editProjectTeam.AddUserIds != null)
            {
                _projectTeamMemberService.Add(editProjectTeam.AddUserIds, result.Id);
            }

            if (editProjectTeam.RemoveUserIds != null)
            {
                _projectTeamMemberService.DeleteMembers(editProjectTeam.RemoveUserIds, result.Id);
            }

            _projectTeamDal.Update(result);

            return new SuccessResult("Proje takımı güncellendi.");
        }
    }
}
