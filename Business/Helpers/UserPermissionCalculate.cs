using DataAccess.Repository.HolidayRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class UserPermissionCalculate
    {
        public static int CalculateTotalLeave(DateTime userCreatedAt, DateTime userBirthDate)
        {
            DateTime now = DateTime.Now;
            var day = CalculateDays(userCreatedAt, now);
            var age = (int)((now - userBirthDate).TotalDays / 365);
            int calculatePermission = (day / 365);

            if (calculatePermission >= 1 && calculatePermission <= 5)
            {
                if (age <= 18 || age >= 50)
                {
                    return 20;
                }
                return 14;
            }
            else if (calculatePermission > 5 && calculatePermission < 15)
            {
                return 20;
            }
            else if (calculatePermission >= 15)
            {
                return 26;
            }

            return 0;
        }

        public static int CountWeekends(DateTime start, DateTime end)
        {
            int weekends = 0;
            for (DateTime date = start; date < end; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekends++;
                }
            }
            return weekends;
        }

        public static int CalculateDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime - startTime;

            int usedDays = (int)duration.TotalDays;

            return usedDays;
        }
        

        public static double CalculateTotalWorkingDays(DateTime permissionStartTime, DateTime permissionEndTime, IHolidayDal holidayDal, string permissionType)
        {
            if(permissionType == "ucretsiz")
            {
                return 0;
            }
            double usedDays = CalculateDays(permissionStartTime, permissionEndTime);
            var holidayHelpers = new HolidayHelpers(holidayDal);

            int weekends = CountWeekends(permissionStartTime, permissionEndTime);
            int holidayCount = holidayHelpers.GetHolidayBetweenDates(permissionStartTime, permissionEndTime);
            int total = weekends + holidayCount;
            double workingDays = usedDays - total;
            TimeSpan permissionDuration = permissionEndTime - permissionStartTime;
            double totalHours = permissionDuration.TotalHours;
            var hour = totalHours % 24;
            if (hour == 0)
            {
                return workingDays;
            }
            else if (hour < 6)
            {
                workingDays += 0.5;
            }
            else if (hour >= 6 && hour < 24)
            {
                workingDays += 1;
            }
            return workingDays;
        }
    }
}
