using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.CalculateAmounts
{
    public class CalculateAmount:ICalculateAmount
    {
        public double Calculate(string splitType,double splitValue,double balance)
        {
            Console.WriteLine("Calculating");
            if (splitType.ToLower() == "flat")
            {
                return splitValue;
            }
            else if(splitType.ToLower() == "percentage")
            {
                return (balance * splitValue) / 100;
            }

            throw new Exception("Unknown SplitType");
        }
        public double Calculate(double splitValue,int totalRatio,double ratioBalance)
        {
               return (splitValue / totalRatio) * ratioBalance;
        }
    }
}
