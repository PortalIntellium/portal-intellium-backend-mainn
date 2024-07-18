using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;

namespace Business.Repository.TaskRepository
{
    public interface ITaskCommentService
    {
        IResult Add(TaskComment taskComment);

        IResult Delete(int taskCommentId);

        IResult DeleteAll(List<TaskCommentDto> taskComments);

        IDataResult<List<TaskCommentDto>> GetAllByTaskId(int taskId);

    }
}
