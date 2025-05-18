using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, ISpecification<T> spec);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task<T> DeleteAsync(int id);
    }
}

