using Business.Helpers.DebitHelpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.DebitRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.DebitRepository
{
    public class DebitManager : IDebitService
    {
        private readonly IDebitDal _debitDal;
        private readonly IUserDal _userDal;
        private readonly GetDebitPDFPath _getDebitPDFPath;

        public DebitManager(IDebitDal debitDal, IUserDal userDal, GetDebitPDFPath getDebitPDFPath)
        {
            _debitDal = debitDal;
            _userDal = userDal;
            _getDebitPDFPath = getDebitPDFPath;
        }

        public IResult Add(Debit debit)
        {
            var user = _userDal.Get(x => x.Id == debit.ReceiverUserId);

            string pdfPath = _getDebitPDFPath.GetPdfPath(debit, user);
            debit.PdfPath = pdfPath;

            _debitDal.Add(debit);
            return new SuccessResult(DebitMessages.AddedDebit);
        }

        public IDataResult<List<Debit>> GetAll()
        {
            return new SuccessDataResult<List<Debit>>(_debitDal.GetAll());
        }


        
    }
}
