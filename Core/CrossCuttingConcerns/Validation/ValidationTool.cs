using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
   public static class ValidationTool
    {
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);//product için doğrulama yapıcam diyoruz.
  
            var result = validator.Validate(context);//yazdığımız validate ile bağdaştırdık
            if (!result.IsValid)//sonuç geçerli değil ise
            {
                throw new ValidationException(result.Errors);//hata fırlat
            }
        }
    }
}
