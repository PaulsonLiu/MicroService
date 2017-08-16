using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;
using System.Linq;

namespace iiERP.Tools
{

    public class DbPagerOracle
    {
        #region RowNum分页
        /// <summary>
        /// 根据RowNum获取某一分页的内容
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNum(string connectionString, string sql, int currentPageIndex, int pageSize, params OracleParameter[] cmdParms)
        {
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select temptb.*,rownum myrowno from ({0}) temptb where rownum<=:recordEndIndex) where myrowno>=:recordStartIndex", sql);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(connectionString, newSql, cmdParmList.ToArray());
            return dt;

        }

        /// <summary>
        /// 根据RowNum获取某一分页的内容
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNum(string connectionString, string sql, int currentPageIndex, int pageSize, out int recordCount, params OracleParameter[] cmdParms)
        {
            recordCount = 0;
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select temptb.*,rownum myrowno from ({0}) temptb where rownum<=:recordEndIndex) where myrowno>=:recordStartIndex", sql);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(connectionString, newSql, cmdParmList.ToArray());
            recordCount = GetRecordCount(connectionString, sql, cmdParms);
            return dt;
        }

        /// <summary>
        /// 根据RowNum获取某一分页的内容
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNum(OracleConnection conn, string sql, int currentPageIndex, int pageSize, params OracleParameter[] cmdParms)
        {
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select temptb.*,rownum myrowno from ({0}) temptb where rownum<=:recordEndIndex) where myrowno>=:recordStartIndex", sql);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(conn, newSql, cmdParmList.ToArray());
            return dt;
        }
       
        /// <summary>
        /// 根据RowNum获取某一分页的内容
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNum(OracleConnection conn, string sql, int currentPageIndex, int pageSize, out int recordCount, params OracleParameter[] cmdParms)
        {
            recordCount = 0;
            if (currentPageIndex <= 0) currentPageIndex = 1;
            if (pageSize <= 0) pageSize = 10;
            int recordStartIndex = (currentPageIndex - 1) * pageSize + 1;
            int recordEndIndex = currentPageIndex * pageSize;
            string newSql = string.Format("select * from (select temptb.*,rownum myrowno from ({0}) temptb where rownum<=:recordEndIndex) where myrowno>=:recordStartIndex", sql);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(conn, newSql, cmdParmList.ToArray());
            recordCount = GetRecordCount(conn, sql, cmdParms);
            return dt;
        }
        #endregion

        #region RowNumber分页
        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber)
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(string connectionString, string sql, string overExpress, int currentPageIndex, int pageSize, params OracleParameter[] cmdParms)
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
            string newSql = string.Format("select * from (select {0},row_number() over({1}) myrowno {2}) where myrowno between :recordStartIndex and :recordEndIndex", fields, overExpress, from);
           
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(connectionString, newSql, cmdParmList.ToArray());
            return dt;
        }

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber)
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(string connectionString, string sql, string overExpress, int currentPageIndex, int pageSize, out int recordCount, params OracleParameter[] cmdParms)
        {
            recordCount = 0;
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
            string newSql = string.Format("select * from (select {0},row_number() over({1}) myrowno {2}) where myrowno between :recordStartIndex and :recordEndIndex", fields, overExpress, from);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(connectionString, newSql, cmdParmList.ToArray());
            // 设置输出记录数
            recordCount = GetRecordCount(connectionString, sql, cmdParms);
            return dt;
        }

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber)
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(OracleConnection conn, string sql, string overExpress, int currentPageIndex, int pageSize, params OracleParameter[] cmdParms)
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
            string newSql = string.Format("select * from (select {0},row_number() over({1}) myrowno {2}) where myrowno between :recordStartIndex and :recordEndIndex", fields, overExpress, from);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(conn, newSql, cmdParmList.ToArray());
            return dt;
        }

        /// <summary>
        /// 根据SQL语句获得某一页的内容(RowNumeber)
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="overExpress">over表达式(比如：ID Desc,)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>分页后的DataTable数据集</returns>
        public static DataTable GetPagerDataByRowNumber(OracleConnection conn, string sql, string overExpress, int currentPageIndex, int pageSize, out int recordCount, params OracleParameter[] cmdParms)
        {
            recordCount = 0;
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
            string newSql = string.Format("select * from (select {0},row_number() over({1}) myrowno {2}) where myrowno between :recordStartIndex and :recordEndIndex", fields, overExpress, from);
            List<OracleParameter> cmdParmList = new List<OracleParameter>();
            if (cmdParms != null && cmdParms.Length > 0)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    cmdParmList.Add(parm);
                }
            }
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordEndIndex", recordEndIndex));
            cmdParmList.Add(DbHelperOracle.AddInParameter("recordStartIndex", recordStartIndex));
            DataTable dt = DbHelperOracle.ExecuteDataTable(conn, newSql, cmdParmList.ToArray());
            // 设置输出记录数
            recordCount = GetRecordCount(conn, sql, cmdParms);
            return dt;
        }
        #endregion

        #region 获取记录总数
        /// <summary>
        /// 根据SQL语句获得相应的记录数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>记录数</returns>
        public static int GetRecordCount(string connectionString, string sql, params OracleParameter[] cmdParms)
        {
            string newSql = string.Format("select count(*) from ({0}) temp001", sql);
            object count = DbHelperOracle.ExecuteScalar(connectionString, newSql, cmdParms);
            if (count == null || count == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(count);
            }
        }
        /// <summary>
        /// 根据SQL语句获得相应的记录数
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="sql">原始SQL语句</param>
        /// <param name="cmdParms">参数数组</param>
        /// <returns>记录数</returns>
        public static int GetRecordCount(OracleConnection conn, string sql, params OracleParameter[] cmdParms)
        {
            string newSql = string.Format("select count(*) from ({0}) temp001", sql);
            object count = DbHelperOracle.ExecuteScalar(conn, newSql, cmdParms);
            if (count == null || count == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(count);
            }
        }
        #endregion
    }
}
