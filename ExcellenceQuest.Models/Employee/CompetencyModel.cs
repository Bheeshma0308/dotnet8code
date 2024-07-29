namespace ExcellenceQuest.Models.Employee
{
    using ExcellenceQuest.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CompetencyModel :BaseObject
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
