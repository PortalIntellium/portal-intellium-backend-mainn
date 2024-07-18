using Business.Helpers;
using Business.Repository.PermissionRepository;
using Business.Repository.UserPermissionRepository;
using Business.Repository.UserRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/userPermission")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;
        private readonly IUserDal _userDal;

        public UserPermissionController(IUserPermissionService userPermissionService, IUserDal userDal)
        {
            _userPermissionService = userPermissionService;
            _userDal = userDal;
        }

        [HttpPost("add")]
        public IActionResult Add(UserPermission userPermission)
        {
            var result = _userPermissionService.Add(userPermission);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userPermissionService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }
        [HttpPut("updateuserpermission")]
        public IActionResult UpdateUserPermissionById(UserPermission userPermission)
        {
            var result = _userPermissionService.Update(userPermission);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getUserPermissionById")]
        public IActionResult GetUserPermissionById(int id)
        {
            var result = _userPermissionService.GetUserPermissionById(id);
            return Ok(result);
        }

        [HttpGet("userPermissionExportToExcel")]
        public IActionResult UserPermissionExportToExcel()
        {
            try
            {
                // UserPermissionExportToExcel sınıfını kullanarak Excel'e veri aktarımını gerçekleştir
                var excelExporter = new UserPermissionExportToExcel(_userPermissionService, _userDal);
                excelExporter.ExportToExcel();

                return Ok("Excel dosyası başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir HTTP durumuyla geri dön
                return BadRequest($"Hata oluştu: {ex.Message}");
            }
        }

    }
}
