using Business.Repository.DebitRepository;
using Business.Repository.PermissionRepository;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] Permission permission, [FromForm] IFormFile? documentFile)
        {
            var result = _permissionService.Add(permission, documentFile);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getPermission")]
        public IActionResult GetPermission()
        {
            var result = _permissionService.GetAll();
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPatch("confirmPermission")]
        public IActionResult ConfirmPermission(int permissionId)
        {
            var result = _permissionService.ConfirmPermission(permissionId);
            return result.Success? Ok(result) : NotFound(result);
        }

        [HttpPatch("declinePermission")]
        public IActionResult DeclinePermission(int permissionId)
        {
            var result = _permissionService.DeclinePermission(permissionId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Permission permission)
        {
            var result = _permissionService.Update(permission);
            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpGet("getByPermissionType")]
        public IActionResult GetByPermissionType(string permissionType)
        {
            var result = _permissionService.GetByPermissionType(permissionType);
            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpGet("createPermissionPDF")]
        public IActionResult CreatePermissionPDF(int permissionId)
        {
            try
            {
                
                string pdfPath = _permissionService.CreatePermissionPDF(permissionId);

                // PDF dosyasının içeriğini oku
                byte[] fileBytes = System.IO.File.ReadAllBytes(pdfPath);

                if (fileBytes != null)
                {
                    // İstemciye PDF dosyasını ve içeriğini dön
                    string[] fileNameParts = Path.GetFileNameWithoutExtension(pdfPath).Split('_');
                    string additionalInfo = fileNameParts.Length > 2 ? fileNameParts[2] : "";

                    // Return the PDF file using PhysicalFileResult with a modified file name
                    return PhysicalFile(pdfPath, "application/pdf", $"{permissionId}_{additionalInfo}_izin_formu.pdf");
                }
                else
                {
                    return NotFound("PDF dosyası bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"PDF oluşturma hatası: {ex.Message}");
            }
        }

        [HttpGet("getPermissionByUserId")]
        public IActionResult GetByPermissionByUserId(long userId)
        {
            var result = _permissionService.GetPermissionByUserId(userId);
            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpGet("getPermissionById")]
        public IActionResult GetPermissionById(int id)
        {
            var result = _permissionService.GetById(id);
            return result.Success ? Ok(result) : NotFound(result);

        }
    }
}
