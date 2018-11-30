using CakeShop.Domain.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CakeShop.Data
{
    public static class LiteDBProvider
    {
        
        private static LiteRepository GetRepository()
        {
            return new LiteRepository("cakeshop.db");
        }

        public static IList<T> GetAll<T>()
        {
            return GetRepository().Query<T>().ToList();
        }

        public static IList<T> Find<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return GetRepository().Fetch(predicate, collectionName);
        }

        public static T Get<T>(Guid id) where T : ShopItem
        {
            return GetAll<T>().FirstOrDefault<T>(x => x.Id == id);
        }

        public static void Update<T>(T item) where T : ShopItem
        {
            GetRepository().Update(item);
        }

        public static void Create<T>(T item) where T : ShopItem
        {
            GetRepository().Insert(item);
        }

        public static void Delete<T>(Guid id) where T : ShopItem
        {
            GetRepository().Delete<T>(x => x.Id == id);
        }
    }
}
