using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //KULLANICI ŞİFREYİ VE KULLANICI ADINI POSTMANDAN GİRİCEK APİYE GÖNDERİCEK
    //EĞER DOĞRUYSA ACCESSTOKEN ÇALIŞICAK İLGİLİ KULLANICI İÇİN OPERATİONCLAİMLERİNİ BULUCAK JSONWEBTOKEN OLUŞUTURCAK VE GERİ DÖNDERİCEK
   public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim>operationClaims);
    }
}
