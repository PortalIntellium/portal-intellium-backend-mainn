using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.TaskRepository;
using Entities.Concrete;
using Microsoft.IdentityModel.Tokens;

namespace Business.Repository.TaskRepository
{
    public class TaskTodoManager : ITaskTodoService
    {
        private readonly ITaskTodoDal _taskTodoDal;


        public TaskTodoManager(ITaskTodoDal taskTodoDal)
        {
            _taskTodoDal = taskTodoDal;
        }

        public IResult Add(TaskTodo taskTodo)
        {
            _taskTodoDal.Add(taskTodo);
            return new SuccessResult("Ekleme İşlemi Başarılı");
        }

        public IResult Change(int id, bool state)
        {
            var result = _taskTodoDal.Get(p => p.Id.Equals(id));
            if (result == null) return new ErrorResult("Todo bulunamadı");
            result.State = state;
            _taskTodoDal.Update(result);
            return new SuccessResult("Güncelleme işlemi başarılı");
        }

        public IResult Delete(int id)
        {
            var result = _taskTodoDal.Get(p => p.Id.Equals(id));
            if (result == null) return new ErrorResult("Silinecek TaskTodo bulunamadı");

            _taskTodoDal.Delete(result);
            return new SuccessResult("Silme işlemi başarılı");
        }

        public IResult DeleteAll(List<TaskTodo> taskTodos)
        {

            if (taskTodos.IsNullOrEmpty()) return new ErrorResult("Silinecek TaskTodolar bulunamadı");

            foreach (var taskTodo in taskTodos)
            {
                Delete(taskTodo.Id);
            }

            return new SuccessResult();
        }

        public IDataResult<List<TaskTodo>> GetAllByTodoListId(int todoListId)
        {
            var result = _taskTodoDal.GetAll(p => p.TaskTodoListId.Equals(todoListId));
            return new SuccessDataResult<List<TaskTodo>>(result);
        }

        public IResult Update(TaskTodo taskTodo)
        {
            var updatedTaskTodo = _taskTodoDal.Get(p => p.Id.Equals(taskTodo.Id));
            if (updatedTaskTodo == null) return new ErrorResult("Güncellenecek Task todo bulunamadı");

            updatedTaskTodo.Content = taskTodo.Content;
            _taskTodoDal.Update(updatedTaskTodo);
            return new SuccessResult("Güncelleme işlemi Başarılı");
        }
    }
}
