using LanisterPay.CalculateAmounts;
using LanisterPay.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.Entity
{
    public class TransactionEntity
    {
        public TransactionEntity() { }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private Guid TransactionEntityId { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public double Amount { get; set; }

        public List<SplitInfoEntity> splitInfo { get; set; } = new List<SplitInfoEntity>();
        public async Task<SplitBreakdown> CalculateAmount(SplitInfoEntity splitInfo)
        {
            double splitAmount;

            splitAmount =CalculateAmount(splitInfo.SplitType, splitInfo.SplitValue, Amount);
            if (splitAmount < 0)
            {
                throw new Exception("Split Amount cannot be lesser than 0");
            }
            if (Amount<splitAmount)
            {
                throw new Exception("Insufficient Balance");
            }
            Amount -= splitAmount;
            return new SplitBreakdown(splitInfo.SplitEntityId, splitAmount);
            
        }

        public async Task<SplitBreakdown> CalculateAmount(SplitInfoEntity splitInfo, int totalRatio,double ratioBalance)
        {
            double splitAmount;
            splitAmount = CalculateAmountByRatio(splitInfo.SplitValue, totalRatio,ratioBalance);
            if (splitAmount < 0)
            {
                throw new Exception("Split Amount cannot be lesser than 0");
            }
            if (Amount < splitAmount)
            {
                throw new Exception("Insufficient Balance");
            }
            Amount -= splitAmount;
            return new SplitBreakdown(splitInfo.SplitEntityId, splitAmount);
        }

        public double GetBalance()
        {
            return Amount;
        }

        private double CalculateAmount(string splitType, double splitValue, double balance)
        {
            if (splitType.ToLower() == "flat")
            {
                return splitValue;
            }
            else if (splitType.ToLower() == "percentage")
            {
                return (balance * splitValue) / 100;
            }

            throw new Exception("Unknown SplitType");
        }
        private double CalculateAmountByRatio(double splitValue, int totalRatio, double ratioBalance)
        {
            return (splitValue / totalRatio) * ratioBalance;
        }
    }
}
