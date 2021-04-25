using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace Core.DataAccess.EntityFramework
{
    //yarın öbür gün Başka bir database ile çalışmak istediğin de kullanabilirsin.
    //artık efcustomerdal için veya efproductdal için teker teker kod yazmayacağız.
   public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
       where TContext:DbContext,new()
    {
        public void Add(TEntity  entity)
        {
            //Kullanmayı bıraktıktan sonra bellekten silinicek ve daha performanslı çalışıcak.
            using (TContext context = new TContext())
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

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
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

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //DbSet deki Product tablosuna yerleş listeye çevir ve bize ver
                return filter == null ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
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
