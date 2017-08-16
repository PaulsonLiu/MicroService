using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using iFramework.Util;
namespace iiERP.Common
{
    /// <summary>
    /// 数据类型转换帮助类
    /// </summary>
    public static class HMTConvertHelper
    {
        /// <summary>
        /// 将一个列表转换为树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyFunc"></param>
        /// <param name="parentKeyFunc"></param>
        /// <param name="displayFunc"></param>
        /// <param name="sortFunc"></param>
        /// <returns></returns>
        public static IEnumerable<TreeModel<T>> ListToTree<T>(this IEnumerable<T> list, Func<T, string> keyFunc, Func<T, string> parentKeyFunc, Func<T, string> displayFunc, Func<T, int?> sortFunc = null)
        {
            if (list != null)
            {

                var listResultTreeModels = new List<TreeModel<T>>();
                var resultDicTreeModels = list.Select(m =>
                {
                    var treeModel = new TreeModel<T>() { };
                    treeModel.Id = keyFunc(m);
                    treeModel.ParentId = parentKeyFunc(m);
                    treeModel.Text = displayFunc(m);
                    treeModel.Model = m;
                    if (sortFunc != null)
                    {
                        treeModel.Sort = sortFunc(m);
                    }
                    return treeModel;
                }
                    ).ToDictionary(m => m.Id);

                foreach (var key in resultDicTreeModels.Keys)
                {
                    var currentModel = resultDicTreeModels[key];
                    var parentKey = currentModel.ParentId;
                    if (string.IsNullOrWhiteSpace(parentKey) == false && resultDicTreeModels.ContainsKey(parentKey) && currentModel.Handled == false)
                    {
                        resultDicTreeModels[parentKey].Children.Add(currentModel);
                        currentModel.Handled = true;
                    }
                    else
                    {
                        if (currentModel.Handled == false)
                        {
                            currentModel.Handled = true;
                            yield return currentModel;
                        }
                    }
                }
            }
            yield break;
        }


        /// <summary>
        /// 将一个列表转换为树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyFunc"></param>
        /// <param name="parentKeyFunc"></param>
        /// <param name="displayFunc"></param>
        /// <param name="sortFunc"></param>
        /// <returns></returns>
        public static IEnumerable<TreeModel> ListToTreeModel<T>(this IEnumerable<T> list, Func<T, string> keyFunc, Func<T, string> parentKeyFunc, Func<T, string> displayFunc, Func<T, int?> sortFunc = null)
        {
            if (list != null)
            {

                var listResultTreeModels = new List<TreeModel>();
                var resultDicTreeModels = list.Select(m =>
                    {
                        var treeModel = new TreeModel() { };
                        treeModel.id = keyFunc(m);
                        treeModel.parentId = parentKeyFunc(m);
                        treeModel.text = displayFunc(m);
                        if (sortFunc != null)
                        {
                            treeModel.sort = sortFunc(m);
                        }
                        return treeModel;
                    }
                    ).ToDictionary(m => m.id);

                foreach (var key in resultDicTreeModels.Keys)
                {
                    var currentModel = resultDicTreeModels[key];
                    var parentKey = currentModel.parentId;
                    if (string.IsNullOrWhiteSpace(parentKey) == false && resultDicTreeModels.ContainsKey(parentKey) && currentModel.handlered == false)
                    {
                        resultDicTreeModels[parentKey].children.Add(currentModel);
                        currentModel.handlered = true;
                    }
                    else
                    {
                        if (currentModel.handlered == false)
                        {
                            currentModel.handlered = true;
                            yield return currentModel;
                        }
                    }
                }
            }
            yield break;
        }

        /// <summary>
        /// 对树形结构进行排序
        /// </summary>
        /// <param name="treeModels"></param>
        /// <returns></returns>
        public static IEnumerable<TreeModel<T>> SortTree<T>(this IEnumerable<TreeModel<T>> treeModels)
        {
            foreach (var item in treeModels)
            {
                item.Children = item.Children.SortTree().ToList();
            }
            return treeModels.OrderBy(m => m.Sort).ToList();
        }

