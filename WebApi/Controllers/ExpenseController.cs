using Business.Repository.CustomerRepository;
using Business.Repository.ExpenseRepository;
using Business.Repository.UserRepository;
using Entities.Concrete;
using Entities.DTOs.ExpenseDto.ExpenseDto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;

        }

        [HttpPost("add")]
        public IActionResult Add(ExpenseDto expenseDto)
        {
            var result = _expenseService.Add(expenseDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] ExpenseDto expenseDto)
        {
            var result = _expenseService.Update(expenseDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]

        public IActionResult Delete(int id)
        {
            var result = _expenseService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getAll")]
        public IActionResult GetAll( long userId)
        {
            var result = _expenseService.GetAll(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _expenseService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }

}