using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
   public interface ICacheManager
    {
        T Get<T>(string key);//CACHEDEN VERİ GETİRİRKEN HANGİ TİP GETİRECEĞİNİ SEN BELİRLE
        void Add(string key, object value,int duration);//CACHE VERİ EKLEYEBİLİRİM
        object Get(string key);
        bool IsAdd(string key);//BUNU CACHEDEN Mİ GETİRELİM YOKSA VERİTABANINDA MI GETİRELİM CACHEDE VAR MI??
        void Remove(string key);
        void RemoveByPattern(string pattern);//MESELA İSMİNDE GET OLANLARI UÇUR VEYA CATEGORYOLANLARI UÇUR

    }
}
