using MicroService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Business
{
    /// <summary>
    /// 主要是应用与基类
    /// </summary>
    public interface IBusinessBase : IDisposable
    {
        List<object> SaveChanges(IEnumerable models);
        /// <summary>
        /// 增加一个模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        object Add(object model);

        /// <summary>
        /// 增加一个模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        object Copy(object model);

        /// <summary>
        /// 更新一个模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        object Update(object model, params string[] updateFieldNames);

        /// <summary>
        /// 删除一个模型
        /// </summary>
        /// <param name="objectId">当前的模型id</param>
        void DeleteById(params object[] objectIds);
        /// <summary>
        /// 通过条件进行删除
        /// </summary>
        /// <param name="args"></param>
        void DeleteByArgs(FilterArgs args);
        /// <summary>
        /// 删除一个模型
        /// </summary>
        /// <param name="model"></param>
        void Delete(object model);

        /// <summary>
        /// 判断数据库是否存在id为objectid的数据项
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        bool ExistByID(string Key);

        object Create();

        void SetDefaultValues(object model, string pagePk = null, Dictionary<string, string> defaultValues = null);

        int ExecCommandText(string SQL, List<System.Data.Common.DbParameter> Parameters = null);
        int BulkCopy(bool DoTimeTrans, DataTable ADataTable, int NotifyAfter = 0, SqlRowsCopiedEventHandler RowsCopied = null, bool UseTransaction = false);
        DataTable QueryByParam(string SQL, bool DoTimeTrans, List<System.Data.Common.DbParameter> Parameters = null);
        List<string> GetFromDataTable(string SQL, bool DoTimeTrans, List<System.Data.Common.DbParameter> Parameters = null);
    }
}
