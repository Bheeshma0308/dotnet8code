using ExcellenceQuest.Models.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class KPISuccessCriterion : ISoftDelete
    {
        public long Id { get; set; }
        public int GradeId { get; set; }
        public int KpiId { get; set; }
        public long Weightage { get; set; }
        public int Rating { get; set; }
        public int SubCompetencyId { get; set; }
        public int SuccessCriteria { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual KeyPerformanceIndex Kpi { get; set; }
        public virtual SubCompetency SubCompetency { get; set; }
    }
}
