using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class HealthInfo
    {
        public long Id { get; set; }
        public string InsuranceScope { get; set; } // sigorta kapsamı
        public string InsuranceCompanyName { get; set; } // sigorta şirketi ismi
        public DateTime InsuranceBeginDate { get; set; } // sigorta başlangıç tarihi
        public DateTime InsuranceEndDate { get; set; } // // sigorta bitiş tarihi
        public DateTime AddedAt { get; set; }
        public Boolean IsActive { get; set; }

        // düzenlenecek
    }  
}
