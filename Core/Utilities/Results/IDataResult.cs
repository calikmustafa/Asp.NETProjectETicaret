using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //mesaj ve bool içeriyor IREsult dolayısıyla yeniden yazmama gerek yok
   public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}
