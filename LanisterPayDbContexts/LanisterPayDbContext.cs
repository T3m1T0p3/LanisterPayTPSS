using LanisterPay.CalculateAmounts;
using LanisterPay.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanisterPay.LanisterPayDbContexts
{
    public class LanisterPayDbContext:DbContext
    {
        IConfiguration _configuration;
        public DbSet<TransactionEntity> transactionEntities { get; set; }
        public DbSet<SplitInfoEntity> splitInfoEntities { get; set; }
        public LanisterPayDbContext(DbContextOptions<LanisterPayDbContext> options,IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_configuration.GetConnectionString("LanisterPay"),
                option => option.MigrationsAssembly(typeof(LanisterPayDbContext).Assembly.GetName().Name));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<TransactionEntity>().HasMany<SplitInfoEntity>();
            builder.Entity<SplitInfoEntity>().HasKey("SplitInfoEntityId");
            builder.Entity<TransactionEntity>().HasKey("TransactionEntityId");
            //builder.Ignore<ICalculateAmount>();
        }
    }
}
