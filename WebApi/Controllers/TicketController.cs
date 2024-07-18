using Business.Repository.TicketRepository;
using Entities.DTOs.TicketDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpPost("add")]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10MB
        public async Task<IActionResult> Add([FromForm]AddTicketDto ticket, [FromForm] List<IFormFile>? attachments)
        {
            var result = await _ticketService.Add(ticket, attachments);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("update")]
        public IActionResult Update(EditTicketDto ticket)
        {
            var result = _ticketService.Update(ticket);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var result = _ticketService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllByCustomer")]
        public IActionResult GetAll(long customerId)
        {
            var result = _ticketService.GetAllByCustomerId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getLastTicketsByCustomer")]
        public IActionResult GetLastTicketsByCustomerId(long customerId, int ticketCount)
        {
            var result = _ticketService.GetLastTicketsByCustomerId(customerId, ticketCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getLastTickets")]
        public IActionResult GetLastTicekts(int ticketCount)
        {
            var result = _ticketService.GetLastTickets(ticketCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _ticketService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getById")]
        public IActionResult GetById(long id)
        {
            var result = _ticketService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getCount")]
        public IActionResult GetTicketCount()
        {
            var result = _ticketService.GetTicketCount();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getCountByCustomerId")]
        public IActionResult GetTicketCountByCustomerId(int customerId)
        {
            var result = _ticketService.GetTicketCountByCustomerId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
