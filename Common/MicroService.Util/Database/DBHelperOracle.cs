using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;

namespace iFramework.Util
{
    /// <summary>
    /// 页 面 名：Oracle数据库操作类<br/>
    /// 说    明：<br/>
    /// 作    者：易小辉<br/>
    /// 创建时间：2012-10-24<br/>
    /// 最后修改：2012-10-24<br/>
    /// </summary>
    public class DbHelperOracle
    {
        #region ExecuteDataTable 创建DataTable

        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataTable 对象。</returns>
        public static DataTable ExecuteDataTable(string connectionString, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataTable(connectionString, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性（sql语句或存储过程）</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataTable 对象。</returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleConnection myConn = new OracleConnection(connectionString))
            {
                using (OracleCommand myCmd = new OracleCommand())
                {
                    OracleDataAdapter myda = null;
                    DataTable dt = new DataTable();
                    try
                    {
                        PrepareCommand(myConn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                        myda = new OracleDataAdapter(myCmd);
                        myda.Fill(dt);
                        myda.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmdParms != null)
                        {
                            myCmd.Parameters.Clear();
                        }
                        myCmd.Dispose();
                        ConnClose(myConn);
                    }
                    return dt;
                }
            }
        }

        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataTable 对象。</returns>
        public static DataTable ExecuteDataTable(OracleConnection conn, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataTable(conn, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataTable 对象。</returns>
        public static DataTable ExecuteDataTable(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                OracleDataAdapter myda = null;
                DataTable dt = new DataTable();
                try
                {
                    PrepareCommand(conn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                    myda = new OracleDataAdapter(myCmd);
                    myda.Fill(dt);
                    myda.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return dt;
            }
        }

        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataTable 对象。</returns>
        public static DataTable ExecuteDataTable(OracleTransaction trans, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataTable(trans, CommandType.Text, cmdText, cmdParms);
        }
        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataTable 对象。</returns>
        public static DataTable ExecuteDataTable(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                OracleDataAdapter myda = null;
                DataTable dt = new DataTable();
                try
                {
                    PrepareCommand(trans.Connection, myCmd, trans, cmdType, cmdText, 120, cmdParms);
                    myda = new OracleDataAdapter(myCmd);
                    myda.Fill(dt);
                    myda.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return dt;
            }
        }
        #endregion

        #region ExecuteDataSet 创建DataSet

        /// <summary>
        /// 创建DataSet
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns> DataSet 对象。</returns>
        public static DataSet ExecuteDataSet(string connectionString, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建DataSet
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataSet 对象。</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleConnection myConn = new OracleConnection(connectionString))
            {
                using (OracleCommand myCmd = new OracleCommand())
                {
                    OracleDataAdapter myda = null;
                    DataSet ds = new DataSet();
                    try
                    {
                        PrepareCommand(myConn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                        myda = new OracleDataAdapter(myCmd);
                        myda.Fill(ds);
                        myda.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmdParms != null)
                        {
                            myCmd.Parameters.Clear();
                        }
                        myCmd.Dispose();
                        ConnClose(myConn);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 创建DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns> DataSet 对象。</returns>
        public static DataSet ExecuteDataSet(OracleConnection conn, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataSet(conn, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建DataSet
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>DataSet 对象。</returns>
        public static DataSet ExecuteDataSet(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                OracleDataAdapter myda = null;
                DataSet ds = new DataSet();
                try
                {
                    PrepareCommand(conn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                    myda = new OracleDataAdapter(myCmd);
                    myda.Fill(ds);
                    myda.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return ds;
            }
        }
        #endregion

        #region ExecuteDataReader 创建OracleDataReader

        /// <summary>
        /// 创建 OracleDataReader
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns> OracleDataReader 对象。</returns>
        public static OracleDataReader ExecuteDataReader(string connectionString, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataReader(connectionString, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建 OracleDataReader
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>一个 OracleDataReader 对象。</returns>
        public static OracleDataReader ExecuteDataReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleConnection myConn = new OracleConnection(connectionString);
            OracleCommand myCmd = new OracleCommand();
            OracleDataReader dr = null;
            try
            {
                PrepareCommand(myConn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                dr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                ConnClose(myConn);
                throw ex;
            }
            finally
            {
                if (cmdParms != null)
                {
                    myCmd.Parameters.Clear();
                }
            }
            return dr;
        }


        /// <summary>
        /// 创建 OracleDataReader。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns> OracleDataReader 对象。</returns>
        public static OracleDataReader ExecuteDataReader(OracleConnection conn, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteDataReader(conn, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 创建 OracleDataReader。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>一个 OracleDataReader 对象。</returns>
        public static OracleDataReader ExecuteDataReader(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand myCmd = new OracleCommand();
            OracleDataReader dr = null;
            try
            {
                PrepareCommand(conn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                dr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmdParms != null)
                {
                    myCmd.Parameters.Clear();
                }
            }
            return dr;
        }
        #endregion

        #region  ExecuteNonQuery 执行SQL语句

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回受影响的行数。</returns>
        public static int ExecuteNonQuery(string connectionString, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回受影响的行数。</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleConnection myConn = new OracleConnection(connectionString))
            {
                using (OracleCommand myCmd = new OracleCommand())
                {
                    int retval = 0;
                    try
                    {
                        PrepareCommand(myConn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                        retval = myCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmdParms != null)
                        {
                            myCmd.Parameters.Clear();
                        }
                        myCmd.Dispose();
                        ConnClose(myConn);
                    }
                    return retval;
                }
            }
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回受影响的行数。</returns>
        public static int ExecuteNonQuery(OracleConnection conn, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteNonQuery(conn, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 对连接对象执行 SQL 语句。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回受影响的行数。</returns>
        public static int ExecuteNonQuery(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                int retval = 0;
                try
                {
                    PrepareCommand(conn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                    retval = myCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return retval;
            }
        }

        /// <summary>
        /// 对事务执行 SQL 语句。
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">Transact-SQL 语句</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回受影响的行数。</returns>
        public static int ExecuteNonQuery(OracleTransaction trans, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteNonQuery(trans, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 对事务执行 SQL 语句。
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回受影响的行数。</returns>
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                int retval = 0;
                try
                {
                    PrepareCommand(trans.Connection, myCmd, trans, cmdType, cmdText, 120, cmdParms);
                    retval = myCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return retval;
            }
        }
        #endregion

        #region ExecuteScalar 执行标量查询

        /// <summary>
        /// 标量查询，返回查询结果集中第一行的第一列。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回值</returns>
        public static Object ExecuteScalar(string connectionString, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteScalar(connectionString, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 标量查询，返回查询结果集中第一行的第一列。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回值</returns>
        public static Object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleConnection myConn = new OracleConnection(connectionString))
            {
                using (OracleCommand myCmd = new OracleCommand())
                {
                    object retval = null;
                    try
                    {
                        PrepareCommand(myConn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                        retval = myCmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (cmdParms != null)
                        {
                            myCmd.Parameters.Clear();
                        }
                        myCmd.Dispose();
                        ConnClose(myConn);
                    }
                    return retval;
                }
            }
        }

        /// <summary>
        /// 标量查询，返回查询结果集中第一行的第一列。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回值</returns>
        public static Object ExecuteScalar(OracleConnection conn, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteScalar(conn, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 标量查询，返回查询结果集中第一行的第一列。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回值</returns>
        public static Object ExecuteScalar(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                object retval = null;
                try
                {
                    PrepareCommand(conn, myCmd, null, cmdType, cmdText, 120, cmdParms);
                    retval = myCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return retval;
            }
        }

        /// <summary>
        /// 标量查询，返回查询结果集中第一行的第一列。
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回值</returns>
        public static Object ExecuteScalar(OracleTransaction trans, string cmdText, params OracleParameter[] cmdParms)
        {
            return ExecuteScalar(trans, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 标量查询，返回查询结果集中第一行的第一列。
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="cmdType">该值指示如何解释 CommandText 属性</param>
        /// <param name="cmdText">Transact-SQL 语句或存储过程名称。</param>
        /// <param name="cmdParms">参数列表，params可变长数组的形式</param>
        /// <returns>返回值</returns>
        public static Object ExecuteScalar(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                object retval = null;
                try
                {
                    PrepareCommand(trans.Connection, myCmd, trans, cmdType, cmdText, 120, cmdParms);
                    retval = myCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cmdParms != null)
                    {
                        myCmd.Parameters.Clear();
                    }
                    myCmd.Dispose();
                }
                return retval;
            }
        }
        #endregion

        #region ExecuteTransaction 执行事务
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="SQLStringList">SQL字符串列表</param>
        public static bool ExecuteTransaction(string connectionString, List<String> SQLStringList)
        {
            using (OracleConnection myConn = new OracleConnection(connectionString))
            {
                using (OracleCommand myCmd = new OracleCommand())
                {
                    myConn.Open();
                    OracleTransaction trans = myConn.BeginTransaction();
                    myCmd.Connection = myConn;
                    myCmd.Transaction = trans;
                    myCmd.CommandTimeout = 180;
                    try
                    {
                        foreach (string sql in SQLStringList)
                        {
                            if (!String.IsNullOrEmpty(sql))
                            {
                                myCmd.CommandText = sql;
                                myCmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                        //return false;
                    }
                    finally
                    {
                        ConnClose(myConn);
                        trans.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="SQLItemList">SQL项</param>
        public static bool ExecuteTransaction(string connectionString, List<PLSQLItem> SQLItemList)
        {
            using (OracleConnection myConn = new OracleConnection(connectionString))
            {
                using (OracleCommand myCmd = new OracleCommand())
                {
                    myConn.Open();
                    OracleTransaction trans = myConn.BeginTransaction();
                    myCmd.Connection = myConn;
                    myCmd.Transaction = trans;
                    myCmd.CommandTimeout = 180;
                    try
                    {
                        foreach (PLSQLItem item in SQLItemList)
                        {
                            if (!String.IsNullOrEmpty(item.SQLString))
                            {
                                myCmd.CommandText = item.SQLString;
                                if (item.CmdParms != null)
                                {
                                    foreach (OracleParameter parm in item.CmdParms)
                                    {
                                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) && (parm.Value == null))
                                        {
                                            parm.Value = DBNull.Value;
                                        }
                                        myCmd.Parameters.Add(parm);
                                    }
                                }
                                myCmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                        //return false;
                    }
                    finally
                    {
                        ConnClose(myConn);
                        trans.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="SQLStringList">SQL字符串列表</param>
        public static bool ExecuteTransaction(OracleConnection conn, List<String> SQLStringList)
        {
            using (OracleCommand myCmd = new OracleCommand())
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                OracleTransaction trans = conn.BeginTransaction();
                myCmd.Connection = conn;
                myCmd.Transaction = trans;
                myCmd.CommandTimeout = 180;
                try
                {
                    foreach (string sql in SQLStringList)
                    {
                        if (!String.IsNullOrEmpty(sql))
                        {
                            myCmd.CommandText = sql;
                            myCmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                    //return false;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }


        #endregion

        #region AddInParameter 添加输入参数
        /// <summary>
        /// 添加In参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="value">值</param>
        /// <returns>返回一个OracleParameter对象</returns>
        public static OracleParameter AddInParameter(string paramName, object value)
        {
            OracleParameter param = new OracleParameter(paramName, value);
            return param;
        }

        /// <summary>
        /// 添加In参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">值</param>
        /// <returns>返回一个OracleParameter对象</returns>
        public static OracleParameter AddInParameter(string paramName, OracleType dbType, object value)
        {
            return AddInParameter(paramName, dbType, 0, value);
        }
        /// <summary>
        /// 添加In参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">字段大小</param>
        /// <param name="value">值</param>
        /// <returns>返回一个OracleParameter对象</returns>
        public static OracleParameter AddInParameter(string paramName, OracleType dbType, int size, object value)
        {
            OracleParameter param;
            if (size > 0)
                param = new OracleParameter(paramName, dbType, size);
            else
                param = new OracleParameter(paramName, dbType);
            param.Value = value;

            return param;
        }
        #endregion

        #region AddOutParameter 添加输出参数
        /// <summary>
        /// 添加Out参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <returns>返回一个OracleParameter对象</returns>
        public static OracleParameter AddOutParameter(string paramName, OracleType dbType)
        {
            return AddOutParameter(paramName, dbType, 0, null);
        }

        /// <summary>
        /// 添加Out参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">字段大小</param>
        /// <returns>返回一个OracleParameter对象</returns>
        public static OracleParameter AddOutParameter(string paramName, OracleType dbType, int size)
        {
            return AddOutParameter(paramName, dbType, size, null);
        }
        public static OracleParameter AddOutParameter(string paramName, OracleType dbType, int size, object value)
        {
            OracleParameter param;
            if (size > 0)
            {
                param = new OracleParameter(paramName, dbType, size);
            }
            else
            {
                param = new OracleParameter(paramName, dbType);
            }
            if (value != null)
            {
                param.Value = value;
            }
            param.Direction = ParameterDirection.Output;

            return param;
        }
        #endregion

        #region PrepareCommand 创建Command
        private static void PrepareCommand(OracleConnection conn, OracleCommand cmd, OracleTransaction trans, CommandType cmdType, string cmdText, int timeout, OracleParameter[] cmdParms)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            //if (timeout > 30) cmd.CommandTimeout = timeout;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) && (parm.Value == null))
                    {
                        parm.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parm);
                }
            }
            ConnOpen(conn);
        }
        #endregion

        #region ConnClose 关闭数据库连接
        public static void ConnClose(OracleConnection conn)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        #endregion

        #region ConnOpen 打开数据库连接
        public static void ConnOpen(OracleConnection conn)
        {
            if (conn != null && conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }
        #endregion
    }

    /// <summary>
    /// SQL项（用于多条SQL语句）
    /// </summary>
    public class PLSQLItem
    {
        /// <summary>
        /// SQL语句
        /// </summary>
        public string SQLString
        {
            get;
            set;
        }
        /// <summary>
        /// 参数数组
        /// </summary>
        public OracleParameter[] CmdParms
        {
            get;
            set;
        }
    }
}
