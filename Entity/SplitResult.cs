using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.Entity
{
    public class SplitResult
    {
        public int ID { set; get; }
        public double Balance { get; set; }
        public IList<SplitBreakdown> SplitBreakdown { get; set; } = new List<SplitBreakdown>();
        public SplitResult(int id=0)
        {
            ID = id;
        }
    }
}
