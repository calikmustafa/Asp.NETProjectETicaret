using Core.Utilities.Results;
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
        IDataResult<List<Product>>GetAll();
        IDataResult<List<Product>>GetAllByCategoryId(int id);
        IDataResult<List<Product>>GetByUnitPrice(decimal min,decimal max);
        IDataResult<List<ProductDetailDto>>GetProductDetails();
        IResult Add(Product product);
        //Tek başına bir ürün döndürüyor mesela ürüne tıkladın sayfadace sadece o ürünle ilgili bilgiler yer alıcak.
        IDataResult<Product> GetById(int productId);

    }
}
