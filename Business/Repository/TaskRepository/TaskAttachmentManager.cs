using Business.File;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.TaskRepository
{
    public class TaskAttachmentManager : ITaskAttachmentService
    {
        private readonly ITaskAttachmentDal _taskAttachmentDal;
        private readonly IFileService _fileService;
        public TaskAttachmentManager(ITaskAttachmentDal taskAttachmentDal, IFileService fileService)
        {
            _taskAttachmentDal = taskAttachmentDal;
            _fileService = fileService;
        }

        public async Task<IResult> Add(List<IFormFile> taskAttachments, int taskId)
        {
            foreach (var taskAttachment in taskAttachments)
            {
                if (taskAttachment.Length > 0)
                {
                    var result = await _fileService.Save(taskAttachment, FileType.TASK_ATTACHMENT);
                    if (!result.Success) continue;

                    TaskAttachment newTaskAttachment = new()
                    {
                        TaskId = taskId,
                        Name = result.Data.Name,
                        AttachmentPath = result.Data.FilePath
                    };
                    _taskAttachmentDal.Add(newTaskAttachment);
                }
            }
            return new SuccessResult("Başarılı");
        }

        public IResult Delete(int taskAttachmentId)
        {
            var result = _taskAttachmentDal.Get(p => p.Id.Equals(taskAttachmentId));
            if (result == null) return new ErrorResult("Silinecek görev eki bulunamadı");

            _taskAttachmentDal.Delete(result);
            _fileService.Delete(result.AttachmentPath, FileType.TASK_ATTACHMENT);

            return new SuccessResult();
        }
        public IResult DeleteAll(List<int> taskAttachmentIds)
        {
            if (taskAttachmentIds.IsNullOrEmpty()) return new ErrorResult("Silinecek görev ekleri bulunamadı");
            foreach (var taskAttachmentId in taskAttachmentIds)
            {
                Delete(taskAttachmentId);
            }
            return new SuccessResult();
        }

        public IDataResult<List<TaskAttachment>> GetAllByTaskId(int taskId)
        {
            var result = _taskAttachmentDal.GetAll(p => p.TaskId.Equals(taskId));
            return new SuccessDataResult<List<TaskAttachment>>(result);
        }

        public IDataResult<List<int>> GetAllIdByTaskId(int taskId)
        {
            var result = _taskAttachmentDal.GetAll(p => p.TaskId.Equals(taskId)).Select(p => p.Id).ToList();
            return new SuccessDataResult<List<int>>(result);
        }
    }
}
