namespace ExcellenceQuest.Models
{
    using ExcellenceQuest.Models.Employee;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TopScorerModel
    {
        public string EmployeeName { get; set; }
        public long EmployeeId { get; set; }
        public string KpiName { get; set; }
        public int KpiId { get; set; }
        public int StreamId { get; set; }
        public string StreamName { get; set; }
        public int SubCompetencyId { get; set; }
        public string  GradeName { get; set; }
        public string SubCompetencyName { get; set; }
        public DateTime AchievementDate { get; set; }

        public int Score { get; set; }
    }
}
