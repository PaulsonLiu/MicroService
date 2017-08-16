using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace iiERP.Tools
{
    /// <summary>
    /// 类    名：存储过程分页<br/>
    /// 功能说明：用存储过程获取分页数据<br/>
    /// 作    者：易小辉<br/>
    /// 创建时间：2011-03-04<br/>
    /// 最后修改：<br/>
    /// </summary>
    public class DbPagerSQL
    {
        #region 获取分页数据（存储过程Row_Number分页，支持单表）
        /// <summary>
        ///  获取分页数据（存储过程Row_Number分页，支持单表）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="getFields">获取的字段列表</param>
        /// <param name="whereCondition">查询条件(不带where)</param>
        /// <param name="orderByField">排序字段（必须,不带order by）</param>
        /// <param name="groupBy">分组(不带group by)</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerData(string connectionString, string tableName, string getFields, string whereCondition, string orderByField, string groupBy, int currentPageIndex, int pageSize, out long recordCount)
        {
            recordCount = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                DbHelperSQL.AddInParameter("@TableName",SqlDbType.VarChar,200, tableName),
                DbHelperSQL.AddInParameter("@GetFields",SqlDbType.VarChar,2000,getFields),
                DbHelperSQL.AddInParameter("@WhereCondition",SqlDbType.VarChar,5000, whereCondition),
                DbHelperSQL.AddInParameter("@OrderByField",SqlDbType.VarChar,500,orderByField),
                DbHelperSQL.AddInParameter("@GroupBy",SqlDbType.VarChar,500,groupBy),
                DbHelperSQL.AddInParameter("@CurrentPageIndex",SqlDbType.Int, currentPageIndex),
                DbHelperSQL.AddInParameter("@PageSize",SqlDbType.Int, pageSize),
                DbHelperSQL.AddOutParameter("@RecordCount",SqlDbType.Int),
                DbHelperSQL.AddOutParameter("@PageCount",SqlDbType.Int)
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "SP_DataPager", parms);
                recordCount = (int)(parms[7].Value);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        #endregion

        #region 获取分页数据（存储过程通过唯一主键分页，支持单表）
        /// <summary>
        /// 获取分页数据（存储过程通过唯一主键分页，支持单表）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">单一主键或唯一值键（必须）</param>
        /// <param name="getFields">获取的字段列表</param>
        /// <param name="whereCondition">查询条件（不带where）</param>
        /// <param name="orderByField">排序字段(不带order by)</param>
        /// <param name="sortType">排序类型，1:正序asc 2:倒序desc 3:多列排序</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByPK(string connectionString, string tableName, string primaryKey, string getFields, string whereCondition, string orderByField, int sortType, int currentPageIndex, int pageSize, out long recordCount)
        {
            recordCount = 0;
            if(sortType < 1 || sortType > 3) sortType =1;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                DbHelperSQL.AddInParameter("@TableName",SqlDbType.VarChar,200, tableName),
                DbHelperSQL.AddInParameter("@PrimaryKey",SqlDbType.VarChar,100,primaryKey),
                DbHelperSQL.AddInParameter("@GetFields",SqlDbType.VarChar,2000,getFields),
                DbHelperSQL.AddInParameter("@WhereCondition",SqlDbType.VarChar,5000, whereCondition),
                DbHelperSQL.AddInParameter("@OrderByField",SqlDbType.VarChar,500,orderByField),                
                DbHelperSQL.AddInParameter("@SortType",SqlDbType.Int,500,sortType),
                DbHelperSQL.AddInParameter("@CurrentPageIndex",SqlDbType.Int, currentPageIndex),
                DbHelperSQL.AddInParameter("@PageSize",SqlDbType.Int, pageSize),
                DbHelperSQL.AddOutParameter("@RecordCount",SqlDbType.Int),
                DbHelperSQL.AddOutParameter("@TotalCount",SqlDbType.Int),
                DbHelperSQL.AddOutParameter("@TotalPageCount",SqlDbType.Int),
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "SP_DataPager_PK", parms);
                recordCount = (int)(parms[8].Value);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 

        #region 获取分页数据(存储过程通过联合主键获取分页，支持单表)
        /// <summary>
        /// 获取分页数据(存储过程通过联合主键获取分页，支持单表)
        /// 适用于联合主键/单主键/存在能确定唯一行列/存在能确定唯一行的多列 (用英文,隔开) 
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">单一主键或唯一值键或联合主键列表(用英文,隔开)或能确定唯一行的多列列表(用英文,隔开) </param>
        /// <param name="getFields">获取的字段列表</param>
        /// <param name="whereCondition">查询条件（不带where）</param>
        /// <param name="orderByField">排序字段(不带order by，可以是多字段排序)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByUnionPK(string connectionString, string tableName, string primaryKey, string getFields, string whereCondition, string orderByField, int currentPageIndex, int pageSize, out long recordCount)
        {
            recordCount = 0;
            try
            {
                SqlParameter[] parms = new SqlParameter[]{
                DbHelperSQL.AddInParameter("@TableName",SqlDbType.VarChar,200, tableName),
                DbHelperSQL.AddInParameter("@PrimaryKey",SqlDbType.VarChar,100,primaryKey),
                DbHelperSQL.AddInParameter("@GetFields",SqlDbType.VarChar,2000,getFields),
                DbHelperSQL.AddInParameter("@WhereCondition",SqlDbType.VarChar,5000, whereCondition),
                DbHelperSQL.AddInParameter("@OrderByField",SqlDbType.VarChar,500,orderByField),
                DbHelperSQL.AddInParameter("@CurrentPageIndex",SqlDbType.Int, currentPageIndex),
                DbHelperSQL.AddInParameter("@PageSize",SqlDbType.Int, pageSize),
                DbHelperSQL.AddOutParameter("@RecordCount",SqlDbType.Int),
                DbHelperSQL.AddOutParameter("@TotalCount",SqlDbType.Int),
                DbHelperSQL.AddOutParameter("@TotalPageCount",SqlDbType.Int),
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "SP_DataPager_PK", parms);
                recordCount = (int)(parms[7].Value);
                return dt;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据SQL语句获得相应的记录数
        /// <summary>
        /// 根据SQL语句获得相应的记录数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>记录数</returns>
        public static long GetRecordCount(string connectionString, string sql, params SqlParameter[] cmdParms)
        {
            string newSql = string.Format("select count(*) from ({0}) temp001", sql);
            object count = DbHelperSQL.ExecuteScalar(connectionString, newSql, cmdParms);
            if (count == DBNull.Value || count == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(count);
            }
        }

        /// <summary>
        /// 根据SQL语句获得相应的记录数
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>记录数</returns>
        public static long GetRecordCount(SqlConnection conn, string sql, params SqlParameter[] cmdParms)
        {
            string newSql = string.Format("select count(*) from ({0}) temp001", sql, cmdParms);
            object count = DbHelperSQL.ExecuteScalar(conn, newSql);
            if (count == DBNull.Value || count == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(count);
            }
        }
        #endregion

        #region 根据SQL语句获得某一页的内容(SQL2000，支持多表)

        /// <summary>
        /// 根据SQL语句获得某一页的内容(SQL2000，支持多表)
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="orderBy">排序的条件(比如：order by ID Desc)</param>
        /// <param name="primaryKey">主键(比如：p.Id)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="parms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataBySQL2000(string connectionString, string sql, string orderBy, string primaryKey, int currentPageIndex, int pageSize, out int recordCount, params SqlParameter[] parms)
        {
            recordCount = 0;
            // 得到from所在的位置
            int fromIndex = sql.LastIndexOf(" from ", StringComparison.OrdinalIgnoreCase);
            // 得到where所在地位置
            int whereIndex = sql.LastIndexOf(" where ", StringComparison.OrdinalIgnoreCase);
            // 得到查询的字段
            string fields = sql.Substring(7, fromIndex - 7);
            // 得到from后面的语句
            string from = sql.Substring(fromIndex, whereIndex - fromIndex);
            // 得到where后面的语句
            string where = (whereIndex == -1 ? "where 1=1" : sql.Substring(whereIndex));
            // 拼凑要执行的SQL语句
            string sqlAll = string.Format("select top {0} {1} {2} {3} and {4} not in(select top {5} {6} {7} {8} {9}) {10}", pageSize, fields, from, where, primaryKey, currentPageIndex, primaryKey, from, where, orderBy, orderBy);
            sqlAll += ";select count(*) " + from + " " + where;
            DataSet ds = DbHelperSQL.ExecuteDataSet(connectionString, sqlAll, parms);
            // 设置输出记录数
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }

        #endregion

        #region 根据SQL语句获得某一页的内容(RowNumeber，支持多表,sql2005+)

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber，支持多表,2005+)
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(string connectionString, string sql, string overExpress, int currentPageIndex, int pageSize, params SqlParameter[] cmdParms)
        {
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select *, ROW_NUMBER() OVER({0}) myRowNo from ({1}) temp001) temp002 where myRowNo BETWEEN @recordStartIndex and @recordEndIndex ", overExpress, sql);
            //参数
            List<SqlParameter> cmdParmList = new List<SqlParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordStartIndex", recordStartIndex));
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordEndIndex", recordEndIndex));
            DataSet ds = DbHelperSQL.ExecuteDataSet(connectionString, newSql, cmdParmList.ToArray());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber，支持多表,sql2005+)
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(string connectionString, string sql, string overExpress, int currentPageIndex, int pageSize, out long recordCount, params SqlParameter[] cmdParms)
        {
            recordCount = 0;
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select *, ROW_NUMBER() OVER({0}) myRowNo from ({1}) temp001) temp002 where myRowNo BETWEEN @recordStartIndex and @recordEndIndex ", overExpress, sql);
            newSql += string.Format(";select count(*) from ({0}) temp003", sql);
            //参数
            List<SqlParameter> cmdParmList = new List<SqlParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordStartIndex", recordStartIndex));
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordEndIndex", recordEndIndex));
            DataSet ds = DbHelperSQL.ExecuteDataSet(connectionString, newSql, cmdParmList.ToArray());
            // 设置输出记录数
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];
        }

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber，支持多表,sql2005+)
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(SqlConnection conn, string sql, string overExpress, int currentPageIndex, int pageSize, params SqlParameter[] cmdParms)
        {
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select *, ROW_NUMBER() OVER({0}) myRowNo from ({1}) temp001) temp002 where myRowNo BETWEEN @recordStartIndex and @recordEndIndex ", overExpress, sql);
            //参数
            List<SqlParameter> cmdParmList = new List<SqlParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordStartIndex", recordStartIndex));
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordEndIndex", recordEndIndex));
            DataSet ds = DbHelperSQL.ExecuteDataSet(conn, newSql, cmdParmList.ToArray());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber，支持多表,sql2005+)
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(SqlConnection conn, string sql, string overExpress, int currentPageIndex, int pageSize, out long recordCount, params SqlParameter[] cmdParms)
        {
            recordCount = 0;
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select *, ROW_NUMBER() OVER({0}) myRowNo from ({1}) temp001) temp002 where myRowNo BETWEEN @recordStartIndex and @recordEndIndex ", overExpress, sql);
            newSql += string.Format(";select count(*) from ({0}) temp003", sql);
            //参数
            List<SqlParameter> cmdParmList = new List<SqlParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordStartIndex", recordStartIndex));
            cmdParmList.Add(DbHelperSQL.AddInParameter("@recordEndIndex", recordEndIndex));
            DataSet ds = DbHelperSQL.ExecuteDataSet(conn, newSql, cmdParmList.ToArray());
            // 设置输出记录数
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];
        }
        #endregion

        /// <summary>
        /// 返回分页的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="overExpress"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static string GetPagerSqlByRowNumber(string sql, string overExpress, int currentPageIndex, int pageSize, params SqlParameter[] cmdParms)
        {
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            // 得到from所在的位置
            int fromIndex = sql.LastIndexOf(" from ", StringComparison.OrdinalIgnoreCase);
            // 得到查询的字段
            string fields = sql.Substring(7, fromIndex - 7);
            // 得到from后面的语句
            string from = sql.Substring(fromIndex);
            // 拼凑要执行的SQL语句
            string newSql = string.Format("select * from (select {0},row_number() over({1}) myrowno {2}) mytable where myrowno between {3} and {4}", fields, overExpress, from, recordStartIndex, recordEndIndex);
             
            return newSql;
        }
    }
}
