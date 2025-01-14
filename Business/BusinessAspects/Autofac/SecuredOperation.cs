﻿using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{
    //JWT İÇİN SECUREDOPERATİON...
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;//[SecuredOperation("admin,Product.add,ADMİN")] DİYE KULLANIYORUZ BURADAKİ VİRGÜLLE AYIRDIĞIMIZ CLAİMLER ARRAY'E ATARAK ROLES OLUŞTURUYOR
        private IHttpContextAccessor _httpContextAccessor;//HER İSTEK İÇİN HTTPCONTEXT OLUŞUR

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');//ROLLERİMİZ VİRGÜLLE AYIRARAK KULLANICAZ
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)//CLAİMİ VARSA HATA VERME
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
