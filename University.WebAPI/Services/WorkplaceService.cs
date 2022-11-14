using PIS.DAL.Models;
using PIS.DAL.Repositories;
using University.WebAPI.Services.Abstract;

namespace University.WebAPI.Services
{
    public class WorkplaceService : IBaseService<Workplace>
    {
        private readonly IBaseRepository<Workplace> _repository;

        public WorkplaceService(IBaseRepository<Workplace> repository)
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

        public async Task<Workplace> Get(int id)
        {
            try
            {
                var workplace = await _repository.GetAsync(id);
                return workplace;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Workplace>> GetAll()
        {
            try
            {
                var workplaces = await _repository.GetAllAsync();
                return workplaces;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Insert(Workplace entity)
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

        public async Task<bool> Update(Workplace entity)
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
