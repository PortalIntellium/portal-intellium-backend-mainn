using DataAccess.Repository.HolidayRepository;
using DataAccess.Repository.LeaveDeducationRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class PermissionForms
    {
        private readonly IUserDal _userDal;
        private readonly ILeaveDeducationDal _leaveDeducationDal;

        public PermissionForms(IUserDal userDal, ILeaveDeducationDal leaveDeducationDal = null)
        {
            _userDal = userDal;
            _leaveDeducationDal = leaveDeducationDal;
        }

        public string GetFormPath(Permission permission,IHolidayDal holidayDal)
        {
            string templatePdfFile = GetTemplatePathByPermissionType(permission);
            string dynamicPath = "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\WebApi\\file-storage\\task-attachments\\";
            var user = GetUser(permission.UserId);
            var pdfName = GetTemplateNameByPermissionType(permission);
            string pdfFileName = $"{permission.Id}_{permission.PermissionType}_{pdfName}_izin_formu.pdf";

            string pdfFilePath = Path.Combine(dynamicPath, pdfFileName);

            PdfReader pdfReader = new PdfReader(templatePdfFile);

            using (FileStream filledPdfStream = new FileStream(pdfFilePath, FileMode.Create))
            {
                PdfStamper pdfStamper = new PdfStamper(pdfReader, filledPdfStream);
                AcroFields pdfFormFields = pdfStamper.AcroFields;

                pdfFormFields.SetField("user_name", user.Name);
                pdfFormFields.SetField("user_mission","Personel");
                pdfFormFields.SetField("user_addetAt", user.AddetAt.Date.ToString("dd/MM/yyyy"));
                pdfFormFields.SetField("permission_updatedDate", $"01.01.{DateTime.Now.Year}");
                pdfFormFields.SetField("permission_description", permission.Description);
                if(templatePdfFile == "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\Business\\Helpers\\PermissionForms\\IK004-saatlik_izin_formu.pdf")
                {
                    pdfFormFields.SetField("permission_startDateTime", permission.StartTime.Date.Hour.ToString());
                    pdfFormFields.SetField("permission_endDateTime", permission.EndTime.Date.Hour.ToString());
                    pdfFormFields.SetField("permission_totalHour", (permission.EndTime.Date.Hour - permission.StartTime.Date.Hour).ToString());
                }
                pdfFormFields.SetField("permission_startDate", " " + permission.StartTime.Date.ToString("dd/MM/yyyy"));
                pdfFormFields.SetField("permission_endDate", " " + permission.EndTime.Date.ToString("dd/MM/yyyy"));
                double result = UserPermissionCalculate.CalculateTotalWorkingDays(permission.StartTime, permission.EndTime, holidayDal, permission.PermissionType);
                pdfFormFields.SetField("permission_totalDay", result.ToString());
                pdfFormFields.SetField("user_contactPhone", permission.PhoneNumber);
                pdfFormFields.SetField("user_contactAddress", permission.Address);
                pdfFormFields.SetField("permission_dateTimeNow", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                pdfFormFields.SetField("permission_dateTimeYear", DateTime.Now.Year.ToString());

                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }

            return pdfFilePath;
        }

        private string GetTemplatePathByPermissionType(Permission permission)
        {
            
            var deducations = _leaveDeducationDal.GetAll();
            var user = GetUser(permission.UserId);

            

            if (deducations.Any(d => d.PermissionType.Equals(permission.PermissionType, StringComparison.OrdinalIgnoreCase)))
            {
                return "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\Business\\Helpers\\PermissionForms\\mazeret_izin_formu.pdf";
            }

            switch (permission.PermissionType)
            {
                case "Ücretli":
                    if ((DateTime.Now - user.AddetAt).TotalDays / 365 < 1)
                    {
                        return "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\Business\\Helpers\\PermissionForms\\IK003-_izin_hakkı_olmayan_personel_izin_talep_formu.pdf";
                    }
                    if (IsHourly(permission.StartTime, permission.EndTime))
                    {
                        return "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\Business\\Helpers\\PermissionForms\\IK004-saatlik_izin_formu.pdf";
                    }
                    return "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\Business\\Helpers\\PermissionForms\\ucretli_izin_formu.pdf";                
                case "Ücretsiz":
                    return "C:\\Users\\yesar\\OneDrive\\Masaüstü\\portal-intellium-backend\\Business\\Helpers\\PermissionForms\\ucretsiz_izin_formu.pdf";
                default:
                    return null; 
            }
        }

        private User GetUser(long id)
        {
            var user = _userDal.Get(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        private bool IsHourly(DateTime permissionStartTime, DateTime permissionEndTime)
        {
            TimeSpan permissionDuration = permissionEndTime - permissionStartTime;
            double totalDays = permissionDuration.TotalDays;

            return totalDays < 1 && permissionDuration.TotalHours > 0 && permissionDuration.TotalHours < 6;
        }

        private string GetTemplateNameByPermissionType(Permission permission)
        {
            var deducations = _leaveDeducationDal.GetAll();
            var user = GetUser(permission.UserId);

            if (deducations.Any(d => d.PermissionType.Equals(permission.PermissionType, StringComparison.OrdinalIgnoreCase)))
            {
                return "mazeret_izin_formu";
            }

            switch (permission.PermissionType)
            {
                case "Ücretli":
                    if ((DateTime.Now - user.AddetAt).TotalDays / 365 < 1)
                    {
                        return "IK003-_izin_hakkı_olmayan_personel_izin_talep_formu";
                    }
                    if (IsHourly(permission.StartTime, permission.EndTime))
                    {
                        return "IK004-saatlik_izin_formu";
                    }
                    return "ucretli_izin_formu";
                case "Ücretsiz":
                    return "ucretsiz_izin_formu";
                default:
                    return null;
            }
        }
    }
}
