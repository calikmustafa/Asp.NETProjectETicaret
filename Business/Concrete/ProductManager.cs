using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Soyut sınıfdan bağlantı kurduk
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)//birisi senden IProductDal isterse sen ona efProductDalı ver diyoruz.
        {
            _productDal = productDal;
        }




        [ValidationAspect(typeof(ProductValidator))]//add metodunu doğrula productvalitora göre
        //add methodunu çalıştırmadan önce program bakıyorum yukarıya bir attribute var önce onu çalıştırıyor.
        public IResult Add(Product product)
        {
            //validation-- doğrulama eklemeye çalıştığın varlığı nesnenin bu iş kurallarına uygun oluğ olmadığını kontrol ediyor.
            //eklemek istediğimiz nesnein YAPISI ile ilgili şeyler validationdur.
            //validation çalıştırma..


            //hep soyut sınıfları kullanıyorum.somut sınıflar zaten soyutlardan referans alıyor.
            //İş kodları.

           
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }



        //Data Accesi çağırmam gerekiyor.
        public IDataResult<List<Product>>GetAll()
        {
            if (DateTime.Now.Hour == 01)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            //İş kodları
            //ProductDaldaki getall ı çağırdık.
            return new DataResult<List<Product>>( _productDal.GetAll(),true,Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return  new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return  new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return  new SuccessDataResult<List<Product>>(_productDal.
                GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            //şuanlık iş kuralım yok direk yazıyoruz
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        
    }
}
