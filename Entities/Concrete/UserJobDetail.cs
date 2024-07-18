using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserJobDetail
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long AnotherId { get; set; }
        public DateTime StartDate { get; set; }
        public string RecruitmentSource { get; set; } //İşe Alma Kaynağı
        public string WorkingStatus { get; set; } // Çalışma Durumu
        public string Department { get; set; }  // BÖlüm
        public int Level { get; set; } // Seviye
        public DateTime? EndDate { get; set; }
        public string PaymentType { get; set; } // Ücret Türü
        public string ServiceArea { get; set; } // Hizmet Alanı
        public string JobCode { get; set; } // Meslek Adı/Kodu
        public string? Seniority { get; set; } // Kıdem
        public string ReasonForLeaving { get; set; } // Ayrılma Nedeni
        public string Location { get; set; } // Konum
        public string JobTitle { get; set; } // İş Ünvanı
        public long? ManagerId { get; set; } // Yönetici Id



    }
}
