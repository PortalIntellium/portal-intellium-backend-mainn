using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.UserOperationClaimRepository
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, PortalContext>, IUserOperationClaimDal
    {
        public List<UserOperationClaimDto> GetListDto(long userId, long customerId)
        {
            using (var context=new PortalContext())
            {
                var result = from userOperationClaim in context.UserOperationClaims.Where(x => x.UserId == userId && x.CustomerId == customerId)
                             join operationClaim in context.OperationClaims
                             on userOperationClaim.OperationClaimId equals operationClaim.Id
                             select new UserOperationClaimDto
                             {
                                 UserId = userId,
                                 Id=operationClaim.Id,
                                 CustomerId = customerId,
                                 OperationClaimId=operationClaim.Id,
                                 OperationDescription = operationClaim.Description,
                                 OperationName = operationClaim.Name
                             };
                return result.ToList();
            }
        }
    }
}
