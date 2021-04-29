using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    //api bizim frontend için gerekli kodları yazdığımız bağlantı kurduğumuz yerdir.
    //bize yapılabilecek istekleri koduluyoruz.Bu controller de.
    //Controller bizim sistemizi kullanacak angular,mobiller uygulamaları(categori istedi mesela) burda bize istekde bulunuyorlar.

    [Route("api/[controller]")]//BU İSTEĞİ YAPARKEN İNSANLAR BU CONTROLLERE NASIL ULAŞTIN APİ YAZSIN ve controllerin ismini yazsın.
    [ApiController]//ATTRIBUTE:BİR CLASS İLE İLGİLİ BİLGİ VERMEKTİR.PRODUCTSCONTROLLER BİR CONTROLLER GÖREVİ GÖRÜCEK DEMEKTİR
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        //controllere diyorki sen bir IProductservice bağımlısısın
        //bizim işimiz tamamen efproductdal ile alakalı ve o yüzden bu class referansını tutuyor.
        //somut sınıf üzerinden hiçbir zaman gitmeyeceğiz hep soyut sınıflardan gitmek zorundayız.
        //ama bize productmanager ihtiyacımız var new ProductManager yazamayız o zaman bağımlı oluruz
        //somut referans gerekiyor bize
        //IoC Container--Liste gibi düşün ben listeye new ProdcutManager,new EfProductDal referanslar koyayım ondan sonra kim ihtiyaç duyuyorsa kullansın bunu startup dosyasında belirticeksın
        public ProductsController(IProductService productService)//bana bir tane Iproductservice implemente eden bir manager ver demek
        {
            _productService = productService;
        }

        [HttpGet("getall")]//DATAYI GÖSTER GETİR DEMEK ve sorgusu  https://localhost:44322/api/products/getall(api controllerinde getalli çalıştır demek) şeklinde
        //200 ok bize başarılı demektir
        public IActionResult GetAll()
        {
            //Dependency chain --
            //var result = _productService.GetAll();//result bize IDataResult,ISuccessResult gibi clasların içerisindeki data,message fielderini döndürüyor
            //if (result.Success)//işlem başarılı mı true ise yani saat 02 ise prodcutmanager de ki getall da kodladım
            //{
            //    return Ok(result.Data);//bu şekilde postman de sağ üstte 200 de data yani okey sıkıntı yok
            //}
            //return BadRequest(result.Message);//eğer yanlışsa 400 badrequest göreceksin ve hata


            var result = _productService.GetAll();//result bize IDataResult,ISuccessResult gibi clasların içerisindeki data,message fielderini döndürüyor
            if (result.Success)//eğer resultun success durumu true ise şuan 01 verdim false dönücek
            {
                return Ok(result);
            }
            return BadRequest(result);//false döndü hem data gelicek hem de badreguest olduğunu belirticek.  
        }
        //iki tane get operasyonumuz olduğu için isim verdik 
        [HttpGet("getbyid")]//https://localhost:44322/api/products/getbyid?id=1 sorgusu bu şekilde
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        //https://localhost:44322/api/products/add
        //POST REGUESTLER DE BEN SANA DATA VERİCEM ONU AL SİSTEMİNE EKLE DEMEK.
        //POSTMANDA BODYE GEL ROWA TIKLA TEXT DEN DE JSONU SEÇ ve aşağıdakini ekle
        //{
        //"categoryId": 1,
        //"productName": "Bardak",
        //"unitsInStock": 15,
        //"unitPrice": 25
        //}
        //arayüzden bana verdiğin ürünü ekle diyoruz.
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);//result IResultdır yani data yok
            if (result.Success==true)//ürün eklendiyse
            {
                return Ok(result);//resultun hespsini getir yani messagı ve succesi getir ve ssağ üste 200 ok yazıcak
            }
            return BadRequest(result);

        }

    }
}
