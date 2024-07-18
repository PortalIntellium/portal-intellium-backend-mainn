namespace Entities.DTOs.TicketDtos
{
    public class EditTicketDto
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public long? AssignedUserId { get; set; }
        public string? RequestType { get; set; }
    }
}
