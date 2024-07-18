using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.TaskRepository
{
    public class TaskCommentManager : ITaskCommentService
    {
        private readonly ITaskCommentDal _taskCommentDal;

        public TaskCommentManager(ITaskCommentDal taskCommentDal)
        {
            _taskCommentDal = taskCommentDal;
        }


        public IResult Add(TaskComment taskComment)
        {
            taskComment.CreatedDate = DateTime.Now;
            _taskCommentDal.Add(taskComment);
            return new SuccessResult("Ekleme Başarılı");
        }

        public IResult Delete(int taskCommentId)
        {
            var result = _taskCommentDal.Get(p => p.Id.Equals(taskCommentId));
            if (result == null) return new ErrorResult("Silinecek görev yorumu bulunamadı");

            _taskCommentDal.Delete(result);
            return new SuccessResult("Silme Başarılı");
        }

        public IResult DeleteAll(List<TaskCommentDto> taskComments)
        {
            if (taskComments.IsNullOrEmpty()) return new ErrorResult("Silinecek görev yorumları bulunamadı");

            foreach (var taskComment in taskComments)
            {
                Delete(taskComment.Id);
            }
            return new SuccessResult();
        }

        public IDataResult<List<TaskCommentDto>> GetAllByTaskId(int taskId)
        {
            var result = _taskCommentDal.GetAllByTaskId(taskId);

            return new SuccessDataResult<List<TaskCommentDto>>(result);

        }
    }
}
