using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        public bool Success { get;}//sadece veri getirir
        public string Message { get;  }
    }
}
