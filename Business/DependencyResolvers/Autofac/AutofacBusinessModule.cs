using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    //startup.cs içerisine IProductDal istendiğinde ProductManager ver diye kod yazmıştık
    //artık bu tür kodları burada yazacağız.
    //bizim yerimize newleyecek.
   public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //registertype services.addsingleteon 'a karşılık gelir
            //biri senden IProdutservice isterse ProductManager örneği ver demek
            //singleInstance tek bir instance oluşturuyor data tutmaz.
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            //autofac kullanmamızın sebebi çalıştırınca attributu var mı diye bakıyor
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
