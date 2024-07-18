using Business.Repository.DebitRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/debit")]
    [ApiController]
    public class DebitController : ControllerBase
    {
        private readonly IDebitService _debitService;

        public DebitController(IDebitService debitService)
        {
            _debitService = debitService;
        }

        [HttpPost("add")]
        public IActionResult Add(Debit debit)
        {
            var result = _debitService.Add(debit);

            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpGet("getdebits")]
        public IActionResult GetDebits()
        {
            var result = _debitService.GetAll();
            return result.Success ? Ok(result) : NotFound();
        }
    }
}
