using ExcellenceQuest.Models.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class Competency  : ISoftDelete
    {
        public Competency()
        {
            Employees = new HashSet<Employee>();
            SubCompetencies = new HashSet<SubCompetency>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<SubCompetency> SubCompetencies { get; set; }
    }
}
