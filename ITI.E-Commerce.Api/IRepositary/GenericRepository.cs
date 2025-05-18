using System;
using ITI.E_Commerce.Api.Interfaces;
using ITI.E_Commerce.Api.Specifications;
using ITI.E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ITI.E_Commerce.Api.IRepositary
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MyContext _context;

        public GenericRepository(MyContext context)  
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<T> GetByIdAsync(int id, ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();

        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var local = _context.Set<T>().FirstOrDefault(entry => entry.Id.Equals(id));

            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            else
            {
                return null;

            }
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }


        // Helper Method
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);

        }
    }
}

