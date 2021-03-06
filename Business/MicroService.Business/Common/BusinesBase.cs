﻿using System;
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
using MicroService.Common.Server;
using iFramework.Util;
using System.Linq.Expressions;

namespace MicroService.Business
{
    public class BusinesBase<Model> : IDisposable, IBusinessBase
        where Model : class, new()
    {
        #region Property
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Ctor
        public BusinesBase()
        {
        }

        public BusinesBase(iiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

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
        public void SetDbContext(iiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion DbContext

        /// <summary>
        /// 新增一条表记录
        /// </summary>
        /// <param name="model"></param>
        public Model Add(Model model)
        {
            var modelType = model.GetType();
            var keyProperty = GetPrimaryKeyProperty(model);
            keyProperty.SetValue(model, Guid.NewGuid().ToString().ToUpper());

            this.SetCreatedTime(model);
            this.SetChangedUsr(model);
            this.SetChangedTime(model);
            this.SetCreatedUsr(model);
            this.SetBuzRk(model);

            DataConverter.ConventLocalToUTCTime(model);

            this._Add(model);
            return model;
        }

        public object Add(object model)
        {
            if (model != null && model is Model)
            {
                return (Model)this.Add(model as Model);
            }
            else
            {
                return model;
            }
        }

        public int BulkCopy(bool DoTimeTrans, DataTable ADataTable, int NotifyAfter = 0, SqlRowsCopiedEventHandler RowsCopied = null, bool UseTransaction = false)
        {
            throw new NotImplementedException();
        }

        public object Copy(object model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="model"></param>
        public Model Copy(Model model)
        {
            return this._Copy(model);
        }
        /// <summary>
        /// 通过查询复制数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IEnumerable<Model> CopyByArgs(BusinessArgs args)
        {
            return this._CopyByArgs(args);
        }

        /// <summary>
        /// 通过查询进行复制
        /// </summary>
        /// <param name="args">复制参数</param>
        /// <returns></returns>
        protected virtual IEnumerable<Model> _CopyByArgs(BusinessArgs args)
        {
            var sourceList = this.GetListByParameter(args.FilterArgs, new PagingArgs(), new SortArgs());
            if (sourceList != null && sourceList.ModelList != null)
            {
                foreach (var item in sourceList.ModelList)
                {
                    yield return this._Copy(item, args.DefaultValues);
                }
            }
        }

        /// <summary>
        /// 获取模型值通过参数获取
        /// </summary>
        /// <param name="filterArgs">过滤参数</param>
        /// <param name="pageArgs">分页参数</param>
        /// <param name="sortArgs">排序参数</param>
        /// <returns></returns>
        protected virtual QueryResult<Model> _GetListByParameter(FilterArgs filterArgs, PagingArgs pageArgs, SortArgs sortArgs)
        {
            return _GetListByParameterEx(filterArgs, pageArgs, sortArgs, "");
        }

        protected virtual QueryResult<Model> _GetListByParameterSQL(FilterArgs filterArgs, PagingArgs pageArgs, SortArgs sortArgs, string SelectSQL)
        {
            return _GetListByParameterEx(filterArgs, pageArgs, sortArgs, SelectSQL);
        }
        private QueryResult<Model> _GetListByParameterEx(FilterArgs filterArgs, PagingArgs pageArgs, SortArgs sortArgs, string SelectSQL)
        {
            //在这里处理查询的分组情况(数据分组处理)
            //HandleFilterArgs(filterArgs);

            QueryResult<Model> resultModels = new QueryResult<Model>();
            IEnumerable<DbParameter> dbparamters = null;
            if (pageArgs != null && pageArgs.RequireTotalCount)
            {
                string countSql = this.QueryBuilder.ToCountSql(filterArgs, SelectSQL, out dbparamters);
                //int? totalCount = this.DbContext.Database.SqlQuery<int>(countSql, dbparamters.ToArray()).FirstOrDefault();
                int? totalCount = this.DbContext.Database.ExecuteSqlCommand(countSql, dbparamters.ToArray()); //Change by paulson liu -2017.8.16

                resultModels.TotalCount = totalCount == null ? 0 : (int)totalCount;

            }

            string querySqlString = this.QueryBuilder.ToQuerySql(filterArgs, sortArgs, pageArgs, SelectSQL, out dbparamters);
            //resultModels.ModelList = this.DbContext.Set<Model>()
            //    .SqlQuery(querySqlString, dbparamters.ToArray())
            //    .ToList();
            resultModels.ModelList = this.DbContext.Set<Model>()
                .FromSql(querySqlString, dbparamters.ToArray())
                .ToList(); //Change by paulson liu -2017.8.16

            return resultModels;
        }

        public virtual IQueryable<Model> GetList(Expression<Func<Model, bool>> predicate)
        {
            IQueryable<Model> results = null;
            Type contexType = dbContext.GetType();

            Type targetType = predicate.GetType();
            PropertyInfo info = contexType.GetProperty(predicate.Parameters[0].Type.Name);

            if (info != null)
            {
                var entity = info.GetValue(dbContext, null) as IQueryable<Model>;
                results = entity.Where(predicate).AsNoTracking();
                foreach (var item in results)
                {
                    DataConverter.ConventUTCToLocalTime(item);
                }
            }
            return results;
        }
        /// <summary>
        /// 获取模型值通过参数获取
        /// </summary>
        /// <param name="filterArgs">过滤参数</param>
        /// <param name="pageArgs">分页参数</param>
        /// <param name="sortArgs">排序参数</param>
        /// <returns></returns>
        public QueryResult<Model> GetListByParameter(FilterArgs filterArgs, PagingArgs pageArgs, SortArgs sortArgs, string SelectSQL = "")
        {
            var begin = HMTDateTime.Now;

            QueryResult<Model> queryResult;
            if (string.IsNullOrEmpty(SelectSQL))
            {
                queryResult = this._GetListByParameter(filterArgs, pageArgs, sortArgs);
            }
            else
            {
                queryResult = this._GetListByParameterSQL(filterArgs, pageArgs, sortArgs, SelectSQL);
            }
            var se = HMTDateTime.Now;
            var end = begin - se;
            if (queryResult != null && queryResult.ModelList != null)
            {
                DateTimeHelper.OToViewDateTimeE(queryResult.ModelList);
                if (filterArgs.RequireReference)
                {
                    this.SetReferenceList(queryResult.ModelList);
                }
            }
            var last = HMTDateTime.Now - se;
            return queryResult;
        }

        /// <summary>
        /// 不做参照直接获取数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Model GetModelById(params object[] keys)
        {
            var model = this._GetModelById(keys);
            if (model != null)
            {
                this.DbContext.Entry(model).State = EntityState.Detached;
            }
            return model;
        }

        /// <summary>
        /// 通过主键复制一份数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Model CopyById(string key)
        {
            var model = this.GetModelById(key);
            if (model != null)
            {
                var newModel = this._Copy(model);
                DateTimeHelper.OToViewDateTime(newModel);
                this._SetReference(newModel);
                return newModel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="model"></param>
        protected virtual Model _Copy(Model model, Dictionary<string, string> defaultValues = null)
        {
            //var modelEntry= this.DbContext.Entry(model);
            //var objectEntry = this.DbContext.ObjectContext.ObjectStateManager.GetObjectStateEntry(model);
            ////foreach (var keyItem in objectEntry.EntityKey.EntityKeyValues)
            ////{
            ////    keyItem.Value = Guid.NewGuid().ToString();
            ////}
            //objectEntry.ChangeState(EntityState.Detached);
            //objectEntry.State = EntityState.Detached;
            if (model is ModelBase)
            {
                (model as ModelBase).SetPrimaryKeyValue(Guid.NewGuid().ToString(), true);
            }
            if (defaultValues != null)
            {
                model.SetValue(defaultValues);
            }
            this.SetCreatedUsr(model);
            this.SetCreatedTime(model);
            //this.SetChangedUsr(model);
            this.SetChangedTime(model);
            //this.SetVersion(model);

            return model;
        }

        public object Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(object model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除一条表记录
        /// </summary>
        /// <param name="model"></param>
        public void Delete(Model model)
        {
            this._Delete(model);
        }

        /// <summary>
        /// 删除一条表记录
        /// </summary>
        /// <param name="model"></param>
        protected virtual void _Delete(Model model)
        {
            if (this.dbContext.Entry(model).State == EntityState.Detached)
            {
                this.dbContext.Set<Model>().Attach(model);
            }
            this.dbContext.Entry(model).State = EntityState.Deleted;
            this.dbContext.SaveChanges();
            this.dbContext.Entry(model).State = EntityState.Detached;
        }

        /// <summary>
        /// 删除一条含有多个主键字段的表的记录
        /// </summary>
        /// <param name="keys">记录的主键值集合</param>
        public void DeleteById(params object[] keys)
        {
            this._DeleteById(keys);
        }
        /// <summary>
        /// 删除一条含有多个主键字段的表的记录
        /// </summary>
        /// <param name="model">记录的主键值集合</param>
        protected virtual void _DeleteById(params object[] keys)
        {
            var model = this.dbContext.Set<Model>().Find(keys);
            if (model != null)
            {
                this.Delete(model);
            }
        }
        public void DeleteByArgs(FilterArgs args)
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

        /// <summary>
        /// 更新一条表记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateFields">更新的字段列表</param>
        public Model Update(Model model, params string[] updateFields)
        {
            this.SetChangedTime(model);
            this.SetChangedUsr(model);
            EntityHelper.SetBuzRk(model);

            DataConverter.ConventLocalToUTCTime(model);

            IEnumerable<string> requireUpdateFields = updateFields;
            var modelModifyFields = EntityHelper.GetFields(model);
            if (requireUpdateFields == null)
            {
                requireUpdateFields = new string[] { };
            }
            if (modelModifyFields != null && requireUpdateFields.Count() == 0)
            {
                requireUpdateFields = requireUpdateFields.Union(modelModifyFields).ToArray();
            }

            //添加修改人 修改时间
            if (model.HasProperty(EntityHelper.GetChangedTimeName(model)))
            {
                requireUpdateFields = requireUpdateFields.Union(new string[] { EntityHelper.GetChangedTimeName(model) });
            }
            if (model.HasProperty(EntityHelper.GetBuzRkName(model)))
            {
                requireUpdateFields = requireUpdateFields.Union(new string[] { EntityHelper.GetBuzRkName(model) });
            }
            if (model.HasProperty(EntityHelper.GetChangedUsrName(model)))
            {
                requireUpdateFields = requireUpdateFields.Union(new string[] { EntityHelper.GetChangedUsrName(model) });
            }

            var newModel = this._Update(model, requireUpdateFields.ToArray());
            return newModel;
        }


        /// <summary>
        /// 更新一条表记录
        /// </summary>
        /// <param name="model"></param>
        protected virtual Model _Update(Model model, params string[] updateFields)
        {
            if (this.dbContext.Entry(model).State == EntityState.Detached)
            {
                this.dbContext.Set<Model>().Attach(model);
            }

            var modelEntity = this.dbContext.Entry(model);

            if (updateFields != null && updateFields.Length > 0)
            {
                foreach (var item in updateFields)
                {
                    if (!string.IsNullOrWhiteSpace(item) && model.HasProperty(item))
                    {
                        modelEntity.Property(item).IsModified = true;
                    }
                }
            }
            else
            {
                modelEntity.State = EntityState.Modified;
            }

            this.dbContext.SaveChanges();

            this.dbContext.Entry(model).Reload();
            this.dbContext.Entry(model).State = EntityState.Detached;
            return model;
        }

        /// <summary>
        /// 新增一条表记录
        /// </summary>
        /// <param name="model"></param>
        protected virtual Model _Add(Model model)
        {
            var modelType = model.GetType();
            if (this.dbContext.Entry(model).State == EntityState.Detached)
            {
                this.dbContext.Set<Model>().Attach(model);
            }

            this.dbContext.Entry(model).State = EntityState.Added;

            dbContext.SaveChanges();

            this.dbContext.Entry(model).State = EntityState.Detached;
            return model;
        }

        /// <summary>
        /// 设置主键GUID值
        /// </summary>
        /// <param name="model"></param>
        protected virtual void SetPrimaryKeyValue(Model model)
        {
    //        var property = dbEntry.Entity.GetType().GetProperties().FirstOrDefault(
    //p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any());

    //        if (property == null)
    //            throw new InvalidOperationException(string.Format(
    //                "Entity {0} has no [Key] attribute.", dbEntry.Entity.GetType().Name));

    //        string keyName = property.Name;

            var primaryKeyField = model.GetType().GetProperties().Where(m =>
            {
                var fieldAttribute = m.GetCustomAttributes(typeof(KeyAttribute), true);
                if (fieldAttribute != null && fieldAttribute.Count() > 0)
                {
                    return (fieldAttribute.FirstOrDefault() as KeyAttribute).PrimaryKey;
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

        private void SetVersion(Model model)
        {
            //string sRelease = "";
            //string sPatch = "";
            //string sIteration = "";

            //sRelease = string.Format("{0}_RELEASE", GetModelName(model));
            //sPatch = string.Format("{0}_PATCH", GetModelName(model));
            //sIteration = string.Format("{0}_ITERATION", GetModelName(model));

            //if (!string.IsNullOrWhiteSpace(sRelease))
            //{
            //    if (model.HasProperty(sRelease) && model.HasProperty(sPatch) && model.HasProperty(sIteration))
            //    {
            //        int nRelease;
            //        int nPatch;
            //        int nIteration;

            //        string sql = @"SELECT * FROM SY0001_PARMS WHERE SY0001_PARAM_MSTR_RK = 'SY4011_NEXT_VERSION'
            //              AND SY0001_BU_RK ='-1' 
            //              AND SY0001_USR_RK = '-1'";

            //        var mParams = this.DatabaseContext.Set<SY0001_PARMS>().SqlQuery(sql).FirstOrDefault();

            //        if (mParams != null)
            //        {
            //            var sNextVersion = mParams.SY0001_PARAM_VALUE;
            //            var aNextVerson = sNextVersion.Split('.');
            //            if (aNextVerson.Length == 3)
            //            {
            //                nRelease = ConvertHelper.ToInt(aNextVerson[0]);
            //                nPatch = ConvertHelper.ToInt(aNextVerson[1]);
            //                nIteration = ConvertHelper.ToInt(aNextVerson[2]);

            //                model.SetValue(sRelease, nRelease);
            //                model.SetValue(sPatch, nPatch);
            //                model.SetValue(sIteration, nIteration);
            //            }
            //            else
            //            {
            //                throw new Exception("下一版本信息有问题");
            //            }
            //        }
            //    }
            //}
        }

        public virtual void SetReferenceList(List<Model> Models)
        {
            //if (Models == null || Models.Count <= 0)
            //{
            //    return;
            //}
            //var theTabName = typeof(Model).Name;
            //var theFieldMapping = MappingHelperProvider.GetFieldMappingInfo(theTabName, false);
            //foreach (var theF in theFieldMapping)
            //{
            //    if (theF.Value == "CMF_MAP_BY_FLD")
            //    {
            //        continue;
            //    }
            //    var theBuValKeys = new Dictionary<string, List<string>>();
            //    foreach (var theM in Models)
            //    {

            //        var theVKey = theM.GetValueByPropertyName(theF.Key);
            //        var theBuField = theTabName.Substring(0, 6) + "_BU_RK";
            //        if (theTabName.IsIn("SY0200_BU_MSTR", "SY0203_BU_REG"))
            //        {
            //            theBuField = theTabName.Substring(0, 6) + "_PK"; ;
            //        }
            //        else if (theTabName == "SY0117_BU_HANDSHAKING")
            //        {
            //            theBuField = "SY0117_SENDER_BU_RK"; ;
            //        }
            //        var theBuRK = theM.GetValueByPropertyName(theBuField).ToString();
            //        if (theVKey != null && theVKey.ToString() != "")
            //        {
            //            if (theBuValKeys.ContainsKey(theBuRK))
            //            {
            //                var theV = theVKey.ToString();
            //                if (theBuValKeys[theBuRK].Contains(theV) == false)
            //                {
            //                    theBuValKeys[theBuRK].Add(theV);
            //                }
            //            }
            //            else
            //            {
            //                var theV = theVKey.ToString();
            //                theBuValKeys.Add(theBuRK, new List<string>() { theV });
            //            }
            //        }
            //    }

            //    ReferenceHelper.DoPrepare(theF.Value, theBuValKeys);
            //}
            //foreach (var theModel in Models)
            //{
            //    SetReference(theModel);
            //}
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

        private Model _GetModelById(params object[] keys)
        {
            return this.DbContext.Set<Model>().Find(keys);
        }

        /// <summary>
        /// 方法执行前的操作
        /// </summary>
        /// <param name="methodName">执行方法</param>
        /// <param name="objectName">执行对象</param>
        /// <param name="tableName">关联表名</param>
        /// <param name="data">关联数据</param>
        protected virtual void OnExecuting(string methodName, string objectName, string tableName, Model data, string bu = null, string user = null, string Msg = "")
        {
            //if (data != null)
            //{
            //    if (string.IsNullOrEmpty(CfgParamMgmt.SysParamsMgmt["OpenDataLog"]) || CfgParamMgmt.SysParamsMgmt["OpenDataLog"] == "true")
            //    {
            //        SysLogMgmt.WriteUserLogInfo(XMLHelper.ToXml(UserActionLog<Model>.New(methodName, objectName, tableName, data, bu ?? UserInfo.GetUserInfo().BU, user ?? UserInfo.GetUserInfo().BU_User, Msg + "<=执行方法前的操作!"), Encoding.UTF8));
            //    }
            //}

        }


        /// <summary>
        /// 方法执行后的操作
        /// </summary>
        /// <param name="methodName">执行方法</param>
        /// <param name="objectName">执行对象</param>
        /// <param name="tableName">关联表名</param>
        /// <param name="data">关联数据</param>
        protected virtual void OnExecuted(string methodName, string objectName, string tableName, Model data, string bu = null, string user = null, string Msg = "")
        {
            //if (data != null)
            //{
            //    if (string.IsNullOrEmpty(CfgParamMgmt.SysParamsMgmt["OpenDataLog"]) || CfgParamMgmt.SysParamsMgmt["OpenDataLog"] == "true")
            //    {
            //        SysLogMgmt.WriteUserLogInfo(XMLHelper.ToXml(UserActionLog<Model>.New(methodName, objectName, tableName, data, bu ?? UserInfo.GetUserInfo().BU, user ?? UserInfo.GetUserInfo().BU_User, Msg + "<=执行方法后的操作!"), Encoding.UTF8));
            //    }
            //}

        }

        protected virtual QueryBuilder CreateQueryBuilder()
        {
            return new SqlEngineQueryBuilder(typeof(Model).Name);
        }

        #region QueryBuilder
        private QueryBuilder queryBuilder;

        public QueryBuilder QueryBuilder
        {
            get
            {
                if (queryBuilder == null)
                {
                    this.queryBuilder = this.CreateQueryBuilder();
                }
                return queryBuilder;
            }
        }
        #endregion

        protected PropertyInfo GetPrimaryKeyProperty(Model model)
        {
            IEnumerable<PropertyInfo> propertys = GetKeyPropertys(model);
            return propertys.FirstOrDefault(p => p.Name.Contains("_PK"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected IEnumerable<PropertyInfo> GetKeyPropertys(Model model)
        {
            var type = typeof(Model);

            var set = ((IObjectContextAdapter)_Contex).ObjectContext.CreateObjectSet<Model>();
            var entitySet = set.EntitySet;
            var keys = entitySet.ElementType.KeyMembers;
            var props = keys.Select(k => type.GetProperty(k.Name));

            return props;
        }

        /// <summary>
        /// 设置创建时间
        /// </summary>
        protected virtual void SetCreatedTime(Model model)
        {
            EntityHelper.SetCreatedTime(model);
        }

        /// <summary>
        /// 设置修改日期
        /// </summary>
        protected virtual void SetChangedTime(Model model)
        {
            EntityHelper.SetChangedTime(model);
        }

        /// <summary>
        /// 设置创建人
        /// </summary>
        protected virtual void SetCreatedUsr(Model model)
        {
            EntityHelper.SetCreatedUsr(model);
        }

        protected virtual void SetBuzRk(Model model)
        {
            EntityHelper.SetBuzRk(model);
        }

        /// <summary>
        /// 设置修改人
        /// </summary>
        protected virtual void SetChangedUsr(Model model)
        {
            EntityHelper.SetChangedUsr(model);
        }

        public object Update(object model, params string[] updateFieldNames)
        {
            throw new NotImplementedException();
        }
    }
}