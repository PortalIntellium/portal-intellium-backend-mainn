using Business.Repository.BoardRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardMembersController : ControllerBase
    {
        private readonly IBoardMemberService _boardMemberService;

        public BoardMembersController(IBoardMemberService boardMemberService)
        {
            _boardMemberService = boardMemberService;
        }

        [HttpPost("add")]
        public ActionResult Add([FromBody] List<BoardMember> boardMembers)
        {
            var result = _boardMemberService.Add(boardMembers);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getall")]
        public ActionResult GetAllByBoardIdWithUsers(int boardId)
        {
            var result = _boardMemberService.GetAllByBoardIdWithUsers(boardId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
    }
}
