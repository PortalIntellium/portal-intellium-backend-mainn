using Business.Repository.CustomerRepository.Constants;
using Business.Repository.CustomerRepository.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.CustomerRepository;
using Entities.Concrete;

namespace Business.Repository.CustomerRepository
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]//validation işlemi
        public IResult Add(Customer customer)
        {
            var customerExist = CustomerExists(customer);
            if (!customerExist.Success)
            {
                return new ErrorResult(customerExist.Message);
            }
            customer.AddetAt = DateTime.Now;
            _customerDal.Add(customer);
            return new SuccessResult(CustomerMessages.AddedCustomer);
        }

        public IResult CustomerExists(Customer customer)
        {
            var result = _customerDal.Get(c => c.TaxIdNumber == customer.TaxIdNumber);
            if (result != null)
            {
                return new ErrorResult(CustomerMessages.CustomerAlreadyExist);
            }
            return new SuccessResult(CustomerMessages.AddedCustomer);
        }

        public IDataResult<Customer> GetById(long id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(p => p.CustomerId == id));
        }

        public IDataResult<UserCustomer> GetCustomer(long customerId)
        {
            return new SuccessDataResult<UserCustomer>(_customerDal.GetCustomer(customerId));
        }

        public IDataResult<List<Customer>> GetList()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(CustomerMessages.UpdatedCustomer);
        }
    }
}
