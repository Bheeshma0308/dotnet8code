using ExcellenceQuest.Models.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellenceQuest.Repository.Models
{
    public partial class Employee : ISoftDelete
    {
        public Employee()
        {
            EmployeeAchievements = new HashSet<EmployeeAchievement>();
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public int GradeId { get; set; }
        public int CompetencyId { get; set; }
        public int SubCompetencyId { get; set; }
        public int StreamId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Competency Competency { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual SubCompetency SubCompetency { get; set; }
        public virtual ICollection<EmployeeAchievement> EmployeeAchievements { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
