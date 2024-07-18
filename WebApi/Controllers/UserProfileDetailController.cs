using Business.Repository.UserProfileDetailRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileDetailController : ControllerBase
    {
        private readonly IUserProfileDetailService _profileDetailService;

        public UserProfileDetailController(IUserProfileDetailService profileDetailService)
        {
            _profileDetailService = profileDetailService;
        }

        [HttpPost("add")]
        public IActionResult Add(UserProfileDetails userProfileDetails)
        {
            var result = _profileDetailService.Add(userProfileDetails);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("getByUserId/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _profileDetailService.GetUserProfileDetailsByUserId(id);
            return result.Success ? Ok(result) : NotFound();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _profileDetailService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var result = _profileDetailService.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpPut("update")]
        public IActionResult Update(UserProfileDetails userProfileDetails)
        {
            var result = _profileDetailService.Update(userProfileDetails);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }

    }
}
