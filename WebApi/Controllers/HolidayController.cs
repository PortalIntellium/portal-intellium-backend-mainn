using Business.Repository.HolidayRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/holiday")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpPost("add")]
        public IActionResult Add(Holiday holiday)
        {
            var result = _holidayService.Add(holiday);
            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _holidayService.GetAll();
            return result.Success ? Ok(result) : NotFound();
        }

        [HttpPut("update")]
        public IActionResult Update(Holiday holiday)
        {
            var result = _holidayService.Update(holiday);
            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _holidayService.Delete(id);
            return result.Success? Ok(result) : NotFound();
        }
    }
}
