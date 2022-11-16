namespace University.Application.Models.Update;

public class JobUpdateRequest
{
    public int JobID { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public List<WorkerUpdateRequest> Workers { get; set; }
}
