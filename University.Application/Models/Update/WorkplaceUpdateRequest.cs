namespace University.Application.Models.Update;

public class WorkplaceUpdateRequest
{
    public int WorkplaceID { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string City { get; set; }
    public List<WorkerUpdateRequest> Workers { get; set; }
}
