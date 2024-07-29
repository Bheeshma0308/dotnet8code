using ExcellenceQuest.Models.Common;
using System;
namespace ExcellenceQuest.Repository.Models
{
    public partial class BadgeConfiguration : ISoftDelete
    {
        public int Id { get; set; }
        public int BadgeId { get; set; }
        public int KpiId { get; set; }
        public int SubCompetencyId { get; set; }
        public int GradeId { get; set; }
        public int KpiCriteria { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Badge Badge { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual KeyPerformanceIndex Kpi { get; set; }
        public virtual SubCompetency SubCompetency { get; set; }
    }
}
