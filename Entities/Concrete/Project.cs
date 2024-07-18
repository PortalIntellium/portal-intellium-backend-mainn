﻿namespace Entities.Concrete
{
    public class Project
    {
        public long Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public long ProjectTypeId { get; set; }
        public long UserId { get; set; } // Proje lideri
        public long CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsActive { get; set; }
    }
}
