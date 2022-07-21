using LanisterPay.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.CustomAttributes
{
    public class SplitInfoArrayConstraint:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var splitInfoArray=(IList<SplitInfoEntity>)validationContext.ObjectInstance;
            if (splitInfoArray.Count < 1 || splitInfoArray.Count > 20)
            {
                return new ValidationResult("Invalid Split Info Array");
            }
            return ValidationResult.Success;
        }
    }
}
