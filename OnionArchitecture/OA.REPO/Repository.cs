using Microsoft.EntityFrameworkCore;
using OA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.REPO
{
                                //using System.Collections.Generic;
    public class Repository<T> : IRepository<T> where T : BaseEntity //using OA.DATA;
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;  //using Microsoft.EntityFrameworkCore;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();

        }

        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();  //using System.Linq;
        }

        public void Insert(T entity)
        {

            if (entity == null)
            {
                //throw new ArgumentNullException("entity");
                Console.WriteLine(entity);
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
    }
}
