using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Common;

namespace MicroService.Models
{
    /// <summary>
    /// 过滤条件结构
    /// 一般的过滤条件
    /// 根据关键字过滤
    /// </summary>
    [DataContract]
    public class FilterArgs
    {
        /// <summary>
        /// 当前的操作是新增查询
        /// </summary>
        public const string AddAction = "ADD";
        /// <summary>
        /// 主表或者主对象名称
        /// </summary>
        [DataMember]
        public string ObjectName { get; set; }
        /// <summary>
        /// 当前的页面pk
        /// </summary>
        [DataMember]
        public string PagePk { get; set; }

        /// <summary>
        /// 从哪个页面调用
        /// </summary>
        [DataMember]
        public string FromPagePk { get; set; }

        /// <summary>
        /// 当前的主键
        /// </summary>
        [DataMember]
        public string PKValue { get; set; }
        /// <summary>
        /// 参数查询的条件列表
        /// </summary>
        [DataMember]
        public List<FilterItem> FilterItems { get; set; }

        [DataMember]
        public string Where { get; set; }
        [DataMember]
        public bool NotNeedBuLimit { get; set; }

        /// <summary>
        /// 显示归档记录
        /// </summary>
        [DataMember]
        public bool ShowArchived { get; set; }

        /// <summary>
        /// 只显示归档记录
        /// </summary>
        [DataMember]
        public bool OnlyShowArchived { get; set; }

        /// <summary>
        /// 是否需要参照
        /// </summary>
        [DataMember]
        public bool RequireReference { get; set; }
        /// <summary>
        /// 扩展信息，可以传参数
        /// </summary>
        [DataMember]
        public Dictionary<string, string> ExtensionDictionary { get; set; }

        [DataMember]
        public string Action { get; set; }
        /// <summary>
        /// 查询要显示的字段名字列表 
        /// </summary>
        [DataMember]
        public List<string> FieldsName { get; set; }

        /// <summary>
        /// 可临时加表关系
        /// </summary>
        public List<Reference_info> ReferenceInfos { get; set; }

        public FilterArgs()
        {
            this.ExtensionDictionary = new Dictionary<string, string>();
            this.FilterItems = new List<FilterItem>();
            this.RequireReference = false;
            this.ReferenceInfos = new List<Reference_info>();
            this.ShowArchived = false;
            this.OnlyShowArchived = false;
        }

        public FilterArgs ToUtcValue(Func<DateTime, DateTime> timeChangeFunc)
        {
            foreach (var item in this.FilterItems)
            {
                item.ToUtcValue(timeChangeFunc);
            }
            return this;
        }
        public FilterArgs And(FilterItem item)
        {
            item.LogicOperation = LogicOperationType.And;
            this.FilterItems.Add(item);
            return this;
        }

        public FilterArgs Or(FilterItem item)
        {
            item.LogicOperation = LogicOperationType.Or;
            this.FilterItems.Add(item);
            return this;
        }

        public string GetCountFiled()
        {
            return " COUNT(*) ";
        }
        public string GetFiledNames()
        {
            if (this.FieldsName != null && this.FieldsName.Count > 0)
            {
                return string.Join(",", this.FieldsName.ToArray());
            }
            else
            {
                return "*";
            }
        }

        public string GetObjectKey()
        {
            if (this.ObjectName != null)
            {
                var nameParts = this.ObjectName.Split(new char[] { '_' });
                if (nameParts != null)
                {
                    var count = nameParts.Count();
                    if (count >= 2)
                    {
                        var realKeyPart = nameParts.Take(1).ToList();
                        realKeyPart.Add("PK");
                        return string.Join("_", realKeyPart.ToArray());
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 得到当前名字的过滤 值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetFilterValue(string name)
        {
            var filterItem = this.FilterItems.FirstOrDefault(m => m.ColumnName == name);
            if (filterItem != null)
            {
                return filterItem.Value1;
            }
            else
            {
                return "";
            }
        }
        public string ToWhereString(out Dictionary<string, string> Paramters)
        {
            Dictionary<string, string> dbList = new Dictionary<string, string>();
            Paramters = dbList;
            List<string> sqlList = new List<string>();
            foreach (var item in this.FilterItems)
            {
                var itemDictionary = new Dictionary<string, string>();
                sqlList.Add(item.ToWhere(out itemDictionary));
                foreach (var itemKey in itemDictionary.Keys)
                {
                    dbList.Add(itemKey, itemDictionary[itemKey]);
                }
            }
            return string.Join(" AND ", sqlList.Where(m => string.IsNullOrWhiteSpace(m) == false).ToArray());

        }
        public bool IsEmpty()
        {
            return this.FilterItems.All(m => m.IsEmpty());
        }
        public static FilterArgs Default
        {
            get
            {
                return new FilterArgs() { RequireReference = true };
            }
        }
        public static FilterArgs NullFilter
        {
            get
            {
                return new FilterArgs();
            }
        }
    }
}

public class Reference_info
{
    public string REFERING_TAB { get; set; }
    public string REFERING_FLD { get; set; }
    public string REFERED_FLD_RK { get; set; }
}
