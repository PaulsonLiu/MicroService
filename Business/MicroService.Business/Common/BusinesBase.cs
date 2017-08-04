using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Models;

namespace MicroService.Business
{
    public class BusinesBase<Model>: IDisposable,IBusinessBase
    {
        public BusinesBase()
        {
        }

        public BusinesBase(iiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void SetDbContext(iiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        #region DbContext
        /// <summary>
        /// 在需要时创建数据库上下文实例。
        /// </summary>
        /// <returns></returns>
        private iiDbContext CreateDbContext()
        {
            //2014-12-19 jian
            if (typeof(Model).Name.StartsWith("TR"))
            {
                return new iiDbContext(DatabaseMgmt.BuildConnectionString(typeof(Model).Name), 0);
            }
            else
            {
                return new iiDbContext();
            }
        }

        private iiDbContext dbContext;

        private iiDbContext DbContext
        {
            get
            {
                if (this.dbContext == null)
                {
                    this.dbContext = this.CreateDbContext();
                }

                //this.dbContext = this.CreateDbContext();
                return dbContext;
            }
        }

        public iiDbContext DatabaseContext
        {
            get
            {
                return this.DbContext;
            }
        }
        #endregion DbContext

        public object Add(object model)
        {
            throw new NotImplementedException();
        }

        public int BulkCopy(bool DoTimeTrans, DataTable ADataTable, int NotifyAfter = 0, SqlRowsCopiedEventHandler RowsCopied = null, bool UseTransaction = false)
        {
            throw new NotImplementedException();
        }

        public object Copy(object model)
        {
            throw new NotImplementedException();
        }

        public object Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(object model)
        {
            throw new NotImplementedException();
        }

        public void DeleteByArgs(FilterArgs args)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(params object[] objectIds)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
            this._Dispose(true);
        }

        public int ExecCommandText(string SQL, List<DbParameter> Parameters = null)
        {
            throw new NotImplementedException();
        }

        public bool ExistByID(string Key)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFromDataTable(string SQL, bool DoTimeTrans, List<DbParameter> Parameters = null)
        {
            throw new NotImplementedException();
        }

        public DataTable QueryByParam(string SQL, bool DoTimeTrans, List<DbParameter> Parameters = null)
        {
            throw new NotImplementedException();
        }

        public List<object> SaveChanges(IEnumerable models)
        {
            throw new NotImplementedException();
        }

        public void SetDefaultValues(object model, string pagePk = null, Dictionary<string, string> defaultValues = null)
        {
            throw new NotImplementedException();
        }

        public object Update(object model, params string[] updateFieldNames)
        {
            throw new NotImplementedException();
        }
    }
}
