using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    //AbstractValidator<Product> biz burda product için validation işlemleri yapacağımızı belirtiyoruz.
    public class ProductValidator:AbstractValidator<Product>
    {
        //kurallar ctorun içerisine yazılır.
        public ProductValidator()
        {
            //p burda product için delegedir.
            //productname için kuralı yazıyoruz product name minimum 2 karakter olmalıdır
            RuleFor(p=>p.ProductName).MinimumLength(2);
            //procutname boş geçilemez.
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            //cotegoryıd değeri 1 olan ürünlerin ürün fiyatı 10 dan büyük olmalıdır.
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //A ile başlamalıdır ürün ismi diyecek.StartwithA kendi method ismimiz.
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");
        }


        
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");//a ile başlarsa true döner.
        }
    }
}
