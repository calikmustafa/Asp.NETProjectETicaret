using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    //ADAPTER PATTERN-> VAR OLAN BİR SİSTEMİ KENDİ SİSTEMİME UYARLIYORUM.
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;//MİCROSOFTUN KÜTÜPHANESİ INTERFACE
        //BUNU ÇALIŞTIRMAK İÇİN COREMODULE EKLEMEMİZ GEREK

        public MemoryCacheManager()//CORE MODULE YAZMIŞTIK HAZIR BİR INJECTION
            //CACHE GİBİ HAZIR YAPILAR DA ADDMEMORYCACHE YAZDIK ARKA PLANDA OLUŞTURDU
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();//BELLEĞE GİDİYOR IMEMORYCACHE VAR MI DİYE GİDİYOR BAKIYOR CORE MODULE VAR.
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));//NE KADAR SÜRE VERİRSENİZ CACHE DE KALACAK KODDUR
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)//BELLEKTE BÖYLE BİR CACHE DEĞERİ VAR MI?
        {
            //datayı istemiyosan out _ KULLANABİLİRSİN
            return _memoryCache.TryGetValue(key,out _);//BELLEK TE OLUĞ OLMADIĞINI KONTROL ETMEK İÇİN
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        //ÇALIŞMA ANINDA BELLEKTEN SİLİYOR->reflection
        public void RemoveByPattern(string pattern)//ona verdiğimiz patterne göre silme işlemii yapıcak
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
