using AutoMapper;
using University.DAL.Domain;
using University.DAL.Repositories;
using University.Application.DTO;
using University.Application.Models.Create;
using University.Application.Models.Update;
using University.Application.Services.Abstract;

namespace University.Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IBaseRepository<Worker> _repository;
        private readonly IMapper _mapper;

        public WorkerService(IBaseRepository<Worker> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Delete(int id)
        {
            var affectedRows = await _repository.DeleteAsync(id);
            return affectedRows > 0;
        }

        public async Task<WorkerDto> Get(int id)
        {
            var worker = await _repository.GetAsync(id);
            return _mapper.Map<WorkerDto>(worker);
        }

        public async Task<IEnumerable<WorkerDto>> GetAll()
        {
            var workers = await _repository.GetAllAsync();
            return _mapper.Map<List<WorkerDto>>(workers);
        }

        public async Task<bool> Insert(WorkerCreateRequest entity)
        {
            var affectedRows = await _repository.InsertAsync(_mapper.Map<Worker>(entity));
            return affectedRows > 0;
        }

        public async Task<bool> Update(WorkerUpdateRequest entity)
        {
            var affectedRows = await _repository.UpdateAsync(_mapper.Map<Worker>(entity));
            return affectedRows > 0;
        }
    }
}
