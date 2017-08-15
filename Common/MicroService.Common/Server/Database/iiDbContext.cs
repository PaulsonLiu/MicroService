using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using System.ComponentModel.DataAnnotations;

namespace MicroService.Common
{
    /// <summary>
    /// EF扩展方法
    /// </summary>
    public class iiDbContext : DbContext
    {
        #region property

        #endregion property

        /// <summary>
        /// 初始化数据库的连接
        /// </summary>
        /// <param name="dbName">数据库名</param>
        public iiDbContext()
        {

        }

        public iiDbContext(DbContextOptions<iiDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 保存前验证模型有效性
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    StringBuilder errMsg = new StringBuilder();
                    foreach (var item in validationResults)
                    {
                        errMsg.Append(item.ErrorMessage + "\r\n");
                    }
                    throw new ValidationException(errMsg.ToString());
                }
            }
            return base.SaveChanges();
        }

        public DbSet<MR0001_USER_MSTR> MR0001_USER_MSTR { get; set; }
        public DbSet<MR0003_COMPANY> MR0003_COMPANY { get; set; }

    }
}
