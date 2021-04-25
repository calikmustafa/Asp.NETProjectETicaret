using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IProductService
    {
        //Business katmanında gelen parametreler arayüzden geliyor UNUTMA
        //İş kodlarını kurallıyacağım
        //Tüm ürünleri listeliyecek.
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int id);
        List<Product> GetByUnitPrice(decimal min,decimal max);
        List<ProductDetailDto> GetProductDetails();

    }
}
