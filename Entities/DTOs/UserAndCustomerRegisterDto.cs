using Entities.Concrete;

namespace Entities.DTOs
{
    public class UserAndCustomerRegisterDto
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public Customer? Customer { get; set; }
    }
}
