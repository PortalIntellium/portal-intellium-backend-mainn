using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.PermissionRepository
{
    public interface IPermissionService
    {
        IResult Add(Permission permission, IFormFile? documentFile);
        IResult Update(Permission permission);
        IDataResult<List<Permission>> GetAll();
        IResult Delete(Permission permission);
        IResult ConfirmPermission(int permissionId);
        IResult DeclinePermission(int permissionId);
        IDataResult<List<Permission>> GetByPermissionType(string permissionType);
        string CreatePermissionPDF(int permissionId);
        IDataResult<Permission> GetById(int permissionId);
        IDataResult<List<Permission>> GetPermissionByUserId(long userId);
    }
}
