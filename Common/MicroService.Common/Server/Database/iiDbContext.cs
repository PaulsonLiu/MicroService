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
        public DbSet<MR0001_USER_MSTR> MR0001_USER_MSTR { get; set; }
        public DbSet<MR0003_COMPANY> MR0003_COMPANY { get; set; }

    }
}
