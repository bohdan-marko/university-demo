using University.Application.DTO;
using University.Application.Models.Create;
using University.Application.Models.Update;

namespace University.Application.Services.Abstract;

public interface IWorkerService
{
    Task<IEnumerable<WorkerDto>> GetAll();
    Task<WorkerDto> Get(int id);
    Task<bool> Insert(WorkerCreateRequest entity);
    Task<bool> Update(WorkerUpdateRequest entity);
    Task<bool> Delete(int id);
}
