﻿namespace Entities.Concrete
{
    public class TaskComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
