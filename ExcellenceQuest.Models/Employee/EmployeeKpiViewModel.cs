using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExcellenceQuest.Models.Employee
{
    public class EmployeeKpiViewModel
    {

        public int KpiId { get; set; }
        public string KpiName { get; set; }
        public int KpiCount { get; set; }
        public int KpiCriteria { get; set; }
        
    }
}
