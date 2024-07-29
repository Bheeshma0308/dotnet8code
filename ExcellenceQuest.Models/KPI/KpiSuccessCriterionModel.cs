namespace ExcellenceQuest.Models.KPI
{
    using ExcellenceQuest.Models.Common;
    using ExcellenceQuest.Models.Employee;
    using System.ComponentModel.DataAnnotations;

    public class KPISuccessCriteriaModel : BaseObject
    {
        [Required]
        public int GradeId { get; set; }

        [Required]
        public int KpiId { get; set; }

        
        [Required(ErrorMessage = "This field is mandatory")]
        [Range(1, 5, ErrorMessage = "The weightage must be a positive integer between 1 and 5.")]


        public long Weightage { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int SubCompetencyId { get; set; }
        [Required(ErrorMessage ="This field is mandatory")]
        [Range(1, 5, ErrorMessage = "The weightage must be a positive integer between 1 and 5.")]

        public int SuccessCriteria { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public  GradeModel Grade { get; set; }
        public  KeyPerformanceIndexModel Kpi { get; set; }
        public  SubCompetencyModel SubCompetency { get; set; }
    }
}
