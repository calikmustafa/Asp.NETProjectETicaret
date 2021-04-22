using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //Kullanmayı bıraktıktan sonra bellekten silinicek ve daha performanslı çalışıcak.
            using (NorthwindContext context=new NorthwindContext())
            {
                //Git database direk ekle demek
                //Referansı yakalamak demek.
                var addedEntity = context.Entry(entity);
                //eklenecek nesne
                addedEntity.State = EntityState.Added;
                //ve ekle demek.
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                //Git database direk ekle demek
                //Referansı yakalamak demek.
                var deletedEntity = context.Entry(entity);
                //eklenecek nesne
                deletedEntity.State = EntityState.Deleted;
                //ve ekle demek.
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                //DbSet deki Product tablosuna yerleş listeye çevir ve bize ver
                return filter == null ? context.Set<Product>().ToList()
                    : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //Git database direk ekle demek
                //Referansı yakalamak demek.
                var updatedEntity = context.Entry(entity);
                //eklenecek nesne
                updatedEntity.State = EntityState.Modified;
                //ve ekle demek.
                context.SaveChanges();
            }
        }
    }
}
