using Business.Repository.RolesForUsersRepository;
using Business.Repository.UserJobDetailsRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesForUsersControlles : ControllerBase
    {
        private readonly IRolesForUsersService _rolesForUsersService;

        public RolesForUsersControlles(IRolesForUsersService rolesForUsersService)
        {
            _rolesForUsersService = rolesForUsersService;
        }

        [HttpPost("add")]
        public IActionResult Add(RolesForUsers rolesForUsers)
        {
            var result = _rolesForUsersService.Add(rolesForUsers);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        
        [HttpGet("getByUserId")]
        public IActionResult GetByUserId(long userId)
        {
            var result = _rolesForUsersService.GetRolesForUsersByUserId(userId);
            return result.Success ? Ok(result) : NotFound();
        }

        [HttpGet("getByRoleId")]
        public IActionResult GetByRoleId(long roleId)
        {
            var result = _rolesForUsersService.GetRolesForUsersByRoleId(roleId);
            return result.Success ? Ok(result) : NotFound();
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _rolesForUsersService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var result = _rolesForUsersService.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
        [HttpPut("update")]
        public IActionResult Update(RolesForUsers rolesForUsers)
        {
            var result = _rolesForUsersService.Update(rolesForUsers);
            return result.Success ? Ok(result) : BadRequest(result.Message);

        }
    }
}
