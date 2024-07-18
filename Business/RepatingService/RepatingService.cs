using Business.Helpers;
using Castle.Core.Logging;
using DataAccess.Repository.UserPermissionRepository;
using DataAccess.Repository.UserRepository;
using Entities.Concrete;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Business
{
    public class RepatingService : BackgroundService
    {
        private ILogger<RepatingService> _logger;
        private readonly IUserPermissionDal _userPermissionDal;
        private readonly IUserDal _userDal;

        public RepatingService(ILogger<RepatingService> logger, IUserPermissionDal userPermissionDal, IUserDal userDal)
        {
            _logger = logger;
            _userPermissionDal = userPermissionDal;
            _userDal = userDal;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //iş kodları buraya yazılacak 
            while (!stoppingToken.IsCancellationRequested)
            {
                var users = _userDal.GetAll();
                DateTime now = DateTime.Now;


                foreach (var user in users)
                {
                    var day = CalculateDays(user.AddetAt, now);
                    var age = (int)((now - user.BirthDate).TotalDays / 365);
                    int calculatePermission = (day / 365);
                    var yesterdayAge = CalculateYesterdayAge(user);
                    var calculatePermissionYesterday = CalculateYesterdayPermission(user);

                    if (day % 365 == 0) {

                        var userPermission = _userPermissionDal.Get(x => x.UserId == user.Id);
                        if (userPermission != null)
                        {
                            userPermission.TransferredPermits += (userPermission.ThisYear - userPermission.UsedLeave);

                            int newPermission = UserPermissionCalculate.CalculateTotalLeave(user.AddetAt, user.BirthDate);
                            userPermission.TotalLeave = newPermission + userPermission.TransferredPermits;
                            userPermission.ReaminingLeave = userPermission.TotalLeave;
                            userPermission.UsedLeave = 0;
                            userPermission.ThisYear = newPermission;
                            _userPermissionDal.Update(userPermission);
                            Console.WriteLine(userPermission.TotalLeave);

                        }

                    }


                }
                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }
        private static int CalculateDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime - startTime;

            int usedDays = (int)duration.TotalDays;

            return usedDays;
        }
        private static int CalculateYesterdayAge(User user)
        {

            DateTime yesterday = DateTime.Now.Date.AddDays(-1);
            var age = (int)((yesterday - user.BirthDate).TotalDays / 365);


            return age;
        }

        private static int CalculateYesterdayPermission(User user)
        {

            DateTime yesterday = DateTime.Now.Date.AddDays(-1);
            var day = CalculateDays(user.AddetAt, yesterday);
            var age = (int)((yesterday - user.BirthDate).TotalDays / 365);
            int calculatePermission = (day / 365);

            return calculatePermission;
        }
    }
}
