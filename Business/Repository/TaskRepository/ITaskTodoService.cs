using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Repository.TaskRepository
{
    public interface ITaskTodoService
    {
        IResult Add(TaskTodo taskTodo);
        IResult Delete(int id);

        IResult Update(TaskTodo taskTodo);
        IResult Change(int id, Boolean state);

        IResult DeleteAll(List<TaskTodo> taskTodos);

        IDataResult<List<TaskTodo>> GetAllByTodoListId(int todoListId);
    }
}
