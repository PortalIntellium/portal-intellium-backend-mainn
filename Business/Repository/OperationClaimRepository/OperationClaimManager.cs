using Business.BusinessAspects;
using Business.Repository.OperationClaimRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.OperationClaimRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.OperationClaimRepository
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        //[SecuredOperation("Admin,OperationClaim.Add")]
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);
        }
        //[SecuredOperation("Admin,OperationClaim.Delete")]

        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }
        //[SecuredOperation("Admin,OperationClaim.GetAll")]

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll(),OperationClaimMessages.Listed);
        }
        //[SecuredOperation("Admin,OperationClaim.Get")]

        public IDataResult<OperationClaim> GetById(long id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(x=>x.Id==id));
        }
        //[SecuredOperation("Admin,OperationClaim.Update")]

        public IResult Update(OperationClaim operationClaim)
        {
           _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }
    }
}
