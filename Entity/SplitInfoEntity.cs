using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.Entity
{
    public class SplitInfoEntity
    {
        public SplitInfoEntity() { }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private Guid SplitInfoEntityId { get; set; }
        public string SplitType { get; set; }
        public double SplitValue { get; set; }
        public string SplitEntityId { get; set; }
        public Guid TransactionEntityId { get; set; }
    }
}
