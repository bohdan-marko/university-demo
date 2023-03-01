namespace University.Application.DTO;

public class WorkplaceDto
{
    public int WorkplaceID { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string City { get; set; }
    public List<WorkerDto> Workers { get; set; }
}
