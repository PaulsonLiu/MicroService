using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Models;
using MicroService.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Newtonsoft.Json.Schema;
using Microsoft.Data;
using System.ComponentModel.DataAnnotations;
using NLog;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MicroService.Business
{
    public class BusinesBase<Model>: IDisposable,IBusinessBase
        where Model :ModelBase, new()
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

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
            return new iiDbContext();
        }

        private iiDbContext dbContext;

        public iiDbContext DbContext
        {
            get
            {
                if (this.dbContext == null)
                {
                    this.dbContext = this.CreateDbContext();
                }

                return dbContext;
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

        private void _Dispose(bool v)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// 新增一条表记录
        /// </summary>
        /// <param name="model"></param>
        protected virtual Model _Add(Model model)
        {
            //string Msg = "";
            SetPrimaryKeyValue(model);

            List<ValidationResult> validResult;
            if (!this.Valid(model, out validResult))
            {
                StringBuilder err = new StringBuilder();
                foreach (var item in validResult)
                {
                    err.Append(item.ErrorMessage + "\r\n");
                }
                throw new Exception(err.ToString());
            }
            if (this.DbContext.Entry(model).State == EntityState.Detached)
            {
                this.DbContext.Set<Model>().Attach(model);
            }

            this.DbContext.Entry(model).State = EntityState.Added;

            try
            {
                this.DbContext.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                //model.ErrorMessage.Add("IN0005", dbEx.Message);
                Logger.Error(dbEx.Message);
                throw new ValidationException(dbEx.Message);
            }

            this.DbContext.Entry(model).State = EntityState.Detached;
            return model;
        }

        protected virtual void SetPrimaryKeyValue(Model model)
        {
            var primaryKeyField = model.GetType().GetProperties().Where(m =>
            {
                var fieldAttribute = m.GetCustomAttributes(typeof(HMTFieldAttribute), true);
                if (fieldAttribute != null && fieldAttribute.Count() > 0)
                {
                    return (fieldAttribute.FirstOrDefault() as HMTFieldAttribute).PrimaryKey;
                }
                else
                {
                    return false;
                }
            }).FirstOrDefault();

            if (primaryKeyField != null)
            {
                var keyValue = primaryKeyField.GetValue(model, null) as string;
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    primaryKeyField.SetValue(model, Guid.NewGuid().ToString(), null);

                }
            }
        }

        /// <summary>
        /// 判断模型是否合法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelValidResult"></param>
        /// <returns></returns>
        public bool Valid(Model model, out List<ValidationResult> modelValidResult)
        {
            return this._Valid(model, out modelValidResult);
        }

        protected bool _Valid(Model model, out List<ValidationResult> ValidResults)
        {
            if (model == null)
            {
                ValidResults = null;
                return false;
            }

           var entity = this.DbContext.Entry(model);

            var entities = (from entry in this.DbContext.ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
            {
                ValidResults = validationResults;
                return false;
            }
            ValidResults = null;
            return true;
        }
        private IQueryable<T> _GetList<T>(params string[] includes)
            where T : class, new()
        {
            if (includes != null && includes.Length > 0)
            {
                var queryResult = this.DbContext.Set<T>().AsNoTracking();
                foreach (var item in includes)
                {
                    queryResult = queryResult.Include(item);
                }
                return queryResult;
            }
            else
            {

                return this.DbContext.Set<T>().AsNoTracking();
            }
        }


    }
}
