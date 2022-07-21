using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.Entity
{
    public class SplitBreakdown
    {
        public string SplitEntityId { get; set; }
        public double Amount { get; set; }
        public SplitBreakdown(string id,double amount = 0)
        {
            SplitEntityId = id;
            Amount = amount;
        }
    }
}
