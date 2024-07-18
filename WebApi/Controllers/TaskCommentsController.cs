using Business.Repository.TaskRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCommentsController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;

        public TaskCommentsController(ITaskCommentService taskCommentService)
        {
            _taskCommentService = taskCommentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(int taskId)
        {
            var result = _taskCommentService.GetAllByTaskId(taskId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] TaskComment taskComment)
        {
            var result = _taskCommentService.Add(taskComment);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int taskCommentId)
        {
            var result = _taskCommentService.Delete(taskCommentId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

    }
}
