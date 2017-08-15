using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iiERP.Model;
using System.Data.Common;

namespace MicroService.Common
{
    public class SqlEngineQueryBuilder : QueryBuilder
    {
        public SqlEngineQueryBuilder(string TableName)
        {
            this.TableName = TableName; 
        }

        public override string ToQuerySql(FilterArgs filterArgs, SortArgs sortArgs, PagingArgs pageArgs, string SelectSQL, out IEnumerable<System.Data.Common.DbParameter> dbParamterList)
        {
            SqlEngine sqlEngine = new SqlEngine();
            Dictionary<string, string> paramters = null;
            dbParamterList = new List<DbParameter>();
            //var where = filterArgs.ToWhereString(out paramters);


            //List<FilterItem> Filters = new List<FilterItem>();
            //FilterItem Filter = new FilterItem();
            //Filter.ObjectName = "MR0106_ACCT_COMM";
            //Filter.ColumnName = "MR0106_COMM_VALUE";
            //Filter.OperationType1 = FilterOperationType.EqualTo;
            //Filter.Value1 = "2323232";
            //Filters.Add(Filter);

            //Filter = new FilterItem();
            //Filter.ObjectName = "MR0140_ACCT_ADDRESS";
            //Filter.ColumnName = "MR0140_ADDRESS_L1";
            //Filter.OperationType1 = FilterOperationType.EqualTo;
            //Filter.Value1 = "111111";
            //Filters.Add(Filter);
            string ObjectType = filterArgs.ObjectName;
            if (string.IsNullOrWhiteSpace(ObjectType))
            {
                ObjectType = this.TableName;
            }

            foreach (var item in filterArgs.FilterItems)
            {
                if (string.IsNullOrWhiteSpace(item.ObjectName))
                {
                    item.ObjectName = this.TableName;

                }
            }
            var privargs = new PrivArgs() { PrivLevel = 2, CurrentBU = UserInfo.GetUserInfo().BU, CurrentUsr = UserInfo.GetUserInfo().BU_User, Object = ObjectType, Option = "OPT_VIEW" };
            if (pageArgs.RequirePaging)
            {
                string sql = sqlEngine.ToSqlByObj(ObjectType, new List<string>() { "*" }, 1, privargs, sortArgs.GetOrderString(), pageArgs.PageIndex, pageArgs.PageSize, "", "", filterArgs.Where, false, filterArgs, SelectSQL);
                //dbParamterList = paramters.Select(m => DbParameterHelper.CreateDbParameter(m.Key, m.Value));
                return sql;
            }
            else
            {
                string sql = sqlEngine.ToSqlByObj(ObjectType, new List<string>() { "*" }, 1, privargs, sortArgs.GetOrderString(), -1, pageArgs.PageSize, "REPORT", "", filterArgs.Where, false, filterArgs.ToUTCFilterArgs(), SelectSQL);
                //dbParamterList = paramters.Select(m => DbParameterHelper.CreateDbParameter(m.Key, m.Value));
                return sql;

            }
        }

        public override string ToCountSql(FilterArgs filterArgs, string SelectSQL, out IEnumerable<System.Data.Common.DbParameter> dbParamterList)
        {
            string ObjectType = filterArgs.ObjectName;
            if (string.IsNullOrWhiteSpace(ObjectType))
            {
                ObjectType = this.TableName;
            }

            foreach (var item in filterArgs.FilterItems)
            {
                if (string.IsNullOrWhiteSpace(item.ObjectName))
                {
                    item.ObjectName = this.TableName;

                }
            }
            dbParamterList = new List<DbParameter>();
            SqlEngine sqlEngine = new SqlEngine();
            Dictionary<string, string> paramters = null;
            //var where = filterArgs.ToWhereString(out paramters);
            var privargs = new PrivArgs() { PrivLevel = 2, CurrentBU = UserInfo.GetUserInfo().BU, CurrentUsr = UserInfo.GetUserInfo().BU_User, Object = ObjectType, Option = "OPT_VIEW" };
            var sql = sqlEngine.ToSqlByObj(ObjectType, new List<string>() { "*" }, 1, privargs, null, 0, 0, "COUNT", "", filterArgs.Where, false, filterArgs.ToUTCFilterArgs());
            //dbParamterList = paramters.Select(m => DbParameterHelper.CreateDbParameter(m.Key, m.Value));
            return sql;
        }


    }
}
