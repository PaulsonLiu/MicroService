using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace MicroService.Common
{
    /// <summary>
    /// 数据库管理器
    /// 管理数据连接以及数据库的连接创建
    /// </summary>
    public class DatabaseMgmt
    {
        private const string SqlServerTemplate = "Data Source={0};Database={1};User ID={2};Password={3};MultipleActiveResultSets=true";
        private const string OraServerTemplate = "";

        public static string GetConnectionString()
        {
            AppConfiguration AppConfig = AppConfigurtaionServices.GetSetting<AppConfiguration>(nameof(AppConfiguration));
            return AppConfig.ConnectionString;
        }
    } 
}
