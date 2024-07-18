using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.HolidayRepository
{
    public interface IHolidayService
    {
        IResult Add(Holiday holiday);
        IResult Update(Holiday holiday);
        IDataResult<List<Holiday>> GetAll();
        IResult Delete(int id);
    }
}
