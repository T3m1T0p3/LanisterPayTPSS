using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.EntityDto
{
    public class SplitInfoEntityDto
    {
        public SplitInfoEntityDto() { }
        public string SplitType { get; set; }
        public double SplitValue { get; set; }
        public string SplitEntityId { get; set; }
    }
}
