using Business.Repository.UserLanguageDetailRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLanguageDetailController : ControllerBase
    {
        private readonly IUserLanguageDetailService _userLanguageDetailService;

        public UserLanguageDetailController(IUserLanguageDetailService userLanguageDetailService)
        {
            _userLanguageDetailService = userLanguageDetailService;
        }

        [HttpPost("add")]
        public IActionResult Add(UserLanguageDetail userLanguageDetail)
        {
            var result = _userLanguageDetailService.Add(userLanguageDetail);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("getByUserId")]
        public IActionResult GetById(int id)
        {
            var result = _userLanguageDetailService.GetUserLanguageByUserId(id);
            return result.Success ? Ok(result) : NotFound();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _userLanguageDetailService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var result = _userLanguageDetailService.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpPut("update")]
        public IActionResult Update(UserLanguageDetail userLanguageDetail)
        {
            var result = _userLanguageDetailService.Update(userLanguageDetail);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }

    }
}
