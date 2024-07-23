using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.ExpenseRepository;
using DataAccess.Repository.UserCustomerRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using Entities.DTOs.ExpenseDto.ExpenseDto;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Business.Repository.ExpenseRepository
{
    public class ExpenseManager : IExpenseService
    {
        private readonly IExpenseDal _expenseDal;
        private readonly IUserDal _userDal;
        private readonly IUserCustomerDal _userCustomerDal;

        public ExpenseManager(IExpenseDal expenseDal, IUserDal userDal,
            IUserCustomerDal userCustomerDal
            )
        {
            _expenseDal = expenseDal;
            _userDal = userDal;
            _userCustomerDal = userCustomerDal;
        }

        public IResult Add(ExpenseDto expenseDto)
        {
            // Validate and remove data:image/png;base64, prefix if present
            string base64Data = Regex.Match(expenseDto.ImageData, @"data:image\/png;base64,(.+)").Groups[1].Value;

            try
            {
                // Convert Base64 string to byte array
                byte[] imageDataBytes = Convert.FromBase64String(base64Data);

                var user = _userDal.Get(u => u.Id == expenseDto.UserId);
                if (user == null)
                {
                    return new ErrorResult("User not found");
                }

                var userCustomer = _userCustomerDal.Get(uc => uc.UserId == expenseDto.UserId && uc.IsActive);
                if (userCustomer == null)
                {
                    return new ErrorResult("UserCustomer relationship not found");
                }

                var expense = new Expense
                {
                    UserId = userCustomer.UserId,
                    CustomerId = userCustomer.CustomerId,
                    ProjectName = expenseDto.ProjectName,
                    Description = expenseDto.Description,
                    ExpenseType = expenseDto.ExpenseType,
                    ExcludingVatAmount = expenseDto.ExcludingVatAmount,
                    VatRate = expenseDto.VatRate,
                    Vat = expenseDto.Vat,
                    TotalAmount = expenseDto.TotalAmount,
                    InvoiceDate = expenseDto.InvoiceDate,
                    InvoiceNumber = expenseDto.InvoiceNumber,
                    InvoiceTitle = expenseDto.InvoiceTitle,
                    ImageData = imageDataBytes,
                    IsActive = true,
                    
                };

                _expenseDal.AddExpense(expense);
                return new SuccessResult("Expense added successfully");
            }
            catch (FormatException)
            {
                return new ErrorResult("Invalid Base64 format for imageData");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Error adding expense: {ex.Message}");
            }
        }

         public IResult Update(ExpenseDto expenseDto)
        {
            var expense = _expenseDal.GetById(expenseDto.Id);

            if (expense.UserId != expenseDto.UserId)
            {
                return new ErrorResult("You are not authorized to update this expense");
            }


            var user = _userDal.Get(u => u.Id == expenseDto.UserId);
            if (user == null)
            {
                return new ErrorResult("User not found");
            }



            if (expense == null)
            {
                return new ErrorResult("Expense not found");
            }

            expense.CustomerId = expenseDto.CustomerId;
            expense.ProjectName = expenseDto.ProjectName;
            expense.Description = expenseDto.Description;
            expense.ExpenseType = expenseDto.ExpenseType;
            expense.ExcludingVatAmount = expenseDto.ExcludingVatAmount;
            expense.VatRate = expenseDto.VatRate;
            expense.Vat = expenseDto.Vat;
            expense.TotalAmount = expenseDto.TotalAmount;
            expense.InvoiceDate = expenseDto.InvoiceDate;
            expense.InvoiceNumber = expenseDto.InvoiceNumber;
            expense.InvoiceTitle = expenseDto.InvoiceTitle;
            expense.IsConfirmation = expenseDto.IsConfirmation; 

            // SendConfirmation özelliğini güncelleme kısmı
            if (expenseDto.IsConfirmation != expense.IsConfirmation)
            {
                expense.IsConfirmation = expenseDto.IsConfirmation;
            }

            _expenseDal.UpdateExpense(expense);
            return new SuccessResult("Expense updated successfully");
        }

        public IResult Delete(int id)
        {
            var expense = _expenseDal.GetById(id);
            if (expense == null)
            {
                return new ErrorResult("Expense not found");
            }

            _expenseDal.DeleteExpense(id);
            return new SuccessResult("Expense deleted successfully");
        }

        public IDataResult<List<Expense>> GetAll(long userId)
        {
            var expenses = _expenseDal.GetAllByUserId(userId);
            return new SuccessDataResult<List<Expense>>(expenses, "Expenses retrieved successfully");
        }

        public IDataResult<Expense> GetById(int id)
        {
            var expense = _expenseDal.GetById(id);
            if (expense == null)
            {
                return new ErrorDataResult<Expense>("Expense not found");
            }
            return new SuccessDataResult<Expense>(expense, "Expense retrieved successfully");
        }

        public IDataResult<List<Expense>> GetAllInvoices()
        {
            var expenses = _expenseDal.GetAll();
            return new SuccessDataResult<List<Expense>>(expenses, "Invoices retrieved successfully");
        }
    }
}
