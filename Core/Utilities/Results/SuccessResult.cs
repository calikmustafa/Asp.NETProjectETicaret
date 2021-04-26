using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
   public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, message)
            //base demek Result şuan base göndermek istediğni yazıcaksın
            //success olduğu için truesını burda oluşturuyorum 
        {

        }
        //mesaj vermedin ama base e true yolluyorsun tek parametreli çalışıcak ve sana basedeki successi vericek
        public SuccessResult():base(true)
        {

        }
    }
}
