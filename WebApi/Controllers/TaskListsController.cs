using Business.Repository.TaskListRepository;
using Entities.Concrete;
using Entities.DTOs.TaskDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly ITaskListService _taskListService;

        public TaskListsController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] TaskList taskList)
        {
            var result = _taskListService.Add(taskList);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int taskListId)
        {
            var result = _taskListService.Delete(taskListId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] TaskList taskList)
        {
            var result = _taskListService.Update(taskList);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPut("updateorder")]
        public IActionResult UpdateOrder([FromBody] TaskListOrderEditDto taskList)
        {
            var result = _taskListService.UpdateOrder(taskList);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getallwithtasks")]
        public IActionResult GetAllWithTasks(int boardId)
        {
            var result = _taskListService.GetAllWithTasks(boardId);
            return (result.Success) ? Ok(result) : StatusCode(204, result);
        }
    }
}
