using Entities.DTOs.ProjectDtos;

namespace Entities.DTOs.TicketDtos
{
    public class GetTicketDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public ProjectForTicketDto Project { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public TicketUserDto CreatorUser { get; set; }
        public TicketUserDto? AssignedUser { get; set; }
        public string? RequestType { get; set; }
    }
}
