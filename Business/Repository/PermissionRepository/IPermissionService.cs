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
        IResult ConfirmPermission(int permissionId); //Onaylama işlemi  
        IResult DeclinePermission(int permissionId); //Reddetme işlemi
        IDataResult<List<Permission>> GetByPermissionType(string permissionType);
        string CreatePermissionPDF(int permissionId);//PermissionId için bir izin pdf i olusturur
        IDataResult<Permission> GetById(int permissionId);
        IDataResult<List<Permission>> GetPermissionByUserId(long userId);
    }
}
