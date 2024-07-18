using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Repository.TaskRepository
{
    public interface ITaskMemberService
    {
        IResult Add(List<int> userIds, int taskId);
        
        IResult DeleteByUserIdAndTaskId(int userId, int taskId);
        IResult DeleteAllByUserIdsAndTaskId(List<int> userIds, int taskId);

        IResult DeleteByTaskMemberId(int taskMemberId);
        IResult DeleteAllByTaskMemberIds(List<int> taskMemberIds);

        IDataResult<List<TaskMember>> GetAllByTaskId(int taskId);
        IDataResult<List<int>> GetAllIdByTaskId(int taskId);

        IDataResult<List<UserDto>> GetTaskMembersAsUserByBoardId(int boardId);
    }
}
