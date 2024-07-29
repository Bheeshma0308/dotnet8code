namespace ExcellenceQuest.Models.Employee
{
    using ExcellenceQuest.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SubCompetencyModel :BaseObject
    {
       
        public string Name { get; set; }
        public int CompetencyId { get; set; }
    }
}
