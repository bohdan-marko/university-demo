namespace University.Application.DTO;

public class JobDto
{
    public int JobID { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public List<WorkerDto> Workers { get; set; }
}
