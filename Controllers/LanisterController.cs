using AutoMapper;
using LanisterPay.CalculateAmounts;
using LanisterPay.Entity;
using LanisterPay.EntityDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using LanisterPay.LanisterPayDbContexts;

namespace LanisterPay.Controllers
{
    [ApiController]
    [Route("split-payments/")]
    public class LanisterController : ControllerBase
    {
        private IMapper _mapper;
        //private LanisterPayDbContext _transactionContext;
        public LanisterController(IMapper mapper)//LanisterPayDbContext context,ICalculateAmount calculateAmount)
        {
            _mapper = mapper;
            //_transactionContext = context;
        }

        [HttpPost("compute")]
        public async Task<IActionResult> Compute([FromBody]TransactionEntityDto clientTransaction)
        {
            TransactionEntityDto transactionDto;
            TransactionEntity transactionEntity;
            SplitInfoEntityDto splitInfoDto;
            SplitResult result;
            int totalRatio = 0;
            double ratioBalance = 0;
            SplitInfoEntity splitInfoEntity=new SplitInfoEntity();
            try
            {
                transactionDto = _mapper.Map<TransactionEntityDto>(clientTransaction);
                transactionEntity=_mapper.Map<TransactionEntity>(transactionDto);
                result = new SplitResult(transactionDto.Id);
                transactionDto.SplitInfo.Sort((x,y)=>x.SplitType[0]-y.SplitType[0]);
                for (int i=0;i< transactionDto.SplitInfo.Count;i++)
                {
                    splitInfoDto = transactionDto.SplitInfo[i];
                    splitInfoEntity = _mapper.Map<SplitInfoEntity>(splitInfoDto);
                    if (splitInfoDto.SplitType.ToLower() == "ratio")
                    {
                        if (totalRatio <= 0)
                        {
                            ratioBalance = transactionEntity.GetBalance();
                            int temp = i;
                            while (temp < transactionDto.SplitInfo.Count)
                            {
                                splitInfoDto = transactionDto.SplitInfo[temp++];
                                totalRatio += (int)splitInfoDto.SplitValue;
                            }
                        }
                        result.SplitBreakdown.Add( await transactionEntity.CalculateAmount(splitInfoEntity,totalRatio,ratioBalance));
                    }
                    else
                    {
                        result.SplitBreakdown.Add(await transactionEntity.CalculateAmount(splitInfoEntity));
                    }
                    
                }

                result.Balance = transactionEntity.GetBalance();
                //await _transactionContext.splitInfoEntities.AddAsync(splitInfoEntity);
                //await _transactionContext.transactionEntities.AddAsync(transactionEntity);
                //await _transactionContext.SaveChangesAsync();
                return Ok(result);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
