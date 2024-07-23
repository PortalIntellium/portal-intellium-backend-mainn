using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ExpenseDto.ExpenseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.ExpenseRepository
{
    public interface IExpenseDal:IEntityRepository<Expense>
    {
        List<Expense> GetAllByUserId(long userId);
        Expense GetById(int id);
        void AddExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(int id);

        List<Expense> GetAllInvoices();

      

    }
}
