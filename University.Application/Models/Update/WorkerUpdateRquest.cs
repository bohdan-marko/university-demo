namespace University.Application.Models.Update;

public class WorkerUpdateRequest
{
    public int WorkerID { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public bool IsAdmin { get; set; }
    public int WorkplaceID { get; set; }
    public WorkplaceUpdateRequest Workplace { get; set; }
    public List<JobUpdateRequest> Jobs { get; set; }
}
