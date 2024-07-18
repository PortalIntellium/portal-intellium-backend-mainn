namespace Entities.DTOs
{
    public class EditUserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string? NewPassword { get; set; }
        public bool IsActive { get; set; }
        public long CustomerId { get; set; }
        public long UserRoleId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
