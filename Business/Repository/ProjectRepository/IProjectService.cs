using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProjectDtos;

namespace Business.Repository.ProjectRepository
{
    public interface IProjectService
    {
        IResult Add(Project project);
        IResult Update(Project project);
        IResult Delete(long id);
        IDataResult<List<GetAllProjectDto>> GetAllByCustomerId(long customerId);
        IDataResult<List<Project>> GetAll();
        IDataResult<GetProjectDto> GetById(long id);

    }
}
