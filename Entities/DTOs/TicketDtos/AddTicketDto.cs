﻿using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.TicketDtos
{
    public class AddTicketDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public long CustomerId { get; set; }
        public long CreatorUserId { get; set; }
        //public List<IFormFile>? Attachments { get; set; }

    }
}
