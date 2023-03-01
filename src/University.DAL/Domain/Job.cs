using University.DAL.Domain.Base;
using University.DAL.Domain;

namespace University.DAL.Domain;

public class Job : BaseEntity
{
    public int JobID { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public List<Worker> Workers { get; set; } = new();
}
