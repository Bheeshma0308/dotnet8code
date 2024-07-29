namespace ExcellenceQuest.Models.Employee
{
    using ExcellenceQuest.Models.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class EmployeeModel :BaseObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public int GradeId { get; set; }
        [Required]
        public int CompetencyId { get; set; }
        [Required]
        public int SubCompetencyId { get; set; }
        [Required]
        public int StreamId { get; set; }
        [JsonIgnore]
        public CompetencyModel Competency { get; set; }
        [JsonIgnore]
        public GradeModel Grade { get; set; }
        [JsonIgnore]
        public SubCompetencyModel SubCompetency { get; set; }
        public List<EmployeeKpiViewModel> KpiDetails { get; set; }
        public List<BadgeDetailsModel> BadgeDetails { get; set; }
/*
        public List<EmployeeAchievementModel> EmployeeAchievementEmployees { get; set; }*/
    }
}
