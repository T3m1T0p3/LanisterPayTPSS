using LanisterPay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.CalculateAmounts
{
    public interface ICalculateAmount
    {
        public double Calculate(string splitType, double splitValue, double amount);
        public double Calculate(double splitValue, int totalRatio,double ratioBalance);
    }
}
