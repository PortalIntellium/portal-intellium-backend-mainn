using Business.Repository.UserRepository;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        
       
        [HttpPut("update")]
        public IActionResult Update(EditUserDto editUser)
        {
            var result= _userService.UpdateAsDto(editUser);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("changeImage")]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10MB
        public async Task<IActionResult> ChangeImage([FromForm] IFormFile image, [FromForm] long userId)
        {
            var result = await _userService.ChangeImage(image, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("removeImage")]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10MB
        public IActionResult RemoveImage(long userId)
        {
            var result = _userService.RemoveImage(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getuserlist")]
        public IActionResult GetUserList()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(long id)
        {
            var result=_userService.GetUserAsDtoById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getByName")]
        public IActionResult GetByName(string name)
        {
            var result = _userService.GetByName(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
