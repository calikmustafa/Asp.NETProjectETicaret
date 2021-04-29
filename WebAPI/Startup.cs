using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //biz bu mimariyi AutoFac e taþýyacaðýz.Net projelerinde kendi içinde  (Ioc)singletonu kendisi yapýyor ve altyapý sunuyor.
            //AOP yapýcaz o yüznde autofac kullanýyoruz çünkü bize aop imkaný sunuyor.aop bir metodun önünde sonunda hata verdiðinde çalýþan kod parçacýklarýný aop mimarisi ile yazýcaz.
            services.AddControllers();
            services.AddSingleton<IProductService,ProductManager>();//bana arka planda bir referans oluþtur IoC container.Birisi senden consturactor da  IProductService isterse arka planda productmanager oluþtur onu ver demek.
            services.AddSingleton<IProductDal, EfProductDal>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
