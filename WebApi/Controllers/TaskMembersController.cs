using Business.Repository.TaskRepository;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskMembersController : ControllerBase
    {
        private readonly ITaskMemberService _taskMemberService;
        public TaskMembersController(ITaskMemberService taskMemberService)
        {
            _taskMemberService = taskMemberService;
        }

        [HttpGet("getallbyboardid")]
        public IActionResult GetAllAsUserByBoardId(int boardId)
        {
            var result = _taskMemberService.GetTaskMembersAsUserByBoardId(boardId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
    }
}
