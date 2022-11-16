using Microsoft.EntityFrameworkCore;
using University.DAL.Contracts.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace University.DAL.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public DbSet<T> CurrentSet { get; set; }
        
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
