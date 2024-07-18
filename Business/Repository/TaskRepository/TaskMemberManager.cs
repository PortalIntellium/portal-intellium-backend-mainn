using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.TaskRepository
{
    public class TaskMemberManager : ITaskMemberService
    {
        private readonly ITaskMemberDal _taskMemberDal;

        public TaskMemberManager(ITaskMemberDal taskMemberDal)
        {
            _taskMemberDal = taskMemberDal;
        }

        public IResult Add(List<int> userIds, int taskId)
        {
            foreach (var id in userIds)
            {
                var result = _taskMemberDal.Get(user => user.UserId.Equals(id) && user.TaskId.Equals(taskId));
                if (result == null)
                {
                    TaskMember member = new()
                    {
                        UserId = id,
                        TaskId = taskId
                    };
                    _taskMemberDal.Add(member);
                }

            }
            return new SuccessResult("Ekleme Başarılı");
        }

        public IResult DeleteByUserIdAndTaskId(int userId, int taskId)
        {

            var result = _taskMemberDal.Get(p => p.UserId.Equals(userId) && p.TaskId.Equals(taskId));
            if (result == null) return new ErrorResult("Silinecek göreve atanmış kullanıcı bulunamadı");

            _taskMemberDal.Delete(result);
            return new SuccessResult("Silme Başarılı");

        }

        public IResult DeleteAllByUserIdsAndTaskId(List<int> userIds, int taskId)
        {
            if (userIds.IsNullOrEmpty()) return new ErrorResult("Silinecek göreve atanmış kullanıcılar bulunamadı");
            foreach (var userId in userIds)
            {
                DeleteByUserIdAndTaskId(userId, taskId);
            }
            return new SuccessResult();
        }

        public IResult DeleteByTaskMemberId(int taskMemberId)
        {
            var result = _taskMemberDal.Get(p => p.Id.Equals(taskMemberId));
            if (result == null) return new ErrorResult("Silinecek göreve atanmış kullanıcı bulunamadı");

            _taskMemberDal.Delete(result);
            return new SuccessResult("Silme Başarılı");
        }
        public IResult DeleteAllByTaskMemberIds(List<int> memberIds)
        {
            if (memberIds.IsNullOrEmpty()) return new ErrorResult("Silinecek göreve atanmış kullanıcılar bulunamadı");
            foreach (var memberId in memberIds)
            {
                DeleteByTaskMemberId(memberId);
            }
            return new SuccessResult();
        }



        public IDataResult<List<TaskMember>> GetAllByTaskId(int taskId)
        {
            var result = _taskMemberDal.GetAll(p => p.TaskId.Equals(taskId));
            return new SuccessDataResult<List<TaskMember>>(result);
        }

        public IDataResult<List<int>> GetAllIdByTaskId(int taskId)
        {
            var result = _taskMemberDal.GetAll(p => p.TaskId.Equals(taskId)).Select(p => p.Id).ToList();
            return new SuccessDataResult<List<int>>(result);
        }

        public IDataResult<List<UserDto>> GetTaskMembersAsUserByBoardId(int boardId)
        {
            var result = _taskMemberDal.GetTaskMembersAsUserByBoardId(boardId);
            return new SuccessDataResult<List<UserDto>>(result);
        }
    }
}
