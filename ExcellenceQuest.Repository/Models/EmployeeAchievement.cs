using ExcellenceQuest.Models.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class EmployeeAchievement : ISoftDelete
    {
        public int Id { get; set; }
        public long EmployeeId { get; set; }
        public int KpiId { get; set; }
        public string Description { get; set; }
        public DateTime AchievedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual KeyPerformanceIndex Kpi { get; set; }
    }
}
