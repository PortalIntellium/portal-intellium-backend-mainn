using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.ExpenseDto.ExpenseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.ExpenseRepository
{
    public interface IExpenseService
    {
        IResult Add(ExpenseDto expenseDto);
        IResult Update(ExpenseDto expenseDto);
        IResult Delete(int id);
        IDataResult<List<Expense>> GetAll( long userId);
        IDataResult<Expense> GetById(int id);

        IDataResult<List<Expense>> GetAllInvoices();

      

    }
}
