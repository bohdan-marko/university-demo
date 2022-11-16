using System.Collections.Generic;
using University.DAL.Contracts.Base;

namespace University.DAL.Models
{
    public class Worker : BaseEntity
    {
        public int WorkerID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }
        public int WorkplaceID { get; set; }
        public Workplace Workplace { get; set; }
        public List<Job> Jobs { get; set; } = new();
    }
}
