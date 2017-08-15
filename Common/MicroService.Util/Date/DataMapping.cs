using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Text;
using System.Reflection;

namespace iFramework.Util
{
    /// <summary>
    /// 数据扩展类
    /// 说明    ：DataTable,DataReader转实体,List集合<br/>
    /// 作者    ：易小辉<br/>
    /// 创建时间：2011-8-16<br/>
    /// </summary>
    public class DataMapping
    {
        #region DataReader转实体
        /// <summary>
        /// DataReader转实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dr">DataReader对象</param>
        /// <returns></returns>
        public static T DataReaderToModel<T>(IDataReader dr) where T : new()
        {
            T t = new T();
            if (dr == null) { return default(T); }
            using (dr)
            {
                if (dr.Read())
                {
                    PropertyInfo[] pis = typeof(T).GetProperties();  //获取此实体的公共属性
                    List<string> DBFieldNameList = new List<string>(dr.FieldCount);
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        DBFieldNameList.Add(dr.GetName(i).ToLower());
                    }
                    Type attrType = typeof(ColumnAttribute);   //获取自定义字段名
                    foreach (PropertyInfo pi in pis)
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        string colName = pi.Name;
                        object[] attrs = pi.GetCustomAttributes(attrType, true);
                        if (attrs != null && attrs.Length > 0 && string.IsNullOrWhiteSpace(((ColumnAttribute)attrs[0]).Name) == false)
                        {
                            colName = ((ColumnAttribute)attrs[0]).Name;
                        }
                        //判断字段名是否存在
                        if (DBFieldNameList.Contains(colName.ToLower()) == false)
                        {
                            continue;
                        }
                        //判断此属性是否有Setter或columnName值是否为空
                        object value = dr[colName];
                        if (value is DBNull || value == DBNull.Value || value == null || pi.CanWrite == false)
                        {
                            continue;
                        }

                        #region SetValue
                        try
                        {
                            switch (pi.PropertyType.ToString())
                            {
                                case "System.String":
                                    pi.SetValue(t, Convert.ToString(value), null);
                                    break;
                                case "System.Char":
                                    pi.SetValue(t, Convert.ToChar(value), null);
                                    break;
                                case "System.Int64":
                                    pi.SetValue(t, Convert.ToInt64(value), null);
                                    break;
                                case "System.Int32":
                                    pi.SetValue(t, Convert.ToInt32(value), null);
                                    break;
                                case "System.UInt64":
                                    pi.SetValue(t, Convert.ToUInt64(value), null);
                                    break;
                                case "System.UInt32":
                                    pi.SetValue(t, Convert.ToUInt32(value), null);
                                    break;
                                case "System.DateTime":
                                    pi.SetValue(t, Convert.ToDateTime(value), null);
                                    break;
                                case "System.Boolean":
                                    pi.SetValue(t, Convert.ToBoolean(value), null);
                                    break;
                                case "System.Double":
                                    pi.SetValue(t, Convert.ToDouble(value), null);
                                    break;
                                case "System.Decimal":
                                    pi.SetValue(t, Convert.ToDecimal(value), null);
                                    break;
                                case "System.Single":
                                    pi.SetValue(t, Convert.ToSingle(value), null);
                                    break;
                                case "System.Byte":
                                    pi.SetValue(t, Convert.ToByte(value), null);
                                    break;
                                case "System.SByte":
                                    pi.SetValue(t, Convert.ToSByte(value), null);
                                    break;
                                default:
                                    pi.SetValue(t, value, null);
                                    break;
                            }
                        }
                        catch
                        {
                            //throw (new Exception(ex.Message));
                        }
                        #endregion
                    }
                }
            }
            return t;
        }
        #endregion

        #region DataReader转List<T>
        /// <summary>
        /// DataReader转List<T>
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dr">DataReader对象</param>
        /// <returns></returns>
        public static IList<T> DataReaderToList<T>(IDataReader dr) where T : new()
        {
            IList<T> tList = new List<T>();
            if (dr == null) { return tList; }
            PropertyInfo[] pis = typeof(T).GetProperties();  //获取此实体的公共属性
            List<string> DBFieldNameList = new List<string>(dr.FieldCount);
            for (int i = 0; i < dr.FieldCount; i++)
            {
                DBFieldNameList.Add(dr.GetName(i).ToLower());
            }
            Type attrType = typeof(ColumnAttribute);   //获取自定义字段名
            using (dr)
            {
                while (dr.Read())
                {
                    T t = new T();
                    foreach (PropertyInfo pi in pis)
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        string colName = pi.Name;
                        object[] attrs = pi.GetCustomAttributes(attrType, true);
                        if (attrs != null && attrs.Length > 0 && string.IsNullOrWhiteSpace(((ColumnAttribute)attrs[0]).Name) == false)
                        {
                            colName = ((ColumnAttribute)attrs[0]).Name;
                        }
                        //判断字段名是否存在
                        if (DBFieldNameList.Contains(colName.ToLower()) == false)
                        {
                            continue;
                        }
                        //判断此属性是否有Setter或columnName值是否为空
                        object value = dr[colName];
                        if (value is DBNull || value == DBNull.Value || value == null || pi.CanWrite == false)
                        {
                            continue;
                        }

                        #region SetValue
                        try
                        {
                            switch (pi.PropertyType.ToString())
                            {
                                case "System.String":
                                    pi.SetValue(t, Convert.ToString(value), null);
                                    break;
                                case "System.Char":
                                    pi.SetValue(t, Convert.ToChar(value), null);
                                    break;
                                case "System.Int64":
                                    pi.SetValue(t, Convert.ToInt64(value), null);
                                    break;
                                case "System.Int32":
                                    pi.SetValue(t, Convert.ToInt32(value), null);
                                    break;
                                case "System.UInt64":
                                    pi.SetValue(t, Convert.ToUInt64(value), null);
                                    break;
                                case "System.UInt32":
                                    pi.SetValue(t, Convert.ToUInt32(value), null);
                                    break;
                                case "System.DateTime":
                                    pi.SetValue(t, Convert.ToDateTime(value), null);
                                    break;
                                case "System.Boolean":
                                    pi.SetValue(t, Convert.ToBoolean(value), null);
                                    break;
                                case "System.Double":
                                    pi.SetValue(t, Convert.ToDouble(value), null);
                                    break;
                                case "System.Decimal":
                                    pi.SetValue(t, Convert.ToDecimal(value), null);
                                    break;
                                case "System.Single":
                                    pi.SetValue(t, Convert.ToSingle(value), null);
                                    break;
                                case "System.Byte":
                                    pi.SetValue(t, Convert.ToByte(value), null);
                                    break;
                                case "System.SByte":
                                    pi.SetValue(t, Convert.ToSByte(value), null);
                                    break;
                                default:
                                    pi.SetValue(t, value, null);
                                    break;
                            }
                        }
                        catch
                        {
                            //throw (new Exception(ex.Message));
                        }
                        #endregion
                    }
                    tList.Add(t);
                }
            }
            return tList;
        }
        #endregion

        #region DataRow转实体
        /// <summary>
        /// DataRow转实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dr">DataReader对象</param>
        /// <returns></returns>
        public static T DataRowToModel<T>(DataRow dr) where T : new()
        {
            T t = new T();
            if (dr == null) { return default(T); }
            PropertyInfo[] pis = typeof(T).GetProperties();  //获取此实体的公共属性            
            Type attrType = typeof(ColumnAttribute);   //获取自定义字段名
            foreach (PropertyInfo pi in pis)
            {
                if (!pi.CanWrite)
                {
                    continue;
                }
                string colName = pi.Name;
                object[] attrs = pi.GetCustomAttributes(attrType, true);
                if (attrs != null && attrs.Length > 0 && string.IsNullOrWhiteSpace(((ColumnAttribute)attrs[0]).Name) == false)
                {
                    colName = ((ColumnAttribute)attrs[0]).Name;
                }
                //判断字段名是否存在
                if (!dr.Table.Columns.Contains(colName))
                {
                    continue;
                }
                //判断此属性是否有Setter或columnName值是否为空
                object value = dr[colName];
                if (value is DBNull || value == DBNull.Value || value == null || pi.CanWrite == false)
                {
                    continue;
                }

                #region SetValue
                try
                {
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.String":
                            pi.SetValue(t, Convert.ToString(value), null);
                            break;
                        case "System.Char":
                            pi.SetValue(t, Convert.ToChar(value), null);
                            break;
                        case "System.Int64":
                            pi.SetValue(t, Convert.ToInt64(value), null);
                            break;
                        case "System.Int32":
                            pi.SetValue(t, Convert.ToInt32(value), null);
                            break;
                        case "System.UInt64":
                            pi.SetValue(t, Convert.ToUInt64(value), null);
                            break;
                        case "System.UInt32":
                            pi.SetValue(t, Convert.ToUInt32(value), null);
                            break;
                        case "System.DateTime":
                            pi.SetValue(t, Convert.ToDateTime(value), null);
                            break;
                        case "System.Boolean":
                            pi.SetValue(t, Convert.ToBoolean(value), null);
                            break;
                        case "System.Double":
                            pi.SetValue(t, Convert.ToDouble(value), null);
                            break;
                        case "System.Decimal":
                            pi.SetValue(t, Convert.ToDecimal(value), null);
                            break;
                        case "System.Single":
                            pi.SetValue(t, Convert.ToSingle(value), null);
                            break;
                        case "System.Byte":
                            pi.SetValue(t, Convert.ToByte(value), null);
                            break;
                        case "System.SByte":
                            pi.SetValue(t, Convert.ToSByte(value), null);
                            break;
                        default:
                            pi.SetValue(t, value, null);
                            break;
                    }
                }
                catch
                {
                    //throw (new Exception(ex.Message));
                }
                #endregion
            }
            return t;
        }
        #endregion

        #region DataTable转List<T>
        /// <summary>
        /// DataTable转List<T>
        /// </summary>
        /// <typeparam name="T">数据项类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns>List数据集</returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> tList = new List<T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return tList;
            }
            PropertyInfo[] pis = typeof(T).GetProperties();  //获取此实体的公共属性
            Type attrType = typeof(ColumnAttribute);   //获取自定义字段名
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo pi in pis)
                {
                    if (!pi.CanWrite)
                    {
                        continue;
                    }
                    string colName = pi.Name;
                    object[] attrs = pi.GetCustomAttributes(attrType, true);
                    if (attrs != null && attrs.Length > 0 && string.IsNullOrWhiteSpace(((ColumnAttribute)attrs[0]).Name) == false)
                    {
                        colName = ((ColumnAttribute)attrs[0]).Name;
                    }
                    //判断字段名是否存在                   
                    if (!dr.Table.Columns.Contains(colName))
                    {
                        continue;
                    }
                    //判断此属性是否有Setter或columnName值是否为空
                    object value = dr[colName];
                    if (value is DBNull || value == DBNull.Value || value == null || pi.CanWrite == false)
                    {
                        continue;
                    }

                    #region SetValue
                    try
                    {
                        switch (pi.PropertyType.ToString())
                        {
                            case "System.String":
                                pi.SetValue(t, Convert.ToString(value), null);
                                break;
                            case "System.Char":
                                pi.SetValue(t, Convert.ToChar(value), null);
                                break;
                            case "System.Int64":
                                pi.SetValue(t, Convert.ToInt64(value), null);
                                break;
                            case "System.Int32":
                                pi.SetValue(t, Convert.ToInt32(value), null);
                                break;
                            case "System.UInt64":
                                pi.SetValue(t, Convert.ToUInt64(value), null);
                                break;
                            case "System.UInt32":
                                pi.SetValue(t, Convert.ToUInt32(value), null);
                                break;
                            case "System.DateTime":
                                pi.SetValue(t, Convert.ToDateTime(value), null);
                                break;
                            case "System.Boolean":
                                pi.SetValue(t, Convert.ToBoolean(value), null);
                                break;
                            case "System.Double":
                                pi.SetValue(t, Convert.ToDouble(value), null);
                                break;
                            case "System.Decimal":
                                pi.SetValue(t, Convert.ToDecimal(value), null);
                                break;
                            case "System.Single":
                                pi.SetValue(t, Convert.ToSingle(value), null);
                                break;
                            case "System.Byte":
                                pi.SetValue(t, Convert.ToByte(value), null);
                                break;
                            case "System.SByte":
                                pi.SetValue(t, Convert.ToSByte(value), null);
                                break;
                            
                            default:
                                pi.SetValue(t, value, null);
                                break;
                        }
                    }
                    catch
                    {
                        //throw (new Exception(ex.Message));
                    }
                    #endregion
                }
                tList.Add(t);
            }
            return tList;
        }
        #endregion

    }
}
