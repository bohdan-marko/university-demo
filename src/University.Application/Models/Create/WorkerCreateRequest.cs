namespace University.Application.Models.Create;

public class WorkerCreateRequest
{
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public bool IsAdmin { get; set; }
    public int WorkplaceID { get; set; }
}
