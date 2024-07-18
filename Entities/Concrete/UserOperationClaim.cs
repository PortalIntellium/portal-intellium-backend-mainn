using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserOperationClaim
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public long OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }


    }
}
