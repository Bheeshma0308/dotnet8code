using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class Grade
    {
        public Grade()
        {
            BadgeConfigurations = new HashSet<BadgeConfiguration>();
            Employees = new HashSet<Employee>();
            KPISuccessCriteria = new HashSet<KPISuccessCriterion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<BadgeConfiguration> BadgeConfigurations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<KPISuccessCriterion> KPISuccessCriteria { get; set; }
    }
}
