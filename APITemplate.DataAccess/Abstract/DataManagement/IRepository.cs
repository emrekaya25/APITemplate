using APITemplate.Entity.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APITemplate.DataAccess.Abstract.DataManagement
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<EntityEntry<T>> AddAsync(T entity);
        Task UpdateAsync (T entity);
        Task DeleteAsync (T entity);
        Task<T> GetAsync(Expression<Func<T,bool>>Filter,params string[] IncludeProperties);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> Filter = null, params string[] IncludeProperties);

    }
}
