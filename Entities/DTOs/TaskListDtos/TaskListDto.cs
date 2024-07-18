using Entities.DTOs.TaskDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.TaskListDtos
{
    public class TaskListDto
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public List<TaskForTaskListDto> Tasks { get; set; }
    }
}
