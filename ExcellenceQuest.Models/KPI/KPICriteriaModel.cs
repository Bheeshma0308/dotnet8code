using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcellenceQuest.Models.KPI
{
    public class KPICriteriaModel
    {
        public int KpiId { get; set; }
        public int GradeID { get; set; }
        public string KpiName { get; set; }
        public long Weightage { get; set; }

    }
}