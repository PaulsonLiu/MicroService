using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiERP.Model;
using System.Data.Common;

namespace MicroService.Common
{
    /// <summary>
    /// 查询生成器
    /// </summary>
    public abstract class QueryBuilder
    {
        /// <summary>
        /// 当前表名
        /// </summary>
        public string TableName { get; protected set; }

        /// <summary>
        /// 生成全sql带分页查询的sql
        /// </summary>
        /// <returns></returns>
        public abstract string ToQuerySql(FilterArgs filterArgs, SortArgs sortArgs, PagingArgs pageArgs, string SelectSQL,out IEnumerable<DbParameter> dbParamterList);

        /// <summary>
        /// 获取全部数量的sql
        /// </summary>
        /// <returns></returns>
        public abstract string ToCountSql(FilterArgs filterArgs, string SelectSQL, out IEnumerable<DbParameter> dbParamterList);



    }
}
