﻿using Entities.DTOs;

namespace Entities.Concrete
{
    public class BoardMember
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public int UserId { get; set; }
    }
}