        /// <summary>
        /// 对树形结构进行排序
        /// </summary>
        /// <param name="treeModels"></param>
        /// <returns></returns>
        public static IEnumerable<TreeModel> SortTreeModel(this IEnumerable<TreeModel> treeModels)
        {
            foreach (var item in treeModels)
            {
                item.children = item.children.SortTreeModel().ToList();
            }
            return treeModels.OrderBy(m => m.sort).ToList();
        }
        /// <summary>
        /// 将数据库中字段的值转换成自定义的枚举类型ControlType
        /// </summary>
        /// <param name="hmtCtrlType"></param>
        /// <returns></returns>
        public static ControlType ToControlType(string hmtCtrlType)
        {
            if (!string.IsNullOrEmpty(hmtCtrlType))
            {
                var controlType = hmtCtrlType;
                if (controlType.StartsWith("AU5004_HMT"))
                {
                    controlType = hmtCtrlType.Trim().Substring(10).ToUpper();

                }
                switch (controlType)
                {
                    case "TIMEPICKER":
                    case "HMTTIMEPICKER": return ControlType.HMTTIMEPICKER;
                    case "COMBOTREE":
                    case "HMTCOMBOTREE": return ControlType.HMTCOMBOTREE;
                    case "PASSWORD":
                    case "HMTPASSWORD":
                        return ControlType.HMTPASSWORD;
                    case "COMBOBOX":
                    case "HMTCOMBOBOX":
                        return ControlType.ComboBox;
                    case "DATETIME":
                    case "HMTDATEPICKER":
                    case "DATEPICKER":
                    case "HMTDATETIME":
                        return ControlType.DatePicker;
                    case "DATETIMEPICKER":
                    case "HMTDATETIMEPICKER":
                        return ControlType.DatetimePicker;
                    case "HMTDATEBETWEEN":
                        return ControlType.HMTDATEBETWEEN;
                    case "HMTNUMBERBETWEEN":
                        return ControlType.HMTNUMBERBETWEEN;
                    case "DROWDOWNLIST":
                    case "HMTDROWDOWNLIST":
                        return ControlType.DropDownList;
                    case "HMTNUMBERBOX":
                    case "NUMBERBOX":
                        return ControlType.NumberBox;
                    case "LABEL":
                    case "HMTLABEL":
                        return ControlType.Label;
                    case "TEXTBOX":
                    case "HMTTEXTBOX":
                        return ControlType.TextBox;
                    case "CHECKBOX":
                    case "HMTCHECKBOX":
                        return ControlType.CheckBox;
                    case "CHECKBOXGROUP":
                        return ControlType.CheckBoxGroup;
                    case "RADIO":
                        return ControlType.Radio;
                    case "RADIOGROUP":
                        return ControlType.RadioGroup;
                    case "TEXTAREA":
                    case "HMTTEXTAREA":
                        return ControlType.TextArea;
                    case "RICHTEXTBOX":
                        return ControlType.RichText;
                    case "HMTXML": return ControlType.TextBox;
                    case "HMTIMAGE": return ControlType.Label;
                    case "HMTDIALOG": return ControlType.HMTDialog;
                    case "DIALOG": return ControlType.HMTDialog;
                    case "EMAIL": return ControlType.Email;
                    case "FILEHANDLER": return ControlType.HMTFileHandler;
                    case "NULL": return ControlType.Label;
                }
            }
            return ControlType.None;
        }
    
