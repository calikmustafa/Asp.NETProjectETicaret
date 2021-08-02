using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
   public class SigningCredentialsHelper
    {
        //MESELA EMAİL VE KULLANICI ADI İLE GİRDİN SİSTEME BU CREDENTİALSDIR.(KULLANICI BİLGİLERİN)

        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);//sen bir tane hash işle yapıcaksın anahtar olarak bu anahtarı kullan şifreleme olarak güvenlik algortimalarından hmacsha512 yi kullan diyoruz
        }
    }
}
