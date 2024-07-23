using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs.ExpenseDto.ExpenseDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository.ExpenseRepository
{
    public class EfExpenseDal : EfEntityRepositoryBase<Expense, PortalContext>, IExpenseDal
    {
        public List<Expense> GetAllByUserId(long userId)
        {
            using (var context = new PortalContext())
            {
                return context.Expenses.Where(e => e.UserId == userId && e.IsActive).ToList();
            }
        }

        public Expense GetById(int id)
        {
            using (var context = new PortalContext())
            {
                return context.Expenses.FirstOrDefault(e => e.Id == id && e.IsActive);
            }
        }

        public void AddExpense(Expense expense)
        {
            using (var context = new PortalContext())
            {
                context.Expenses.Add(expense);
                context.SaveChanges();
            }
        }


        public void UpdateExpense(Expense expense)
        {
            using (var context = new PortalContext())
            {
                context.Expenses.Update(expense);
                context.SaveChanges();
            }
        }

        public void DeleteExpense(int id)
        {
            using (var context = new PortalContext())
            {
                var expense = context.Expenses.FirstOrDefault(e => e.Id == id);
                if (expense != null)
                {
                    expense.IsActive = false;
                    context.SaveChanges();
                }
            }
        }

        public List<Expense> GetAllInvoices()
        {
            using(var context = new PortalContext())

            {
                return context.Expenses.ToList();
            }
        }

        
    }
}
