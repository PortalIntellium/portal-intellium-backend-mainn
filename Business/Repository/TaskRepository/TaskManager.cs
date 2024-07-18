using Business.Repository.TaskRepository.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.DTOs.TaskDtos;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.TaskRepository
{
    public class TaskManager : ITaskService
    {
        private readonly ITaskDal _taskDal;
        private readonly ITaskMemberService _taskMemberService;
        private readonly ITaskLabelService _taskLabelService;
        private readonly ITaskAttachmentService _taskAttachmentService;
        private readonly ITaskCommentService _taskCommentService;
        private readonly ITaskTodoListService _taskTodoListService;

        public TaskManager(ITaskDal taskDal, ITaskMemberService taskMemberService, ITaskLabelService taskLabelService,
            ITaskAttachmentService taskAttachmentService, ITaskCommentService taskCommentService, ITaskTodoListService taskTodoListService)
        {
            _taskDal = taskDal;
            _taskMemberService = taskMemberService;
            _taskLabelService = taskLabelService;
            _taskAttachmentService = taskAttachmentService;
            _taskCommentService = taskCommentService;
            _taskTodoListService = taskTodoListService;
        }
        public IResult Add(AddTaskDto taskDto)
        {
            var task = taskDto.Task;
            var maxOrder = _taskDal.GetAll(p => p.TaskListId.Equals(task.TaskListId)).OrderByDescending(p => p.OrderNo).FirstOrDefault();
            task.CreatedDate = DateTime.Now;
            task.OrderNo = (maxOrder != null) ? maxOrder.OrderNo + 1000 : 1000;


            _taskDal.Add(task);

            if (taskDto.AddUserIds != null) { _taskMemberService.Add(taskDto.AddUserIds, task.Id); }

            if (taskDto.AddLabelIds != null) { _taskLabelService.Add(taskDto.AddLabelIds, task.Id); }

            return new SuccessResult(TaskMessages.AddedTask);
        }

        public IResult Delete(int taskId)
        {
            var task = _taskDal.Get(p => p.Id.Equals(taskId));
            if (task == null) return new ErrorResult("Silinecek görev bulunamadı");

            var taskMembers = _taskMemberService.GetAllIdByTaskId(taskId).Data;
            if (taskMembers != null) { _taskMemberService.DeleteAllByTaskMemberIds(taskMembers); }

            var taskLabels = _taskLabelService.GetAllIdByTaskId(taskId).Data;
            if (taskLabels != null) { _taskLabelService.DeleteAllByTaskLabelIds(taskLabels); }


            var taskAttachments = _taskAttachmentService.GetAllIdByTaskId(taskId).Data;
            if (taskAttachments != null) { _taskAttachmentService.DeleteAll(taskAttachments); }

            var taskComments = _taskCommentService.GetAllByTaskId(taskId).Data;
            if (taskComments != null) { _taskCommentService.DeleteAll(taskComments); }

            var taskTodoLists = _taskTodoListService.GetAllByTaskId(taskId).Data;
            if (taskTodoLists != null) { _taskTodoListService.DeleteAll(taskTodoLists); }


            _taskDal.Delete(task);
            return new SuccessResult(TaskMessages.DeletedTask);
        }

        public IResult DeleteAll(List<Entities.Concrete.Task> tasks)
        {
            if (tasks.IsNullOrEmpty()) return new ErrorResult("Silinecek görevler bulunamadı");
            foreach (var task in tasks)
            {
                Delete(task.Id);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Entities.Concrete.Task>> GetAllByTaskListId(int taskListId)
        {
            var result = _taskDal.GetAll(p => p.TaskListId.Equals(taskListId));
            return new SuccessDataResult<List<Entities.Concrete.Task>>(result);
        }

        public IDataResult<TaskViewDto> GetById(int taskId)
        {
            var result = _taskDal.GetTaskByTaskId(taskId);
            return (result != null) ? new SuccessDataResult<TaskViewDto>(result) : new ErrorDataResult<TaskViewDto>("Görev bulunamadı");
        }

        public IResult Update(EditTaskDto taskDto)
        {
            var task = _taskDal.Get(p => p.Id.Equals(taskDto.Task.Id));
            if (task == null) return new ErrorResult("Güncellenecek görev bulunamadı");

            task.Name = taskDto.Task.Name;
            task.Description = taskDto.Task.Description;
            task.DueDate = taskDto.Task.DueDate;


            if (taskDto.AddUserIds != null) { _taskMemberService.Add(taskDto.AddUserIds, task.Id); }
            if (taskDto.RemoveUserIds != null) { _taskMemberService.DeleteAllByUserIdsAndTaskId(taskDto.RemoveUserIds, task.Id); }

            if (taskDto.AddLabelIds != null) { _taskLabelService.Add(taskDto.AddLabelIds, task.Id); }
            if (taskDto.RemoveLabelIds != null) { _taskLabelService.DeleteAllByLabelIdsAndTaskId(taskDto.RemoveLabelIds, task.Id); }

            _taskDal.Update(task);

            return new SuccessResult(TaskMessages.UpdatedTask);
        }

        public IResult UpdateOrder(TaskOrderEditDto taskOrderEditDto)
        {
            var task = _taskDal.Get(p => p.Id.Equals(taskOrderEditDto.Id));
            if (taskOrderEditDto.TaskListId != 0)
            {
                task.TaskListId = taskOrderEditDto.TaskListId;
            }
            task.OrderNo = taskOrderEditDto.OrderNo;
            _taskDal.Update(task);
            return new SuccessResult("Görevin konumu başarılı şekilde değiştirildi");
        }
    }
}