        /// <summary>
        /// 将数据库中字段的值转换成System.Data.DbType枚举
        /// </summary>
        /// <param name="dbFieldValue"></param>
        /// <returns></returns>
        public static DbType ToDbType(string dbFieldValue)
        {
            if (!string.IsNullOrWhiteSpace(dbFieldValue))
            {
                var dbFldValue = dbFieldValue;
                if (dbFieldValue.StartsWith("AU5003_"))
                {
                    dbFieldValue = dbFieldValue.Substring(7);
                }
                //au5003_datetime au5003_image au5003_int au5003_nchar au5003_ntext au5003_numeric au5003_nvarchar au5003_unknown au5003_richtext au5003_bool
                switch (dbFldValue.ToLower())
                {
                    case "nvarchar":
                        return DbType.String;
                    case "int":
                        return DbType.Int32;
                    case "datetime":
                        return DbType.DateTime;
                    case "image":
                        return DbType.Object;
                    case "nchar":
                        return DbType.String;
                    case "ntext":
                        return DbType.String;
                    case "numeric":
                        return DbType.Decimal;
                    case "bool":
                        return DbType.Boolean;

                    case "bigint":
                        return DbType.Int64;
                    case "binary":
                        return DbType.Binary;

                    case "char":
                        return DbType.Byte;

                    case "decimal":
                        return DbType.Decimal;

                    case "nvarchar(Max)":
                        return DbType.String;
                    case "smallint":
                        return DbType.Int16;
                    case "sql_variant":
                        return DbType.Binary;
                    case "text":
                        return DbType.Xml;
                    case "tinyint":
                        return DbType.Byte;
                    case "uniqueidentifier":
                        return DbType.String;
                    case "varbinary":
                        return DbType.Binary;
                    case "varbinary(Max)":
                        return DbType.Binary; ;
                    case "varchar":
                        return DbType.String;
                    case "xml":
                        return DbType.Xml;
                    default:
                        return DbType.String;
                }
            }
            return DbType.String;
        }
        /// <summary>
        /// 数据库数据类型名称转化为csharp类型名称
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static string DbTypeToCsharpType(string dbName, bool nullable = false)
        /// <summary>
        {
            switch (dbName)
            {
                case "AU5003_IMAGE": return " byte[] ";
                case "AU5003_BOOL": return " bool ";
                case "INT":
                case "AU5003_INT": return " int ";// +(nullable ? "? " : " ");
                case "DATETIME":
                case "AU5003_DATETIME": return " DateTime" + (nullable ? "? " : " ");
                case "AU5003_NCHAR":
                case "NVARCHAR":
                case "XML":
                case "AU5003_NVARCHAR":
                case "AU5003_UNKNOWN":
                case "SS5019_STRING": return " string ";
                case "AU5003_NUMERIC": return " decimal ";// +(nullable ? "? " : " ");
                case "AU5003_NTEXT": return " string";
                default: return " string ";
            }
        }
  
        /// <summary>
        /// (通过实体)填充模型值
        /// </summary>
        /// <param name="modelEntry"></param>
        /// <param name="model"></param>
        public static void Fill(ModelEntry modelEntry, object model, bool ingoreNull = false)
        {
            if (model != null)
            {
                var plist = model.GetType().GetProperties();
                foreach (var item in plist)
                {
                    var fieldName = item.Name;
                    var itemValue = item.GetValue(model, null);
                    var stringValue = string.Empty;
                    if (itemValue != null)
                    {
                        stringValue = Convert.ToString(itemValue);
                    }
                    if (modelEntry != null)
                    {
                        if (ingoreNull && string.IsNullOrWhiteSpace(stringValue))
                        {
                            continue;
                        }
                        if (modelEntry.CurrentValues.PropertyValues.ContainsKey(fieldName))
                        {
                            modelEntry.CurrentValues.PropertyValues[fieldName] = stringValue;
                        }
                        else
                        {
                            modelEntry.CurrentValues.PropertyValues.Add(fieldName, stringValue);
                        }

                    }
                }
            }
        }
        /// <summary>
        /// 生成对应的模型并返回更新的字段列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelEntry"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static T ToModel<T>(ModelEntry modelEntry, out string[] fields)
        {
            var defaultInstance = Activator.CreateInstance<T>();
            var updateFields = defaultInstance.SetValue(modelEntry.CurrentValues.PropertyValues);
            fields = updateFields.ToArray();
            return defaultInstance;
        }
        /// <summary>
        /// 生成对应的模型并返回更新的字段列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelEntry"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static object ToModel(ModelEntry modelEntry, Type modeltype, out string[] fields)
        {
            var defaultInstance = Activator.CreateInstance(modeltype);
            var updateFields = defaultInstance.SetValue(modelEntry.CurrentValues.PropertyValues);
            fields = updateFields.ToArray();
            return defaultInstance;
        }

