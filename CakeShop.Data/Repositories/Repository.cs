using CakeShop.Data;
using CakeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CakeShop.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : ShopItem
    {
        
        public IList<T> All()
        {
            return LiteDBProvider.GetAll<T>();
        }

        public T GetById(Guid id)
        {
            return LiteDBProvider.GetAll<T>().FirstOrDefault(x => x.Id == id);
        }

        public bool Add(T item)
        {
            if (LiteDBProvider.GetAll<T>().Any(i => item.Id == i.Id))
            {
                return false;
            }
            LiteDBProvider.Create(item);
            return true;
        }

        public bool Update(T item)
        {
            if (!Delete(item.Id))
            {
                return false;
            }
            LiteDBProvider.Create(item);
            return true;
        }

        public bool Delete(Guid id)
        {
            var foundItem = GetById(id);
            if (foundItem == default(T))
            {
                return false;
            }
            LiteDBProvider.Delete<T>(foundItem.Id);
            return true;
        }
    }
}
