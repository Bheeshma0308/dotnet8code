namespace ExcellenceQuest.Models.Employee
{
    using ExcellenceQuest.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class EmployeeAchievementModel :BaseObject
    {
       
        [Required]
        public long EmployeeId { get; set; }
        [Required]
        public int KpiId { get; set; }

        [MaxLength(350)]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime AchievedOn { get; set; }
        public virtual KeyPerformanceIndexModel Kpi { get; set; }
    }
}
