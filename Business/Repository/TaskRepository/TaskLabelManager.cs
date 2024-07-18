using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Business.Repository.TaskRepository
{
    public class TaskLabelManager : ITaskLabelService
    {

        private readonly ITaskLabelDal _taskLabelDal;

        public TaskLabelManager(ITaskLabelDal taskLabelDal)
        {
            _taskLabelDal = taskLabelDal;
        }

        public IResult Add(List<int> labelIds, int taskId)
        {
            foreach (var labelId in labelIds)
            {
                var result = _taskLabelDal.Get(label => label.LabelId.Equals(labelId) && label.TaskId.Equals(taskId));
                if (result == null)
                {
                    TaskLabel taskLabel = new()
                    {
                        LabelId = labelId,
                        TaskId = taskId
                    };
                    _taskLabelDal.Add(taskLabel);
                }
            }
            return new SuccessResult("Görev etiketleri eklendi.");
        }

        public IResult DeleteByLabelIdAndTaskId(int labelId, int taskId)
        {
            var result = _taskLabelDal.Get(p => p.LabelId.Equals(labelId) && p.TaskId.Equals(taskId));
            if (result == null) return new ErrorResult("Silinecek görev etiketi bulunamadı");

            _taskLabelDal.Delete(result);

            return new SuccessResult("Görev etiketleri silindi.");
        }
        public IResult DeleteAllByLabelIdsAndTaskId(List<int> labelIds, int taskId)
        {
            if (labelIds.IsNullOrEmpty()) return new ErrorResult("Silinecek görev etiketleri bulunamadı");
            foreach (var labelId in labelIds)
            {
                DeleteByLabelIdAndTaskId(labelId, taskId);
            }
            return new SuccessResult();
        }

        public IResult DeleteByTaskLabelId(int taskLabelId)
        {
            var result = _taskLabelDal.Get(p => p.Id.Equals(taskLabelId));
            if (result == null) return new ErrorResult("Silinecek görev etiketi bulunamadı");

            _taskLabelDal.Delete(result);

            return new SuccessResult("Görev etiketleri silindi.");
        }

        public IResult DeleteAllByTaskLabelIds(List<int> taskLabelIds)
        {
            if (taskLabelIds.IsNullOrEmpty()) return new ErrorResult("Silinecek görev etiketleri bulunamadı");
            foreach (var taskLabelId in taskLabelIds)
            {
                DeleteByTaskLabelId(taskLabelId);
            }
            return new SuccessResult();
        }

        public IDataResult<List<TaskLabel>> GetAllByTaskId(int taskId)
        {
            var result = _taskLabelDal.GetAll(p => p.TaskId.Equals(taskId));
            return new SuccessDataResult<List<TaskLabel>>(result);
        }

        public IDataResult<List<int>> GetAllIdByTaskId(int taskId)
        {
            var result = _taskLabelDal.GetAll(p => p.TaskId.Equals(taskId)).Select(p => p.Id).ToList();
            return new SuccessDataResult<List<int>>(result);
        }

        
    }
}
