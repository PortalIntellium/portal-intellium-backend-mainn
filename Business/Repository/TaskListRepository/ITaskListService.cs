using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;
using Entities.DTOs.TaskListDtos;

namespace Business.Repository.TaskListRepository
{
    public interface ITaskListService
    {
        IResult Add(TaskList taskList);
        IResult Delete(int taskListId);
        IResult DeleteAll(List<TaskList> taskLists);
        IResult Update(TaskList taskList);
        IResult UpdateOrder(TaskListOrderEditDto taskList);
        IDataResult<List<TaskList>> GetAllByBoardId(int boardId);
        IDataResult<List<TaskListDto>> GetAllWithTasks(int boardId);

    }
}
