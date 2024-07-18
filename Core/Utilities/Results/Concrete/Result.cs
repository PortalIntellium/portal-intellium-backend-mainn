using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        // Bir işlemyapıldığında sonucu geri döndür. 
        //sadece veri getirir

        public Result(bool success)//Sadece sonuç isteyebilirim
        {
            Success = success;
        }
        public Result(bool success, string message):this(success)//hem sonuç hemde mesaj isteyebilirim
        {
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
