using LanisterPay.CustomAttributes;
using LanisterPay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.EntityDto
{
    public class TransactionEntityDto
    {
        public TransactionEntityDto() { }
        public int Id { get; set; }
        public double Amount {get; set;
        }
        public string Currency { get; set; }
        public string CustomerEmail { get; set; }
        //[SplitInfoArrayConstraint]
        public List<SplitInfoEntityDto> SplitInfo { get; set; } = new List<SplitInfoEntityDto>();
    }
}
