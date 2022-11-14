using PIS.DAL.Models;
using PIS.DAL.Repositories;
using University.WebAPI.Services.Abstract;

namespace University.WebAPI.Services
{
    public class WorkerService : IBaseService<Worker>
    {
        private readonly IBaseRepository<Worker> _repository;

        public WorkerService(IBaseRepository<Worker> repository)
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

        public async Task<Worker> Get(int id)
        {
            try
            {
                var worker = await _repository.GetAsync(id);
                return worker;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Worker>> GetAll()
        {
            try
            {
                var workers = await _repository.GetAllAsync();
                return workers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Insert(Worker entity)
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

        public async Task<bool> Update(Worker entity)
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
