using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OperationClaim
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }//ticket.add
        public string Description { get; set; }//kullancı ekleyebilir
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }=true;

    }
}
