using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Soyut sınıfdan bağlantı kurduk
        IProductDal _productDal;
        //productmanager IProductdal hariç ıCategorydalı enjekte edemez.


        //sana yönetim dediki kategory si 15 den fazla olamaz bir ürünün
        //burda ICategoryService kullanmamız gerekir
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)//birisi senden IProductDal isterse sen ona efProductDalı ver diyoruz.
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }



        //loglama yapılan operasyonların bir yerde kaydını tutmaktır.kim nerede ne zaman  bir ürün ekledi.
        
        //JWT(Json Web Token)
        //Claim->admin veya product.add yazdığımız kelimeler hep claimdir.
        //Hashing= kullanıcının şifresini ve kullanıcı adını veritabanın da açık açık tutmayız gizleriz buna hashing denir
        //mesela 12341234 olsun şifre BDX3-5FDGHD algoritmaya göre bu şekilde veritabanın da tutarız gizli olması için
        //Salting-> Kullanıcı aB1 girdi mesela biz bu parolayı güçlendirirz(aB112Bc) gibi buna salting deniz
        
        [SecuredOperation("admin,product.add")]//YETKİNİZ YOK DER OPERATİON CLAİMSLER TABLOSUNDA ADMİN,PRODUCTS.ADD GİBİ OLMASI GEREK
        [ValidationAspect(typeof(ProductValidator))]//add metodunu doğrula productvalitora göre

        //add methodunu çalıştırmadan önce program bakıyorum yukarıya bir attribute var önce onu çalıştırıyor.
        [CacheRemoveAspect("IproductService.Get")]
        public IResult Add(Product product)
        {
            //validation-- doğrulama eklemeye çalıştığın varlığı nesnenin bu iş kurallarına uygun oluğ olmadığını kontrol ediyor.
            //eklemek istediğimiz nesnein YAPISI ile ilgili şeyler validationdur.
            //validation çalıştırma..


            //hep soyut sınıfları kullanıyorum.somut sınıflar zaten soyutlardan referans alıyor.
            //İş kodları.

            //bir kategoride en fazla 10 ürün olabilir
           IResult result= BusinessRules.Run(CheckIfProductNameExists(product.ProductName),CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());
            if (result != null)//kurala uymayan  bir durum oluşmuşsa
            {
                return result;

            }
            return new SuccessResult(Messages.ProductAdded);

        }



        //CACHE 
        [CacheAspect]
        //DAHA ÖNCE BİR KULLANICI GET ALL'I ÇAĞIRDIĞIN DA VEYA GETBYID'I ÇAĞIRDIĞINDA O DATA DEĞİŞMEDİYSE BİR DAHA BİR DAHA VERİTABANINA GİTMESİNE GEREK YOK
        //DİĞER TÜM İSTEKLER DE DATAYI CACHE'DEN GETİRMESİNİ İSTİYORUZ VERİTABANINA GİTMESİNE GEREK KALMIYOR.
        // CACHE YAPILMASINI İSTEDİĞİMİZ DATAYI BELLEKTE KEY,VALUE DİYE TUTUYORUZ
        //KEY CACHEYE VERDİĞİMİZ İSİMDİR ÖRNEK BUSİNESS.CONCRETE.PRODUCTMANAGER.GETALL
        public IDataResult<List<Product>>GetAll()
        {
            if (DateTime.Now.Hour == 1)
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

        [CacheAspect]
        //***[PerformanceAspect(5)]//BU METODUN ÇALIŞMASI 5 SANİYEYİ GEÇERSE BENİ UYAR
        //AMA BUNU CORE INTERCEPTORDEKİ YERE KOYARSAK SİSTEMDE HER ŞEYİ TAKİP EDER
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

        [CacheRemoveAspect("IproductService.Get")]//IProductService İÇERİSİNDE GET OLAN KEYLERİ İPTAL ET DEMEK
        public IResult Update(Product product)
        {
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId);
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}
            //throw new NotImplementedException();
            _productDal.Update(product);
            return new SuccessResult("ürün güncellendi");
        }
        //private başlamamızın sebebi bu metodun sadece bu clasın içerisinde kullanmamız gerekicek

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)//burası iş kodlarını yazdığımız yer categoryid 15 dan fazla olamaz diye bize söylediler
            //bu kodları metod içerisine almalıyız ve bunu da sadece burda kullanacağımız için private yaptım.temiz kod yazmak zorundayız
            //bu kod bize IResult Döndürüyor.
        {
            //select count(*)from products where categoryıd=1
            var result = _productDal.GetAll(c=>c.CategoryId==categoryId).Count;
            if (result>=15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        //Aynı isimde ürün eklemenez bize bu işlem sonucu IResult yani error mü successmi döndürsün diyeIResult kullandık.
        private IResult CheckIfProductNameExists(string productNName)
        {
            var result = _productDal.GetAll(p=>p.ProductName==productNName).Any();//böyle bir sorgu var mı? bool döndürüyor.
            if (result)
            {
                new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        //bu kuralı categoryide neden yaza
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        //***[TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            throw new Exception("");
           
        }
    }
}
