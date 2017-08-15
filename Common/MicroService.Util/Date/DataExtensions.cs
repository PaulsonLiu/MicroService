using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace iFramework.Util
{
    /// <summary>
    /// 数据扩展类
    /// 说明    ：DataTable,DataReader转实体,List集合<br/>
    /// 作者    ：易小辉<br/>
    /// 创建时间：2010-4-7<br/>
    /// 最后修改：2011-3-10<br/>
    /// </summary>
    public class DataExtensions
    {

        #region DataRow转实体
        /// <summary>
        /// DataRow转实体
        /// </summary>
        /// <typeparam name="T">数据型类</typeparam>
        /// <param name="dr">DataRow</param>
        /// <returns>模式</returns>
        public static T DataRowToModel<T>(DataRow dr) where T : new()
        {
            //T t = (T)Activator.CreateInstance(typeof(T));
            T t = new T();
            if (dr == null) return default(T);
            PropertyInfo[] propertys = t.GetType().GetProperties(); //获取此实体的公共属性
            foreach (PropertyInfo pi in propertys)
            {
                if (!pi.CanWrite)
                {
                    continue;
                }
                string columnName = pi.Name;
                if (dr.Table.Columns.Contains(columnName))
                {
                    // 判断此属性是否有Setter或columnName值是否为空
                    object value = dr[columnName];
                    if (value is DBNull || value == DBNull.Value || value == null || !pi.CanWrite)
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
            PropertyInfo[] propertys = typeof(T).GetProperties();   //获取此实体的公共属性
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    if (!pi.CanWrite)
                    {
                        continue;
                    }
                    string columnName = pi.Name;
                    if (dr.Table.Columns.Contains(columnName))
                    {
                        // 判断此属性是否有Setter或columnName值是否为空
                        object value = dr[columnName];
                        if (value is DBNull || value == DBNull.Value || value == null || !pi.CanWrite)
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
                tList.Add(t);
            }
            return tList;
        }
        #endregion

        #region DataReader转实体
        /// <summary>
        /// DataReader转实体
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dr">DataReader</param>
        /// <returns>实体</returns>
        public static T DataReaderToModel<T>(IDataReader dr) where T : new()
        {
            T t = new T();
            if (dr == null)
            {
                return default(T);
            }
            using (dr)
            {
                if (dr.Read())
                {
                    PropertyInfo[] propertys = t.GetType().GetProperties(); //获取此实体的公共属性
                    List<string> DBFieldNameList = new List<string>(dr.FieldCount);
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        DBFieldNameList.Add(dr.GetName(i).ToLower());
                    }

                    foreach (PropertyInfo pi in propertys)
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        string columnName = pi.Name;
                        if (DBFieldNameList.Contains(columnName.ToLower()))
                        {
                            //判断此属性是否有Setter或columnName值是否为空
                            object value = dr[columnName];
                            if (value is DBNull || value == DBNull.Value || value == null || !pi.CanWrite)
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
            }
            dr.Close();
            return t;
        }
        #endregion

        #region DataReader转List<T>
        /// <summary>
        /// DataReader转List<T>
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dr">DataReader</param>
        /// <returns>List数据集</returns>
        public static List<T> DataReaderToList<T>(IDataReader dr) where T : new()
        {
            List<T> tList = new List<T>();
            if (dr == null)
            {
                return tList;
            }
            using (dr)
            {
                PropertyInfo[] propertys = typeof(T).GetProperties();    //获取此实体的公共属性
                List<string> DBFieldNameList = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    DBFieldNameList.Add(dr.GetName(i).ToLower());
                }
                while (dr.Read())
                {
                    T t = new T();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        string columnName = pi.Name;
                        if (DBFieldNameList.Contains(columnName.ToLower()))
                        {
                            // 判断此属性是否有Setter或columnName值是否为空
                            object value = dr[columnName];
                            if (value is DBNull || value == DBNull.Value || value == null || !pi.CanWrite)
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
                    tList.Add(t);
                }
            }
            dr.Close();
            return tList;
        }
        #endregion

        #region 泛型集合转DataTable
        /// <summary>
        /// 泛型集合转DataTable
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="entityList">泛型集合</param>
        /// <returns>DataTable</returns>
        public static DataTable ListToDataTable<T>(IList<T> entityList)
        {
            if (entityList == null) return null;
            DataTable dt = CreateTable<T>();
            Type entityType = typeof(T);
            //PropertyInfo[] properties = entityType.GetProperties();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in entityList)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyDescriptor property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        #endregion

        #region 创建DataTable的结构
        /// <summary>
        /// 创建表结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            //PropertyInfo[] properties = entityType.GetProperties();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            //生成DataTable的结构
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                dt.Columns.Add(prop.Name);
            }
            return dt;
        }
        #endregion


    }
}
