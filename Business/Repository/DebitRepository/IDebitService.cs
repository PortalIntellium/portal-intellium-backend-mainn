using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.DebitRepository
{
    public interface IDebitService
    {
        IResult Add(Debit debit);
        IDataResult<List<Debit>> GetAll();
    }
}
