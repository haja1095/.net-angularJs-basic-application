using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.DAL
{
    public abstract class Repository<T> where T : class
    {
        private DbSet<T> table = default(DbSet<T>);
        private DbContext entitys = default(DbContext);
        public Repository(GSTModel gstmodel)
        {
            entitys = gstmodel;
            table = entitys.Set<T>();
        }

        //Methods
        public IEnumerable<T> GetAll()
        {
            return table.AsEnumerable<T>();
        }
        public IEnumerable<T> GetAll(Expression<Func<T,bool>> predicate)
        {
            return table.Where(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return table.FirstOrDefault(predicate);
        }

        public T Add(T entity)
        {
            table.Add(entity);
            entitys.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            entitys.Entry(entity).State = EntityState.Modified;
            entitys.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            entitys.Entry(entity).State = EntityState.Modified;
            entitys.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddBulk(IEnumerable<T> entrys)
        {
            table.AddRange(entrys);
            entitys.SaveChanges();
            return entrys;
        }

        public IEnumerable<T> RemoveBulk(IEnumerable<T> entrys)
        {
            table.RemoveRange(entrys);
            entitys.SaveChanges();
            return entrys;
        }
    }
}
