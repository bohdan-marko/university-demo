namespace University.Application.DTO;

public class WorkerDto
{
    public int WorkerID { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public bool IsAdmin { get; set; }
    public int WorkplaceID { get; set; }
    public WorkplaceDto Workplace { get; set; }
    public List<JobDto> Jobs { get; set; }
}
