using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //Şifreleme olan sistemlerde bizim herşeyi byte array formatında vermemiz gerekiyor
    //KISACA SECURİTYKEY HELPER BİZİM OLUŞTURDUĞUMUZ ANAHTARI BYTE ANAHTARINA ÇEVİRMEMİZE YARIYOR
    
    public class SecurityKeyHelper
    {
        //securityKey appsetting de oluşşutrduğumuz anahtar kelimemiz
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));//bize simetrik bir anahtar gerek
        }
    }
}
