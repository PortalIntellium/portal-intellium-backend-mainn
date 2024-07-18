namespace Entities.Concrete
{
    public class TaskTodo
    {
        public int Id { get; set; }

        public int TaskTodoListId { get; set; }

        public string Content { get; set; }

        public Boolean State { get; set; }
    }
}
