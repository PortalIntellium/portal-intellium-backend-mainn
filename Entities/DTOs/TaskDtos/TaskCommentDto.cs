using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.TaskDtos
{
    public class TaskCommentDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
