using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Reflection;

namespace MicroService.Models
{
    [Serializable]
    /// <summary>
    /// 商务模型的基类的定义
    /// </summary>
    public abstract class ModelBase : IModelBase
    {
        public ModelBase()
        {
            ReferenceDictionary = new NullableDictionary();
            FieldStyle = new NullableDictionary();
            FieldIcon = new NullableDictionary();
            ExtensionDictionary = new NullableDictionary();
            IgnoreFields = new List<string>();
            ErrorMessage = new Dictionary<string, string>();
            this.Fields = new List<string>();
            this.Actions = new Dictionary<string, bool>();
        }

        [XmlIgnore]
        [DataMember]
        public List<string> Fields { get; set; }
        [XmlIgnore]
        [DataMember]
        public List<string> IgnoreFields { get; set; }
        [XmlIgnore]

        //当前字段的图标

        [DataMember]
        public NullableDictionary FieldIcon { get; set; }
        [XmlIgnore]
        //当前字段的样式定义
        [DataMember]
        public NullableDictionary FieldStyle { get; set; }
        /// <summary>
        /// 时间状态
        /// </summary>
        [DataMember]
        [
        HMTField(Ingore = true)]
        public TimeZoneSts TimeSts { get; set; }
        [XmlIgnore]
        [DataMember]
        public NullableDictionary ReferenceDictionary { get; set; }
        [XmlIgnore]
        [DataMember]
        public NullableDictionary ExtensionDictionary { get; set; }
        /// <summary>
        /// 当前的操作列表状态
        /// </summary>
        [XmlIgnore]
        [DataMember]
        public Dictionary<string, bool> Actions { get; set; }
        [XmlIgnore]
        /// <summary>
        /// 模型的状态
        /// </summary>
        [DataMember]
        public ModelState ModelState { get; set; }
        [XmlIgnore]
        [DataMember]
        public Dictionary<string, string> ErrorMessage { get; set; }

        //string IModelBase.ArchiveField
        //{
        //    get;
        //    set;
        //}
        public virtual string GetPrimaryKeyValue()
        {
            return "";
        }
        /// <summary>
        /// 设置更新字段的名称
        /// </summary>
        /// <param name="updateFields"></param>
        public virtual void SetModeifyFields(string[] updateFields)
        {
            if (updateFields != null)
            {
                this.Fields = this.Fields.Union(this.GetType().GetProperties().Where(W => updateFields.Contains(W.Name)).Select(S => S.Name)).ToList();
            }
        }
        /// <summary>
        /// 设置不要更新的字段
        /// </summary>
        /// <param name="updateFields"></param>
        public virtual void SetIngoreFields(params string[] ingoreFields)
        {
            if (ingoreFields != null)
            {
                this.IgnoreFields = this.IgnoreFields.Union(this.GetType().GetProperties()
                    .Where(W => ingoreFields.Contains(W.Name)).Select(S => S.Name)).ToList();
            }

        }
        /// <summary>
        /// 得到当前字段的显示值
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="defaultValue">默认显示</param>
        /// <returns></returns>
        public string GetDisplayText(string fieldName, string defaultValue = null)
        {
            if (this.ReferenceDictionary.ContainsKey(fieldName))
            {
                return this.ReferenceDictionary[fieldName];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 取所有字段
        /// </summary>
        /// <returns></returns>
        public string[] GetFields()
        {

            return this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).Select(s => s.Name).ToArray();
        }

        public virtual void SetPrimaryKeyValue(string PK = "", bool force = false)
        {
            if (string.IsNullOrWhiteSpace(PK))
            {
                PK = Guid.NewGuid().ToString();
            }

            var primaryKeyField = this.GetType().GetProperties().Where(m =>
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
                var keyValue = primaryKeyField.GetValue(this, null);
                if (string.IsNullOrWhiteSpace(ConvertHelper.ToString(keyValue)) || (force & string.IsNullOrWhiteSpace(ConvertHelper.ToString(keyValue)) == false))
                {
                    primaryKeyField.SetValue(this, PK, null);
                }
            }
        }

        /// <summary>
        /// 通过Model取要copy的字段并转为Dictionary
        /// </summary>
        /// <param name="ignoreFields"></param>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetCopyDictionaryByModel(List<string> ignoreFields = null)
        {
            Dictionary<string, string> DicModel = new Dictionary<string, string>();

            var pros = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).ToList();
            if (ignoreFields != null)
            {
                pros = pros.Where(w => !ignoreFields.Contains(w.Name)).ToList();
            }

            foreach (var item in pros)
            {
                bool bAdd = true;
                var fieldAttributeList = item.GetCustomAttributes(typeof(HMTFieldAttribute), true);
                if (fieldAttributeList != null && fieldAttributeList.Length > 0)
                {
                    var PrimaryKey = (fieldAttributeList[0] as HMTFieldAttribute).PrimaryKey;
                    if (PrimaryKey == true)
                    {
                        bAdd = false;
                    }
                }

                if (bAdd)
                {
                    string[] ignore = new string[] { "CRT_USR_RK", "CRT_TIME", "CHG_USR_RK", "_CHG_TIME" };
                    var hasIgnore = ignore.Where(w => item.Name.IndexOf(w) >= 0).FirstOrDefault();
                    if (hasIgnore == null)
                    {
                        DicModel.Add(item.Name, ConvertHelper.ToString(item.GetValue(this, null)));
                    }
                }
            }
            return DicModel;
        }

        public void SetAction(string action, bool status)
        {
            if (this.Actions.ContainsKey(action))
            {
                this.Actions[action] = status;
            }
            else
            {
                this.Actions.Add(action, status);
            }
        }


        public bool IsSelected { get; set; }

        public virtual string GetPartitionID()
        {
            string thePrefix = this.GetType().Name.Substring(0, 6) + "_";

            var theObjVal = this.GetValueByPropertyName(thePrefix.ToUpper() + "PARTITION_ID");
            if (theObjVal != null)
            {
                return theObjVal.ToString();
            }
            return "";
        }
        private static Dictionary<string, Dictionary<string, string>> _TableMappingCodes = null;
        static ModelBase()
        {
            _TableMappingCodes = new Dictionary<string, Dictionary<string, string>>();
        }
        private static Dictionary<string, string> GetFieldMappings(Type AType)
        {
            Dictionary<string, string> theResults = new Dictionary<string, string>();
            var fieldList = AType.GetProperties();
            foreach (var item in fieldList)
            {
                var fieldAttributeList = item.GetCustomAttributes(typeof(HMTFieldAttribute), true);
                if (fieldAttributeList != null && fieldAttributeList.Length > 0)
                {
                    var mappingCode = (fieldAttributeList[0] as HMTFieldAttribute).MappingCode;
                    if (string.IsNullOrWhiteSpace(mappingCode) == false)
                    {
                        theResults.Add(item.Name, mappingCode);
                    }
                }

            }
            return theResults;
        }
        public Dictionary<string, string> GetFieldMappingCodes()
        {
            return GetFieldMappingCodes(this.GetType());
        }
        public static Dictionary<string, string> GetFieldMappingCodes(Type AType)
        {
            if (AType == null)
            {
                return null;
            }
            var theName = AType.Name;
            if (_TableMappingCodes.ContainsKey(theName))
            {
                return _TableMappingCodes[theName];
            }
            lock (typeof(ModelBase))
            {
                if (_TableMappingCodes.ContainsKey(theName))
                {
                    return _TableMappingCodes[theName];
                }
                var theMapping = GetFieldMappings(AType);
                _TableMappingCodes.Add(theName, theMapping);
                return theMapping;
            }
        }
    }
}
