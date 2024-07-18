using Business.Repository.HolidayRepository.Constans;
using Business.Repository.PermissionRepository.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repository.HolidayRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.HolidayRepository
{
    public class HolidayManager : IHolidayService
    {
        private readonly IHolidayDal _holidayDal;

        public HolidayManager(IHolidayDal holidayDal)
        {
            _holidayDal = holidayDal;
        }

        public IResult Add(Holiday holiday)
        {
            _holidayDal.Add(holiday);
            return new SuccessResult(HolidayMessages.AddedHoliday);
        }

        public IResult Delete(int id)
        {
            var holiday = _holidayDal.Get(x => x.Id.Equals(id));
            _holidayDal.Delete(holiday);
            return new SuccessResult(HolidayMessages.DeletedPermission);
        }

        public IDataResult<List<Holiday>> GetAll()
        {
            return new SuccessDataResult<List<Holiday>>(_holidayDal.GetAll());
        }

        public IResult Update(Holiday holiday)
        {
            _holidayDal.Update(holiday);
            return new SuccessResult(HolidayMessages.UpdatedPermission);
        }
    }
}
