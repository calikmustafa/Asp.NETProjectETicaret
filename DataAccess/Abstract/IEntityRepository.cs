using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //generic constraint
    //class:Referans tip olabilir
    //Her hangi bir referans tip tipinde class yazamasın sadece ve sadece Entities klasöründeki classları yazabilsin
    //bu clasların ortak özelliği ise hepsinin IEntity olması.
    //IEntity:IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //New():new'lenebilir olmalı
   public interface IEntityRepository<T> where T:class,IEntity,new()
       //T yi sınırlandırmak istiyorum herkes kafasına göre yazamasın
    {
        //Expression demek filtre ver yani e ticaret sitesinde her zaman ürünler gelmez
        //categoriye göre veya ürüne göre getir diyeceğiz.Expression p=>p......
        List<T> GetAll(Expression<Func<T,bool>>filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //Ürünleri category göre listelicek
        
    }
}
