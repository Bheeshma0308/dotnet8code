using ExcellenceQuest.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class KeyPerformanceIndex : ISoftDelete
    { 
        public KeyPerformanceIndex()
        {
            BadgeConfigurations = new HashSet<BadgeConfiguration>();
            EmployeeAchievements = new HashSet<EmployeeAchievement>();
            KPISuccessCriteria = new HashSet<KPISuccessCriterion>();
        }
        [Required]
       
        public int Id { get; set; }

       
        public string Name { get; set; }

        public string Description { get; set; }

        public int StreamId { get; set; }

        public DateTime CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public long ModifiedBy { get; set; }

        public int SubCompetencyId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Stream Stream { get; set; }
        public virtual SubCompetency SubCompetency { get; set; }
        public virtual ICollection<BadgeConfiguration> BadgeConfigurations { get; set; }
        public virtual ICollection<EmployeeAchievement> EmployeeAchievements { get; set; }
        public virtual ICollection<KPISuccessCriterion> KPISuccessCriteria { get; set; }
    }
}
