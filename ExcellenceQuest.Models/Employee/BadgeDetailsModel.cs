using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcellenceQuest.Models.Employee
{
    public class BadgeDetailsModel
    {
        public string KpiName { get; set; }

        public int BadgeId { get; set; }

        public string BadgeName { get; set; }

        public int KPIsRequired { get; set; }

        public string NextBadge { get; set; }
    }
}
