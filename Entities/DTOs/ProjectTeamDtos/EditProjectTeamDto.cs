namespace Entities.DTOs.ProjectTeamDtos
{
    public class EditProjectTeamDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public List<long>? AddUserIds { get; set; }
        public List<long>? RemoveUserIds { get; set; }
    }
}
