using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    //BU CLASS SAYESİNDE STARTUP DOSYASINDA DEĞİL DE BURAYA ÇÖZÜMLEYECEĞİZ.
    //BU CLASS GENEL BAĞIMLILIKLARIMIZI OLUŞTURACAĞIMIZ CLASDIR
    //BUSİNESS DEKİ DependencyResolvers'DAN FARKLI OLARAK GENEL BAĞIMLILIKLARIMIZI BURAYA YAZACAĞIZ
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<Stopwatch>();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
