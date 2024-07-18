using Business.Repository.BoardRepository;
using Entities.Concrete;
using Entities.DTOs.BoardDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(int customerId, int userId)
        {
            var result = _boardService.GetAll(customerId, userId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get")]
        public IActionResult GetAll(int boardId)
        {
            var result = _boardService.Get(boardId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult AddBoard([FromBody] Board board)
        {
            var result = _boardService.Add(board);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete")]
        public IActionResult DeleteBoard(int boardId)
        {
            var result = _boardService.Delete(boardId);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public IActionResult UpdateBoard([FromBody] EditBoardDto board)
        {
            var result = _boardService.Update(board);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
    }
}
