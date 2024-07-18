using Entities.Concrete;

namespace Entities.DTOs.BoardDtos
{
    public class BoardDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public BoardCategory Category { get; set; }
        public UserDto CreatedUser { get; set; }
        public List<BoardMemberDto> BoardMembers { get; set; }
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        public bool PrivateToProjectMembers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
