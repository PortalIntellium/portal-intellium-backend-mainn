using Entities.Concrete;

namespace Entities.DTOs.ProjectTeamDtos
{
    public class ProjectTeamMemberDto : BaseUserDto
    {
        public UserRole? UserRole { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
