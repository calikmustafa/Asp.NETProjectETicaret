using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //ACCES TOKEN ANLAMSIZ KARAKTERLERDEN OLUŞAN TOKENDIR

    //KULLANICI BİZE POSTMAN'DAN ŞİFRE VE KULLANICI ADI VERİCEK.BİZDE ONA TOKEN VE NE ZAMANA KADAR GEÇERLİ BİLGİSİNİ VERİCEZ
   public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }//BİTİŞ ZAMANI TOKENIN
    }
}
