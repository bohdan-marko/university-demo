using PIS.DAL.Models;
using PIS.DAL.Repositories;
using University.Application.Services.Abstract;

namespace University.Application.Services
{
    public class JobService : IBaseService<Job>
    {
        private readonly IBaseRepository<Job> _repository;

        public JobService(IBaseRepository<Job> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var affectedRows = await _repository.DeleteAsync(id);
                return affectedRows > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Job> Get(int id)
        {
            try
            {
                var job = await _repository.GetAsync(id);
                return job;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            try
            {
                var jobs = await _repository.GetAllAsync();
                return jobs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Insert(Job entity)
        {
            try
            {
                var affectedRows = await _repository.InsertAsync(entity);
                return affectedRows > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Job entity)
        {
            try
            {
                var affectedRows = await _repository.UpdateAsync(entity);
                return affectedRows > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
