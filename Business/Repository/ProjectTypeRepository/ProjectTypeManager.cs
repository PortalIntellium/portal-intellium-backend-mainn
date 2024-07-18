using Business.Repository.ProjectTypeRepository.Constants;
using Business.Repository.ProjectTypeRepository.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.ProjectTypeRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.ProjectTypeRepository
{
    public class ProjectTypeManager : IProjectTypeService
    {
        private readonly IProjectTypeDal _projectTypeDal;

        public ProjectTypeManager(IProjectTypeDal projectTypeDal)
        {
            _projectTypeDal = projectTypeDal;
        }

        [ValidationAspect(typeof(ProjectTypeValidator))]
        public IResult Add(ProjectType projectType)
        {
            _projectTypeDal.Add(projectType);
            return new SuccessResult(ProjectTypeMessages.AddedProjectType);
        }

        public IResult Delete(long id)
        {
            var result=_projectTypeDal.Get(p=>p.Id== id);
            _projectTypeDal.Delete(result);
            return new SuccessResult(ProjectTypeMessages.DeletedProjectType);
        }

        public IDataResult<List<ProjectType>> GetAll()
        {
            return new SuccessDataResult<List<ProjectType>>(_projectTypeDal.GetAll());
        }

        public IDataResult<ProjectType> GetById(long id)
        {
            return new SuccessDataResult<ProjectType>(_projectTypeDal.Get(p=>p.Id == id));
        }

        public IResult Update(ProjectType projectType)
        {
            _projectTypeDal.Update(projectType);
            return new SuccessResult(ProjectTypeMessages.UpdatedProjectType);
        }
    }
}
