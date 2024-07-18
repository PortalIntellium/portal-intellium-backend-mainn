using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Repository.CustomerRepository
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IDataResult<List<Customer>> GetList();
        IDataResult<Customer> GetById(long id);
        IDataResult<UserCustomer> GetCustomer(long customerId);
        IResult CustomerExists(Customer customer);
        //IResult UserCustomerAdd(long customerId, long userId);
    }
}
