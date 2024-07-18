using Business.Repository.UserJobDetailsRepository;
using Business.Repository.UserProfileDetailRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserJobDetailController : ControllerBase
    {
        private readonly IUserJobDetailService _userJobDetailService;

        public UserJobDetailController(IUserJobDetailService userJobDetailService)
        {
            _userJobDetailService = userJobDetailService;
        }

        

        [HttpPost("add")]
        public IActionResult Add(UserJobDetail userJobDetail)
        {
            var result = _userJobDetailService.Add(userJobDetail);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("getByUserId")]
        public IActionResult GetById(int id)
        {
            var result = _userJobDetailService.GetByUserId(id);
            return result.Success ? Ok(result) : NotFound();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _userJobDetailService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var result = _userJobDetailService.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpPut("update")]
        public IActionResult Update(UserJobDetail userJobDetail)
        {
            var result = _userJobDetailService.Update(userJobDetail);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
    }
}
