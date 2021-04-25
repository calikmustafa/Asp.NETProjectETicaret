using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //IEntityRepository interfacesi bize product mı category mi belirleyip methodların içine yazıp döndürüyor.
   public interface IProductDal:IEntityRepository<Product>
    {
        //ProductDala özgü bir join yazacağım zaman bu interface kullanıcam
        List<ProductDetailDto> GetProductDetails();

    }
}
//Code Refactoring
