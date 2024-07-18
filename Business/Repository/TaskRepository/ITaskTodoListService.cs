using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;

namespace Business.Repository.TaskRepository
{
    public interface ITaskTodoListService
    {
        IResult Add(TaskTodoList taskTodoList);
        IResult Delete(int id);
        IResult DeleteAll(List<TaskTodoList> taskTodoLists);
        IResult Update(TaskTodoList taskTodoList);
        IDataResult<List<TaskTodoList>> GetAllByTaskId(int taskId);
        IDataResult<List<TaskTodoListDto>> GetAllWithTodoByTaskId(int taskId);
    }
}
