using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserOperationClaimDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public long CustomerId { get; set; }
        public long OperationClaimId { get; set; }
        public string OperationName { get; set; }
        public string OperationDescription { get; set; }

    }
}
