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
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MicroService.Business
{
    public class BusinesBase<Model>: IDisposable,IBusinessBase
        where Model :class, new()
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

            ModelValidationResult validResult;
            if (!this.Valid(model, out validResult))
            {
                StringBuilder err = new StringBuilder();
                foreach (var item in validResult.ValidationErrors)
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
            catch (DbEntityValidationException dbEx)
            {
                model.ErrorMessage.Add("IN0005", dbEx.Message);
                SysLogMgmt.WriteErrorLog(dbEx.Message);
            }

            this.DbContext.Entry(model).State = EntityState.Detached;
            return model;
        }

        protected virtual void SetPrimaryKeyValue(Model model)
        {
            var primaryKeyField = model.GetType().GetProperties().Where(m =>
            {
                var fieldAttribute = m.GetCustomAttributes(typeof(HMTFieldAttribute), true);
                if (fieldAttribute != null && fieldAttribute.Length > 0)
                {
                    return (fieldAttribute[0] as HMTFieldAttribute).PrimaryKey;
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

        protected bool _Valid(Model model, out ModelValidationResult modelValidResult)
        {
            if (model == null)
            {
                modelValidResult = null;
                return false;
            }
            var trimFileds = model.GetType().GetProperties().Where(m => m.PropertyType == typeof(string) && m.CanWrite).ToArray();
            foreach (var item in trimFileds)
            {
                var fieldValue = item.GetValue(model, null);
                if (fieldValue != null)
                {
                    item.SetValue(model, fieldValue.ToString().TrimEnd(), null);
                }
            }
            var validResult = this.DbContext.Entry(model).GetValidationResult();
            modelValidResult = validResult.ToModelValidationResult();
            return modelValidResult.IsValid;
        }
        private IQueryable<T> _GetList<T>(params string[] includes)
            where T : class, new()
        {
            if (includes != null && includes.Length > 0)
            {
                var queryResult = (DbQuery<T>)this.DbContext.Set<T>().AsNoTracking();
                foreach (var item in includes)
                {
                    queryResult = (DbQuery<T>)queryResult.Include(item);
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
