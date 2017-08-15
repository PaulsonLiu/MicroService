using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using iiERP.Tools;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace iFramework.Util
{
    [Serializable]
    public class HMDbParameter
    {
        public DbType DbType { get; set; }
        public ParameterDirection Direction { get; set; }
        public bool IsNullable { get; set; }
        public string ParameterName { get; set; }
        public int Size { get; set; }
        public string SourceColumn { get; set; }
        public bool SourceColumnNullMapping { get; set; }
        public DataRowVersion SourceVersion { get; set; }
        public object Value { get; set; }
        public DatabaseType DatabaseType { get; set; }

        public DbParameter ToDbParameter()
        {
            switch (DatabaseType)
            {
                case Tools.DatabaseType.SqlServer:
                    return ToSqlParameter();
                case Tools.DatabaseType.Oracle:
                    return ToOracleParameter();
            }
            return null;
        }
        public static HMDbParameter New(DatabaseType DatabaseType, string PName, object Val,
            DbType DbType = System.Data.DbType.String, ParameterDirection Direction = ParameterDirection.Input,
            int Size = 50)
        {
            HMDbParameter theParam = new HMDbParameter();
            theParam.DbType = DbType;
            theParam.DatabaseType = DatabaseType;
            theParam.Direction = Direction;
            theParam.Value = Val;
            if (Val == DBNull.Value)
            {
                theParam.Value = null;
            }
            theParam.Size = Size;
            theParam.ParameterName = PName;
            theParam.SourceVersion = DataRowVersion.Current;
            theParam.SourceColumn = "";
            HandleParam(theParam);
            return theParam;
        }
        private HMDbParameter()
        {
            this.DbType = System.Data.DbType.String;
            Value = "";
            Direction = ParameterDirection.Input;
            IsNullable = true;
            Size = 50;
            SourceVersion = DataRowVersion.Current;
            DatabaseType = Tools.DatabaseType.SqlServer;
        }
        public SqlParameter ToSqlParameter()
        {
            SqlParameter theP = new SqlParameter();
            theP.DbType = DbType;
            theP.Direction = Direction;
            theP.IsNullable = IsNullable;
            theP.ParameterName = ParameterName;
            theP.Size = Size;
            theP.SourceColumn = SourceColumn;
            theP.SourceColumnNullMapping = SourceColumnNullMapping;
            theP.SourceVersion = SourceVersion;
            theP.Value = Value;
            if (theP.SqlDbType == SqlDbType.VarChar)
            {
                if (Value != null)
                {
                    theP.Value = Value.ToString();
                }
            }
            if(Value==null)
            {
                theP.Value = DBNull.Value;
            }
            
            return theP;
        }
        public OracleParameter ToOracleParameter()
        {

            OracleParameter theP = new OracleParameter();
            theP.DbType = DbType;
            theP.Direction = Direction;
            theP.IsNullable = IsNullable;
            theP.ParameterName = ParameterName;
            theP.Size = Size;
            theP.SourceColumn = SourceColumn;
            theP.SourceColumnNullMapping = SourceColumnNullMapping;
            theP.SourceVersion = SourceVersion;
            theP.Value = Value;
            if (Value == null)
            {
                theP.Value = DBNull.Value;
            }
            return theP;
        }
        public HMDbParameter Clone()
        {
            var theP = new HMDbParameter();
            theP.DbType = this.DbType;
            theP.Direction = this.Direction;
            theP.IsNullable = this.IsNullable;
            theP.ParameterName = this.ParameterName;
            theP.Size = this.Size;
            theP.SourceColumn = this.SourceColumn;
            theP.SourceColumnNullMapping = this.SourceColumnNullMapping;
            theP.SourceVersion = this.SourceVersion;
            theP.Value = this.Value;

            return theP;
        }

        private static void HandleParam(HMDbParameter Param)
        {
            if (Param.Value != null)
            {
                if (Param.Value is DateTime)
                {
                    Param.Value = ((DateTime)(Param.Value)).ToUtcDateTime(false);
                }
                if (Param.Value is DateTime?)
                {
                    Param.Value = ((DateTime?)(Param.Value)).Value.ToUtcDateTime(false);
                }
            }
        }

        public static HMDbParameter DbParameterToHM(DbParameter Parameter)
        {
            HMDbParameter theP = new HMDbParameter();
            theP.DbType = Parameter.DbType;
            theP.Direction = Parameter.Direction;
            theP.IsNullable = Parameter.IsNullable;
            theP.ParameterName = Parameter.ParameterName;
            theP.Size = Parameter.Size;
            theP.SourceColumn = Parameter.SourceColumn;
            theP.SourceColumnNullMapping = Parameter.SourceColumnNullMapping;
            theP.SourceVersion = Parameter.SourceVersion;
            
            theP.Value = Parameter.Value;
            if (Parameter.Value == DBNull.Value)
            {
                theP.Value = null;
            }
            return theP;
        }
        public static List<DbParameter> ToDbParameters(List<HMDbParameter> Parameters)
        {
            List<DbParameter> theResult = new List<DbParameter>();
            if (Parameters != null)
            {
                foreach (var theP in Parameters)
                {
                    theResult.Add(theP.ToDbParameter());
                }
            }
            return theResult;
        }
        public static List<HMDbParameter> ToHMDbParameters(List<DbParameter> Parameters)
        {
            List<HMDbParameter> theResult = new List<HMDbParameter>();
            if (Parameters != null)
            {
                foreach (var theP in Parameters)
                {
                    theResult.Add(HMDbParameter.DbParameterToHM(theP));
                }
            }
            return theResult;
        }
    }
}
