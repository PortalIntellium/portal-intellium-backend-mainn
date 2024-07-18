using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.TaskRepository
{
    public class TaskTodoListManager : ITaskTodoListService
    {
        private readonly ITaskDal _taskDal;
        private readonly ITaskTodoListDal _taskTodoListDal;
        private readonly ITaskTodoService _taskTodoService;

        public TaskTodoListManager (ITaskTodoListDal taskTodoListDal, ITaskTodoService taskTodoService, ITaskDal taskDal)
        {
            _taskTodoListDal = taskTodoListDal;
            _taskTodoService = taskTodoService;
            _taskDal = taskDal;
        }

        public IResult Add(TaskTodoList taskTodoList)
        {
            var result = _taskDal.Get(p => p.Id.Equals(taskTodoList.TaskId));
            if (result == null) return new ErrorResult("Task bulunamadı");
            _taskTodoListDal.Add(taskTodoList);
            return new SuccessResult("Ekleme işlemi başarılı");
        }

        public IResult Delete(int id)
        {
            var result = _taskTodoListDal.Get(p => p.Id.Equals(id));
            if (result == null) return new ErrorResult("Silme işlemi gerçekleştirilemedi");

            var deletedTaskTodos = _taskTodoService.GetAllByTodoListId(id).Data;
            if (!deletedTaskTodos.IsNullOrEmpty())
                _taskTodoService.DeleteAll(deletedTaskTodos);

            _taskTodoListDal.Delete(result);
            return new SuccessResult("Silme işlemi başarılı");

        }

        public IResult DeleteAll(List<TaskTodoList> taskTodoLists)
        {
            if (taskTodoLists.IsNullOrEmpty()) return new ErrorResult("Silme işlemi gerçekleştirilemedi");

            foreach (var taskTodoList in taskTodoLists)
            {
                Delete(taskTodoList.Id);
            }
            return new SuccessResult();
        }

        public IDataResult<List<TaskTodoList>> GetAllByTaskId(int taskId)
        {
            var result = _taskTodoListDal.GetAll(p => p.TaskId.Equals(taskId));
            return new SuccessDataResult<List<TaskTodoList>>(result);
        }

        public IDataResult<List<TaskTodoListDto>> GetAllWithTodoByTaskId(int taskId)
        {
            var result = _taskTodoListDal.GetAllWithTodoByTaskId(taskId);
            return new SuccessDataResult<List<TaskTodoListDto>>(result);
        }

        public IResult Update(TaskTodoList taskTodoList)
        {
            var updatedTaskTodoList = _taskTodoListDal.Get(p => p.Id.Equals(taskTodoList.Id));
            if (updatedTaskTodoList == null) return new ErrorResult("Güncellenecek Task todo list bulunamadı");

            updatedTaskTodoList.Title = taskTodoList.Title;
            _taskTodoListDal.Update(updatedTaskTodoList);
            return new SuccessResult("Güncellendi");
        }
    }
}
