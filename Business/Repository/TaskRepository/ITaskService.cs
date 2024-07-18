using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;

namespace Business.Repository.TaskRepository
{
    public interface ITaskService
    {
        IResult Add(AddTaskDto taskDto);
        IResult Delete(int taskId);
        IResult DeleteAll(List<Entities.Concrete.Task> tasks);
        IResult Update(EditTaskDto taskDto);
        IResult UpdateOrder(TaskOrderEditDto taskOrderEditDto);
        IDataResult<TaskViewDto> GetById(int taskId);
        IDataResult<List<Entities.Concrete.Task>> GetAllByTaskListId(int taskListId);
    }
}
