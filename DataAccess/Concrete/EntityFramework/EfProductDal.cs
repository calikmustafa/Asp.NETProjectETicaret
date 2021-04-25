using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Bize hangi veritabanınla kullandıysan ve hangi nesne ile çalıştıysan kodların içerisine yazıp döndürüyor.
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            //Join atıyoruz şuan
            using (NorthwindContext context=new NorthwindContext())
            {
                // categoryId ler eşitse Ürünlerle categorileri join et demek ürünlere p demek joinlere c de demek
                var result = from p in context.Products
                             join c in context.Categories

                             on p.CategoryId equals c.CategoryId
                             // sonucu yazdığım kolonlara uydurarak ver
                             select new ProductDetailDto {ProductId=p.ProductId,ProductName=p.ProductName
                             ,CategoryName=c.CategoryName,UnitsInStock=p.UnitsInStock };
                return result.ToList();
            }
        }
    }
}
