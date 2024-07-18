using Business.Repository.MailRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.MailRepository;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.MailRepository
{
    public class MailManager : IMailService
    {
        private readonly IMailDal _mailDal;

        public MailManager(IMailDal mailDal)
        {
            _mailDal = mailDal;
        }

        public IResult SendMail(SendMailDto sendMailDto)
        {
            _mailDal.SendMail(sendMailDto);
            return new SuccessResult(MailMessages.MailSendSuccesfull);
        }
    }
}
