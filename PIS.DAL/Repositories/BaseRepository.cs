using Microsoft.EntityFrameworkCore;
using University.DAL.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace University.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        
        public DbSet<T> CurrentSet { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            CurrentSet = _context.Set<T>();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await CurrentSet.FindAsync(id);

            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            CurrentSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync() 
            => await CurrentSet.ToListAsync();

        public async Task<T> GetAsync(int id) 
            => await CurrentSet.FindAsync(id);

        public async Task<int> InsertAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await CurrentSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync() 
            => await _context.SaveChangesAsync();

        public async Task<int> UpdateAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            CurrentSet.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
