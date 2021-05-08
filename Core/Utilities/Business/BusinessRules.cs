using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
   public class BusinessRules
    {
        //metodu static yazarsan clası yapmana gerek yok ve newlenemede gerek kalmaz.
        public static IResult Run(params IResult[] logics)//params verdiğiniz zaman run içerisine istediğiniz kadar IResult verebiliyorsunuz istediğiniz kadar.
                                                          //CheckIfProductNameExists gibi IResultları arrayin içerisine atar params ve logic olur arrayimiz ismi
        {
            foreach (var logic in logics)//CheckIfProductCountOfCategoryCorrect
            {
                if (!logic.Success)
                {
                    return logic;//kurala uymayanı döndürüyoruz
                }
            }
            return null;
        }
    }
}
