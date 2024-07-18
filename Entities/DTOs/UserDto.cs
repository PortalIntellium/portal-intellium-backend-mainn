using Entities.Concrete;

namespace Entities.DTOs
{
    public class UserDto : BaseUserDto
    {
        public string Email { get; set; }
        public string Language { get; set; }
        public CustomerDto? Customer { get; set; }
        public UserRole? UserRole { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime AddetAt { get; set; }
    }
}
