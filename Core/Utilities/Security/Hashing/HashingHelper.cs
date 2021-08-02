using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
   public class HashingHelper
    {
        //out-> oraya döndürülen değerleri boş bile olsa doldurup geri döndürüyor
        //out'u dışarıya verilicek değer gibi düşünebiliriz.
        //kısaca ben şifre vericem outlar dışarıya değer döndürücek

        
        public static void CreatePasswordHash(string password,out byte[]passwordHash,out byte[]passwordSalt )//ona verdiğimiz bir şifrenin hashini oluşturucak ve salting de oluşturucak
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;//key o an oluşturduğunuz algortimanın her kullanıcı için farklı key oluşturur.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//şifreyi byte çeviricez
            }
        }


        //ŞifreHasini doğrula demek
        //Doğrulucağız girdiğimiz şifre ile  eşleşip eşleşmediği yerdir.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
             var   computeddHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeddHash.Length; i++)
                {
                    if (computeddHash[i]!=passwordHash[i])//passwordhash veritabanındaki şifremiz
                    {
                        return false;
                    }
                }
                return true;
            }
            
        }
    }
}
