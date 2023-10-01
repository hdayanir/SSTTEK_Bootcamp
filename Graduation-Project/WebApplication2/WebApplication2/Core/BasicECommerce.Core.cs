using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain;

namespace WebApplication2.Core
{
    public class BasicECommerceCore : ControllerBase
    {
        public interface IRepository<TEntity> where TEntity : class
        {
            TEntity GetById(int id);
            void Add(TEntity entity);
            void Update(TEntity entity);
            void Remove(TEntity entity);
            List<Product> GetProductsByCategoryId(int categoryId);
        }

        public interface IMapper<TSource, TDestination>
        {
            TDestination Map(TSource source);
            List<TDestination> MapList(List<TSource> sourceList);
        }
    }
}