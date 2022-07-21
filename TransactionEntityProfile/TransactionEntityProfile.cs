using AutoMapper;
using LanisterPay.CalculateAmounts;
using LanisterPay.Entity;
using LanisterPay.EntityDto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.TransactionEntityProfile
{
    public class TransactionEntityProfile:Profile
    {
        //public TransactionEntityProfile() { }
        public TransactionEntityProfile()
        {
            this.CreateMap< TransactionEntityDto, TransactionEntity>();
            this.CreateMap< SplitInfoEntityDto, SplitInfoEntity>();
        }
    }
}
