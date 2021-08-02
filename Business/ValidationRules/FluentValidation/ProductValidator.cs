using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    //fluent validator eklentimizi ekledik şimdi ise onu fluentvalidation dan çözümlemeyi yapın
   public class ProductValidator:AbstractValidator<Product>
    {
        //kurallar ctorun içerisine yazılır.
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA);
        }
        //arg burda productname
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
