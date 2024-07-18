﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Task
    {
        public int Id { get; set; }
        public int TaskListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set;}
    }
}
