using Business.Helpers;
using Business.Repository.PermissionRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.HolidayRepository;
using DataAccess.Repository.LeaveDeducationRepository;
using DataAccess.Repository.PermissionRepository;
using DataAccess.Repository.UserPermissionRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Repository.PermissionRepository
{
    public class PermissionManager : IPermissionService
    {
        private readonly IPermissionDal _permissionDal;
        private readonly IUserPermissionDal _userPermissionDal;
        private readonly IHolidayDal _holidayDal;
        private readonly ILeaveDeducationDal _leaveDeducationDal;
        private readonly IUserDal _userDal;
        private readonly PermissionHelpers _permissionHelpers;

        public PermissionManager(IPermissionDal permissionDal, IUserPermissionDal userPermissionDal, IHolidayDal holidayDal, ILeaveDeducationDal leaveDeducationDal, IUserDal userDal, PermissionHelpers permissionHelpers)
        {
            _permissionDal = permissionDal;
            _userPermissionDal = userPermissionDal;
            _holidayDal = holidayDal;
            _leaveDeducationDal = leaveDeducationDal;
            _userDal = userDal;
            _permissionHelpers = permissionHelpers;
        }

        public IResult Add(Permission permission, IFormFile? documentFile)
        {
            UserPermission userPermission = _userPermissionDal.GetUserPermissionByUserId(permission.UserId);
            if (userPermission != null)
            {
                var leaveDeductionHelpers = new LeaveDeducationHelpers(_leaveDeducationDal);
                if (!leaveDeductionHelpers.IsPermissionWithinDeductionRange(permission.PermissionType, permission.StartTime, permission.EndTime))
                {
                    return new ErrorResult(PermissionMessages.OutOfRange);
                }

                if (documentFile != null)
                {
                    string pdfPath = _permissionHelpers.GetFilePath(documentFile);

                    // sign necessary veriable
                    permission.DocumentPath = pdfPath;
                }

                permission.Status = "Pending";
                permission.IsAllowed = false;
                _permissionDal.Add(permission);
                return new SuccessResult(PermissionMessages.AddedPermission);
            }
            return new ErrorResult(PermissionMessages.UserNotFound);
        }

        public IResult ConfirmPermission(int permissionId)
        {
            var permission = _permissionDal.Get(x => x.Id == permissionId);
            double workingDays = UserPermissionCalculate.CalculateTotalWorkingDays(permission.StartTime, permission.EndTime, _holidayDal, permission.PermissionType);
            UserPermission userPermission = _userPermissionDal.GetUserPermissionByUserId(permission.UserId);
            User user = _userDal.Get(x => x.Id == permission.UserId);
            var leaveDeductionHelpers = new LeaveDeducationHelpers(_leaveDeducationDal);

            if (permission != null)
            {
                permission.IsAllowed = true;
                permission.Status = "Confirmed";
                var leaveDeductions = _leaveDeducationDal.GetByPermissionType(permission.PermissionType);

                if (leaveDeductions == null)
                {
                    // 
                    userPermission.UsedLeave += workingDays;
                    userPermission.ReaminingLeave = userPermission.TotalLeave - userPermission.UsedLeave;
                }

                // Update Database
                _userPermissionDal.Update(userPermission);
                _permissionDal.Update(permission);

                // Send Mail
                SendMail sendMail = new SendMail();
                string subject = "İzin Talebiniz Kabul Edildi";
                string body = $"İzin talebiniz kabul edildi. \n Tarih : {DateTime.Now} \n ";
                //_ = sendMail.SendEmailAsync(user.Email, subject, body);

                return new SuccessResult(PermissionMessages.ConfirmPermission);

            }
            return new ErrorResult(PermissionMessages.UserNotFound);
        }

        public IResult DeclinePermission(int permissionId)
        {
            var permission = _permissionDal.Get(x => x.Id == permissionId);
            User user = _userDal.Get(x => x.Id == permission.UserId);

            if (permission != null)
            {
                permission.IsAllowed = false;
                permission.Status = "Declined";

                _permissionDal.Update(permission);

                SendMail sendMail = new SendMail();
                string subject = "İzin Talebiniz Reddedildi";
                string body = $"İzin talebiniz reddedildi. \n Tarih : {DateTime.Now} \n ";
               // _ = sendMail.SendEmailAsync(user.Email, subject, body);


                return new SuccessResult(PermissionMessages.DeclinePermission);
            }
            return new ErrorResult(PermissionMessages.UserNotFound);
        }

        public IResult Delete(Permission permission)
        {
            _permissionDal.Delete(permission);
            return new SuccessResult(PermissionMessages.DeletedPermission);
        }

        public IDataResult<List<Permission>> GetAll()
        {
            return new SuccessDataResult<List<Permission>>(_permissionDal.GetAll());
        }

        public IDataResult<List<Permission>> GetByPermissionType(string permissionType)
        {
            return new SuccessDataResult<List<Permission>>(_permissionDal.GetByPermissionType(permissionType));
        }

        public string CreatePermissionPDF(int permissionId)
        {
            var permission = _permissionDal.Get(x => x.Id == permissionId);

            if (permission == null)
            {
                throw new Exception("İzin bulunamadı");
            }

            PermissionForms permissionForms = new PermissionForms(_userDal, _leaveDeducationDal);

            try
            {
                return permissionForms.GetFormPath(permission, _holidayDal);
            }
            catch (Exception ex)
            {
                throw new Exception($"PDF oluşturma hatası: {ex.Message}");
            }
        }

        public IResult Update(Permission permission)
        {
            _permissionDal.Update(permission);
            return new SuccessResult(PermissionMessages.UpdatedPermission);
        }

        public IDataResult<Permission> GetById(int permissionId)
        {
            var permission = _permissionDal.GetById(permissionId);
            return new SuccessDataResult<Permission>(permission);
        }

        public IDataResult<List<Permission>> GetPermissionByUserId(long userId)
        {
            return new SuccessDataResult<List<Permission>>(_permissionDal.GetPermissionByUserId(userId));
        }
    }
}
