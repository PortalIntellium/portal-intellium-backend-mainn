using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Repository.TaskRepository
{
    public interface ITaskLabelService
    {
        IResult Add(List<int> labelIds, int taskId);

        IResult DeleteByLabelIdAndTaskId(int labelId, int taskId);
        IResult DeleteAllByLabelIdsAndTaskId(List<int> labelIds, int taskId);
        IResult DeleteByTaskLabelId(int taskLabelId);
        IResult DeleteAllByTaskLabelIds(List<int> taskLabelIds);

        IDataResult<List<TaskLabel>> GetAllByTaskId(int taskId);
        IDataResult<List<int>> GetAllIdByTaskId(int taskId);
    }
}