        public static SqlDbType ToSqlDbType(this string Value)
        {
            switch (Value.ToLower())
            {
                case "bigint":
                    return SqlDbType.BigInt;
                case "binary":
                    return SqlDbType.Binary;
                case "bit":
                    return SqlDbType.Bit;
                case "char":
                    return SqlDbType.Char;
                case "datetime":
                    return SqlDbType.DateTime;
                case "decimal":
                    return SqlDbType.Decimal;
                case "int":
                    return SqlDbType.Int;
                case "nchar":
                    return SqlDbType.NChar;
                case "numeric":
                    return SqlDbType.Decimal;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "nvarchar(Max)":
                    return SqlDbType.NVarChar;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "sql_variant":
                    return SqlDbType.VarBinary;
                case "text":
                    return SqlDbType.Text;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "varbinary(Max)":
                    return SqlDbType.VarBinary;
                case "varchar":
                    return SqlDbType.VarChar;
                case "xml":
                    return SqlDbType.Xml;
                default:
                    return SqlDbType.NVarChar;
            }
        }

        /// 对象换字符、可把NULL换为“”
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(this object obj, string failString = "")
        {
            if (obj == null)
            {
                return failString;
            }
            else
            {
                return obj.ToString();
            }
        }
        /// 对象换字符、可把NULL换为“”
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonValue(this object obj, string failString = "")
        {
            if (obj == null)
            {
                return failString;
            }
            else
            {
                return obj.ToString().ToLower();
            }
        }
        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj, int failInt = 0)
        {
            if (obj != null)
            {
                var dataType = obj.GetType();
                if (dataType == typeof(int) || dataType == typeof(int?))
                {
                    var intValue = Convert.ToInt32(obj);
                    return intValue;
                }
                if (dataType == typeof(long) || dataType == typeof(long?))
                {
                    var intValue = Convert.ToInt32(obj);
                    return intValue;
                }
                if (dataType == typeof(string))
                {
                    var strValue = obj.ToString().Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(strValue))
                    {
                        return failInt;
                    }
                    var intvalue = 0;
                    var status = int.TryParse(strValue, out intvalue);
                    if (status)
                    {
                        return intvalue;
                    }
                }
                return failInt;
            }
            else
            {
                return failInt;
            }

        }
        /// <summary>
        /// 转化为布尔类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="failvalue"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj, bool failvalue = false, string trueValueStr = "true,on", string falseValueStr = "false,off")
        {
            if (obj != null)
            {
                var dataType = obj.GetType();
                if (dataType == typeof(int) || dataType == typeof(int?))
                {
                    var intValue = Convert.ToInt32(obj);
                    return intValue > 0;
                }
                if (dataType == typeof(long) || dataType == typeof(long?))
                {
                    var intValue = Convert.ToInt64(obj);
                    return intValue > 0;
                }
                if (dataType == typeof(string))
                {
                    var strValue = obj.ToString().Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(strValue))
                    {
                        return false;
                    }
                    var intvalue = 0;
                    var status = int.TryParse(strValue, out intvalue);
                    if (status)
                    {
                        return intvalue > 0;
                    }
                    if (trueValueStr.Contains(strValue))
                    {
                        return true;
                    }
                    if (falseValueStr.Contains(strValue))
                    {
                        return false;
                    }

                }
            }
            return failvalue;
        }

    }
}
