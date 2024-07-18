namespace Entities.DTOs.ProjectTeamDtos
{
    public class ProjectTeamForProjectDetailDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public List<ProjectTeamMemberDto> Members { get; set; }
    }
}
