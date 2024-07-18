namespace Entities.DTOs.ProjectTeamDtos
{
    public class AddProjectTeamDto
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public List<long>? AddUserIds { get; set; }
    }
}
