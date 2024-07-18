using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Repository.TaskRepository
{
    public interface ITaskMemberDal : IEntityRepository<TaskMember>
    {
        List<UserDto> GetTaskMembersAsUserByBoardId(int boardId);
    }
}
