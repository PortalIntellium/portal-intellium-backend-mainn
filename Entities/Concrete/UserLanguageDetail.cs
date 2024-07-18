using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserLanguageDetail
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FreignLanguage { get; set; } //Yabancı Dil Bilgisi
        public string Read { get; set; }
        public string Write { get; set; }
        public string Speaking { get; set; } 
        public string? DocumentPath { get; set; }

    }
}
