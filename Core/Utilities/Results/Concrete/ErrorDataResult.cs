using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //Hatalı işlem sonucu döndürür
        public ErrorDataResult(T data) : base(data, false)//hatalı işlem verisi döndürür
        {
        }

        public ErrorDataResult(T data, string message) : base(data, false, message)//hatalı işlem verisi ve mesajını döndürür
        {
        }
        public ErrorDataResult() : base(default, false)//sadece false döner,veri dönmeyebilir
        {

        }
        public ErrorDataResult(string message):base(default,false,message)
        {
            
        }
    }
}
