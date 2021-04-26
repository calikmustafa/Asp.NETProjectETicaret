using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //Artık Bu clası newlediğim de constructor çalışacağı için ben parametre ile true,ve mesajımı gönderebileceğim 
        public Result(bool success, string message):this(success)
            //iki parametre yolladığında sadece bu method çalışıcak ama sen çalış aynı zamanda tek parametreli olanıda çalıştır demek.
            //this demek bu class successli olan tek parametreli consta successi yolla
        {
            Message = message;
        }
        //Mesaj vermeden sadece işlem sonucunu döndürmek istediğimizde bu const kullanıcaz.
        public Result(bool success)
        {
            Success = success;
            
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
