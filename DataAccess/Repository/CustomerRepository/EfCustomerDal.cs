using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Repository.CustomerRepository
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, PortalContext>, ICustomerDal
    {
        /*public void UserCustomerAdd(long customerId, long userId)
        {
            using (var context = new PortalContext())
            {
                UserCustomer userCustomer = new UserCustomer()
                {
                    CustomerId = customerId,
                    UserId=userId,
                    AddetAt = DateTime.Now,
                    IsActive = true,
                };
                context.UserCustomers.Add(userCustomer);
                context.SaveChanges();
            }
        }*/

        public UserCustomer GetCustomer(long customerId)
        {
            using (var context = new PortalContext())
            {
                var result = context.UserCustomers.Where(p => p.CustomerId == customerId).FirstOrDefault();
                return result;
            }
        }
    }
}
