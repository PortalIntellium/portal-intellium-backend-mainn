using Business.Repository.ProjectRepository.Constants;
using Business.Repository.ProjectRepository.Validation;
using Business.Repository.ProjectTeamRepository;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.ProjectRepository;
using Entities.Concrete;
using Entities.DTOs.ProjectDtos;

namespace Business.Repository.ProjectRepository
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectDal _projectDal;
        private readonly IProjectTeamService _projectTeamService;

        public ProjectManager(IProjectDal projectDal, IProjectTeamService projectTeamService)
        {
            _projectDal = projectDal;
            _projectTeamService = projectTeamService;
        }

        [ValidationAspect(typeof(ProjectValidator))]
        public IResult Add(Project project)
        {
            _projectDal.Add(project);
            return new SuccessResult(ProjectMessages.AddedProject);
        }

        public IResult Delete(long id)
        {
            var result = _projectDal.Get(p => p.Id == id);
            if (result == null) { return new ErrorResult("Proje bulunamadı."); }

            _projectTeamService.DeleteAllByProject(id);
            _projectDal.Delete(result);
            return new SuccessResult(ProjectMessages.DeletedProject);
        }

        public IDataResult<List<Project>> GetAll()
        {
            var result = _projectDal.GetAll();
            return new SuccessDataResult<List<Project>>(result);
        }

        public IDataResult<List<GetAllProjectDto>> GetAllByCustomerId(long customerId)
        {
            var result = _projectDal.GetAllByCustomerId(customerId);
            return new SuccessDataResult<List<GetAllProjectDto>>(result, ProjectMessages.ProjectsListed);
        }

        public IDataResult<GetProjectDto> GetById(long id)
        {
            var result = _projectDal.GetById(id);
            return new SuccessDataResult<GetProjectDto>(result, ProjectMessages.ProjectListed);
        }

        public IResult Update(Project project)
        {
            _projectDal.Update(project);
            return new SuccessResult(ProjectMessages.UpdatedProject);
        }
    }
}
