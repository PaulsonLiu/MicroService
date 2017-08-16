using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFramework.Util
{
    public class DatabaseHelper
    {
        private string _ConnString { get; set; }
        public DatabaseHelper(string ConnString)
        {
            _ConnString = ConnString;
        }
        private SqlConnection GetConnection(DbConnection Conn)
        {
            if (Conn == null)
            {
                Conn = new SqlConnection(_ConnString);
            }
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            return (SqlConnection)Conn;
        }
        private DbDataAdapter CreateSqlDataAdapter()
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            return dbDataAdapter;
        }
        private int BulkCopy2008(DataTable DataTable, DbConnection Conn)
        {
            if (DataTable != null && DataTable.Rows.Count > 0)
            {
                SqlConnection theConn = GetConnection(Conn);
                var theFieldString = "";
                foreach (DataColumn theCol in DataTable.Columns)
                {
                    if (theCol.ColumnName == "IS_DUP" || theCol.ColumnName == "ROW_ID")
                    {
                        continue;
                    }
                    if (theFieldString == "")
                    {
                        theFieldString = theCol.ColumnName;
                    }
                    else
                    {
                        theFieldString += "," + theCol.ColumnName;
                    }
                }
                var theSQL = "INSERT INTO " + DataTable.TableName + "(" + theFieldString + ") SELECT " + theFieldString + " FROM @DataTable AS A1";
                SqlCommand theCmmd = new SqlCommand("", theConn);
                SqlParameter theParameter = theCmmd.Parameters.AddWithValue("@DataTable", DataTable);
                theParameter.SqlDbType = SqlDbType.Structured;
                theParameter.TypeName = "dbo.BulkUdt";
                try
                {
                    return theCmmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    theConn.Close();
                }
            }
            else
            {
                return 0;
            }

        }
        public int BulkToDB(DataTable Datatable, DbConnection Conn, int NotifyAfter = 0, SqlRowsCopiedEventHandler RowsCopied = null, bool UseTransaction = false)
        {
            SqlConnection theConn = GetConnection(Conn);
            SqlTransaction theTrans = null;
            if (UseTransaction)
            {
                theTrans = theConn.BeginTransaction();
            }
            SqlBulkCopy theBulkCopy = new SqlBulkCopy(theConn, SqlBulkCopyOptions.Default, theTrans);
            theBulkCopy.DestinationTableName = Datatable.TableName;
            theBulkCopy.BatchSize = Datatable.Rows.Count;
            theBulkCopy.BulkCopyTimeout = 300;
            if (RowsCopied != null)
            {
                theBulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(RowsCopied);
            }

            if (NotifyAfter > 0)
            {
                theBulkCopy.NotifyAfter = NotifyAfter;
            }
            try
            {
                if (Datatable != null && Datatable.Rows.Count != 0)
                {
                    theBulkCopy.WriteToServer(Datatable);
                }
                if (theTrans != null)
                {
                    theTrans.Commit();
                }
                return Datatable.Rows.Count;
            }
            catch (Exception ex)
            {
                if (theTrans != null)
                {
                    theTrans.Rollback();
                }
                throw ex;
            }
            finally
            {
                theConn.Close();
                if (theBulkCopy != null)
                {
                    theBulkCopy.Close();
                }

            }
        }
        public int ExecuteCommandText(string SQL, List<System.Data.Common.DbParameter> Parameters, DbConnection Conn, DbTransaction Trans = null)
        {
            var theConn = GetConnection(Conn);
            try
            {
                var theCommand = theConn.CreateCommand();
                theCommand.CommandText = SQL;
                theCommand.Transaction = (SqlTransaction)Trans;
                if (Parameters != null)
                {
                    foreach (var item in Parameters)
                    {
                        theCommand.Parameters.Add(item);

                    }
                }
                var theCount = theCommand.ExecuteNonQuery();
                theConn.Close();
                return theCount;
            }
            finally
            {
                if (theConn.State == ConnectionState.Open && Trans == null)
                {
                    theConn.Close();
                }
            }

        }
        public System.Data.DataTable QueryByParam(string SQL, List<System.Data.Common.DbParameter> Parameters, DbConnection Conn, DbTransaction Trans = null)
        {
            var theConn = GetConnection(Conn);
            try
            {
                var theCommand = theConn.CreateCommand();
                theCommand.CommandText = SQL;
                theCommand.Transaction = (SqlTransaction)Trans;
                if (Parameters != null)
                {
                    foreach (var item in Parameters)
                    {
                        theCommand.Parameters.Add(item);

                    }
                }
                DbDataAdapter theDataAdapter = CreateSqlDataAdapter();
                theDataAdapter.SelectCommand = theCommand;
                DataSet theDataSet = new DataSet();
                theDataAdapter.Fill(theDataSet);

                theConn.Close();
                return theDataSet.Tables[0];
            }
            finally
            {
                if (theConn.State == ConnectionState.Open && Trans == null)
                {
                    theConn.Close();
                }
            }

        }
        public List<string> GetTables(DbConnection Conn)
        {
            throw new NotImplementedException();
        }
    }
}
