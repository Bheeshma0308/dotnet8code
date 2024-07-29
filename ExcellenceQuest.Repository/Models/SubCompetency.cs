using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class SubCompetency
    {
        public SubCompetency()
        {
            BadgeConfigurations = new HashSet<BadgeConfiguration>();
            Employees = new HashSet<Employee>();
            KPISuccessCriteria = new HashSet<KPISuccessCriterion>();
            KeyPerformanceIndices = new HashSet<KeyPerformanceIndex>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompetencyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Competency Competency { get; set; }
        public virtual ICollection<BadgeConfiguration> BadgeConfigurations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<KPISuccessCriterion> KPISuccessCriteria { get; set; }
        public virtual ICollection<KeyPerformanceIndex> KeyPerformanceIndices { get; set; }
    }
}
