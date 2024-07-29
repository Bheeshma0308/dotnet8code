namespace ExcellenceQuest.Models.Badge
{
    using ExcellenceQuest.Models.Common;
    using ExcellenceQuest.Models.Employee;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class BadgeConfigurationModel :BaseObject
    {
        [Required]
        [Range(1, int.MaxValue-1, ErrorMessage = "The BadgeID must be a positive integer.")]
        public int BadgeId { get; set; }
        [Required]
        [Range(1, int.MaxValue - 1, ErrorMessage = "The KpiID must be a positive integer.")]
        public int KpiId { get; set; }
        [Required]
        [Range(1, int.MaxValue - 1, ErrorMessage = "The SubCompetencyId must be a positive integer.")]
        public int SubCompetencyId { get; set; }
        [Required]
        [Range(1, int.MaxValue - 1, ErrorMessage = "The GradeID must be a positive integer.")]
        public int GradeId { get; set; }
        [Required]
        [Range(1, int.MaxValue - 1, ErrorMessage = "The KpiCriteria must be a positive integer.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid input. Please enter only numbers.")]
        public int KpiCriteria { get; set; }
        public string Description { get; set; }

        
        public string BadgeName { get; set; }
      
        public  BadgeModel Badge { get; set; }
        [JsonIgnore]
        public  GradeModel Grade { get; set; }
        [JsonIgnore]
        public  KeyPerformanceIndexModel Kpi { get; set; }
        [JsonIgnore]
        public  SubCompetencyModel SubCompetency { get; set; }
    }
}
