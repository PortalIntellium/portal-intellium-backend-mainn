using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;

namespace DataAccess.Repository.TaskRepository
{
    public interface ITaskCommentDal : IEntityRepository<TaskComment>
    {
        List<TaskCommentDto> GetAllByTaskId(int taskId);
    }
}
