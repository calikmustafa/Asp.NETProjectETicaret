using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //Interceptor araya girmek demek yani methodun başında sonunda nerede araya gireceğini belirticek


    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //git clasın attributlarını oku
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            //git methodun attributlarını oku
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            
            //bunların çalışma sırasını da Prioriyt çalışma önceliğine göre sırala
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
