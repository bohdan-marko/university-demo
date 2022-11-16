using System.Collections.Generic;
using University.DAL.Contracts.Base;

namespace University.DAL.Models
{
    public class Job : BaseEntity
    {
        public int JobID { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }
}
