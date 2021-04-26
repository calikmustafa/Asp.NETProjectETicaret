using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç çünkü void metodlar sadece işlem sonucu döndürür yani gerçekleşti veya gerçekleşmedi gibi
    //mesaj vermemiz yeterli
  public  interface IResult
    {
        //Get demek okumak demek sadece okuyacağız
        bool Success { get; }
        string Message { get; }
    }
}
