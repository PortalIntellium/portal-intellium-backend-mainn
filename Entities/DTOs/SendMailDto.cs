using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SendMailDto
    {
        
        public  MailParameters  mailParameters { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

    }
}
