using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        //Başarılı işlem verisi döndürür
        public SuccessDataResult(T data) : base(data, true)//başarılı işlemn verisi döndürür
        {
        }

        public SuccessDataResult(T data, string message) : base(data, true, message)//başarılı işlem verisi ve mesajını döndürür
        {
        }
        public SuccessDataResult():base(default,true)//sadece true döner,veri dönmeyebilir
        {
            
        }
    }
}
