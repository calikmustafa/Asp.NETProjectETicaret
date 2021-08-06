using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    // IServiceCollection BİZİM SERVİS BAĞIMLIKLARIMIZI EKLEDİĞİMİZ KOLEKSİYONDUR
    //BU CLASS YARIN ÖBÜR GÜN ICOREMODULDEN VAZGEÇİP BAŞKA BİR MODULE EKLEDİĞİMİZDE SİSTEM BOZULMAMASI İÇİN
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection,ICoreModule[] modules)//THİS KULLANDIK ÇÜNKÜ EXTENSİONS YAPIYORUZ
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);//BİRDEN FAZLA MODULE EKLEYEBİLEYECEĞİMİZİ KODLADIK
            }
            return ServiceTool.Create(serviceCollection);
            //CORE KATMANIDA DAHİL OLMAK ÜZERE EKLEYECEĞİMİZ BÜTÜN INJECTİONLARI TOPLAYABİLEYECEĞİMİZ BİR ALANA DÖNÜŞTÜ.
        }
    }
}
