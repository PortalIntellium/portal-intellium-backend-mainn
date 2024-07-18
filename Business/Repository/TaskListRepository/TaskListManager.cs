using Business.Repository.TaskRepository;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskListRepository;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;
using Entities.DTOs.TaskListDtos;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.TaskListRepository
{
    public class TaskListManager : ITaskListService
    {
        private readonly ITaskListDal _taskListDal;
        private readonly ITaskService _taskService;

        public TaskListManager(ITaskListDal taskListDal, ITaskService taskService)
        {
            _taskListDal = taskListDal;
            _taskService = taskService;
        }

        public IResult Add(TaskList taskList)
        {
            var result = _taskListDal.GetAll(p => p.BoardId.Equals(taskList.BoardId)).OrderByDescending(p => p.OrderNo).FirstOrDefault();

            taskList.OrderNo = (result != null) ? result.OrderNo + 1000 : 1000;
            _taskListDal.Add(taskList);
            return new SuccessResult("Ekleme İşlemi Başarılı");

        }

        public IResult Delete(int taskListId)
        {
            var removedTaskList = _taskListDal.Get(p => p.Id.Equals(taskListId));
            if (removedTaskList == null) return new ErrorResult("Silinecek görev listesi bulunamadı");

            var removedTasks = _taskService.GetAllByTaskListId(taskListId).Data;

            if (removedTasks != null)
            {
                _taskService.DeleteAll(removedTasks);
            }

            _taskListDal.Delete(removedTaskList);
            return new SuccessResult("Silme İşlemi Başarılı");
        }

        public IResult DeleteAll(List<TaskList> taskLists)
        {
            if (taskLists.IsNullOrEmpty()) return new ErrorResult("Silinecek görev listeleri bulunamadı");
            foreach (var taskList in taskLists)
            {
                Delete(taskList.Id);
            }
            return new SuccessResult();
        }

        public IDataResult<List<TaskList>> GetAllByBoardId(int boardId)
        {
            var result = _taskListDal.GetAll(p => p.BoardId.Equals(boardId));
            return (!result.IsNullOrEmpty()) ? new SuccessDataResult<List<TaskList>>(result)
                : new ErrorDataResult<List<TaskList>>("Bu panoya bağlı task list yok");
        }

        public IDataResult<List<TaskListDto>> GetAllWithTasks(int boardId)
        {
            var result = _taskListDal.GetAllWithTasks(boardId);
            return (!result.IsNullOrEmpty()) ? new SuccessDataResult<List<TaskListDto>>(result)
                : new ErrorDataResult<List<TaskListDto>>("Bu panoya bağlı task list yok");
        }

        public IResult Update(TaskList taskList)
        {
            var result = _taskListDal.Get(p => p.Id.Equals(taskList.Id));
            if (result == null) return new ErrorResult("Güncellenecek görev listesi bulunamadı");

            result.Name = taskList.Name;
            _taskListDal.Update(result);
            return new SuccessResult("Güncelleme işlemi Başarılı");
        }

        public IResult UpdateOrder(TaskListOrderEditDto taskList)
        {
            var result = _taskListDal.Get(p => p.Id.Equals(taskList.Id));
            if (result == null) return new ErrorResult("Güncellenecek görev listesi bulunamadı");

            result.OrderNo = taskList.OrderNo;
            _taskListDal.Update(result);
            return new SuccessResult("İşlem Başarılı");
        }
    }
}
