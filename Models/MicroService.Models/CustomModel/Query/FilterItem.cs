using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace MicroService.Models
{
    [DataContract]
    public class FilterItem : FilterItemBase
    {
        [DataMember]
        public string FilterId { get; set; }
        [DataMember]
        public string ObjectName { get; set; }

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        private DataType _DataType;
        [DataMember]
        public DataType DataType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.ObjectName) == false)
                {
                    try
                    {
                        var columnProperty = Type.GetType("iiERP.Model." + this.ObjectName).GetProperty(this.ColumnName);
                        if (columnProperty != null)
                        {
                            Type propertyType = columnProperty.PropertyType;
                            if (columnProperty.PropertyType.IsGenericParameter)
                            {
                                propertyType = columnProperty.PropertyType.GetGenericArguments()[0];
                            }
                            if (propertyType == typeof(string))
                            {
                                return Models.DataType.String;
                            }
                            if (propertyType == typeof(DateTime))
                            {
                                return Models.DataType.DateTime;
                            }
                            if (propertyType == typeof(int))
                            {
                                return Models.DataType.Number;
                            }
                            if (propertyType == typeof(Decimal))
                            {
                                return Models.DataType.Number;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return this._DataType;
                    }
                }
                return this._DataType;
            }
            set
            {
                this._DataType = value;
            }
        }

        [DataMember]
        public List<string> Values { get; set; }

        [DataMember]
        public bool GroupFilter { get; set; }
        //[DataMember]
        public string Value1
        {
            get
            {
                if (this.Values != null)
                {
                    return this.Values.FirstOrDefault();
                }
                return null;
            }
            set
            {
                if (this.Values == null)
                {
                    this.Values = new List<string>();
                }

                if (value != null && this.Values.Contains(value) == false)
                {
                    this.Values.Add(value);
                }
            }
        }

        //[DataMember]
        public string Value2 { get; set; }

        [DataMember]
        public bool XmlColumn { get; set; }

        [DataMember]
        public string ValueOperationType { get; set; }

        /// <summary>
        /// 第一个操作符
        /// </summary>
        [DataMember]
        public FilterOperationType OperationType1 { get; set; }

        /// <summary>
        /// 第二个操作符
        /// </summary>
        [DataMember]
        public FilterOperationType OperationType2 { get; set; }

        /// <summary>
        /// 第三个操作符
        /// </summary>
        [DataMember]
        public FilterOperationType OperationType3 { get; set; }

        [DataMember]
        public FilterLikeType LikeType { get; set; }
        [DataMember]
        public List<FilterItem> FilterItems { get; set; }
        [DataMember]
        public bool AddNullHandle { get; set; }
        [DataMember]
        public string NullValue { get; set; }

        public FilterItem()
        {
            this.FilterItems = new List<FilterItem>();
        }

        public FilterItem And(FilterItem item)
        {
            item.LogicOperation = LogicOperationType.And;
            this.FilterItems.Add(item);
            return this;
        }

        public FilterItem Or(FilterItem item)
        {
            item.LogicOperation = LogicOperationType.Or;
            this.FilterItems.Add(item);
            return this;
        }

        public FilterItem SetXmlColumn(bool xmlColumn)
        {
            this.XmlColumn = xmlColumn;
            return this;
        }

        public FilterItem SetValue1(string value)
        {
            this.Value1 = value;
            return this;
        }
        public FilterItem SetValue2(string value)
        {
            this.Value2 = value;
            return this;
        }

        public FilterItem SetLikeType(FilterLikeType likeType)
        {
            this.LikeType = likeType;
            return this;
        }

        public FilterItem SetOperationType1(FilterOperationType operationType)
        {
            this.OperationType1 = operationType;
            return this;
        }

        public FilterItem SetOperationType2(FilterOperationType operationType)
        {
            this.OperationType2 = operationType;
            return this;
        }

        public FilterItem SetOperationType3(FilterOperationType operationType)
        {
            this.OperationType3 = operationType;
            return this;
        }

        public FilterItem SetObjectName(string objectName)
        {
            this.ObjectName = objectName;
            return this;
        }

        public FilterItem SetColumnName(string columnName)
        {
            this.ColumnName = columnName;
            return this;
        }

        public FilterItem SetLogicOperation(LogicOperationType logicOperationType)
        {
            this.LogicOperation = logicOperationType;
            return this;
        }
        public FilterItem ToUtcValue(Func<DateTime, DateTime> timeChangeFunc)
        {
            foreach (var item in this.FilterItems)
            {
                item.ToUtcValue(timeChangeFunc);
            }
            if (this.DataType == Models.DataType.DateTime || this.DataType == Models.DataType.Date)
            {
                if (timeChangeFunc != null)
                {
                    List<string> newValues = new List<string>();
                    foreach (var item in this.Values)
                    {
                        if (string.IsNullOrWhiteSpace(item) == false)
                        {
                            DateTime newDateTime;
                            var resultSts = DateTime.TryParse(item, out newDateTime);
                            if (resultSts)
                            {
                                newValues.Add(timeChangeFunc(newDateTime).ToString());
                            }
                        }
                        else {
                            newValues.Add("");
                        }
                    }
                    this.Values = newValues;
                }
            }
            return this;
        }
        public override string ToWhere(out Dictionary<string, string> Paramters)
        {
            Dictionary<string, string> parList = new Dictionary<string, string>();
            StringBuilder whereBuilder = new StringBuilder();
            string theFieldName = this.ColumnName;
            if (this.AddNullHandle)
            {
                theFieldName = "ISNULL(" + theFieldName + ",@" + this.ColumnName + "NULLVAL)";
                parList.Add(this.ColumnName + "NULLVAL", this.NullValue);
            }
            switch (this.OperationType1)
            {
                case FilterOperationType.Like:
                    whereBuilder.AppendFormat("{0} like @{1} ", theFieldName, this.ColumnName);
                    switch (this.LikeType)
                    {
                        case FilterLikeType.Contains:
                            parList.Add(this.ColumnName, string.Format("%{0}%", this.Value1));
                            break;
                        case FilterLikeType.Left:
                            parList.Add(this.ColumnName, string.Format("%{0}", this.Value1));
                            break;
                        case FilterLikeType.Right:
                            parList.Add(this.ColumnName, string.Format("{0}%", this.Value1));
                            break;
                        case FilterLikeType.None:
                            parList.Add(this.ColumnName, string.Format("{0}", this.Value1));
                            break;
                        default:
                            break;
                    }
                    break;
                case FilterOperationType.Contains:
                    break;
                case FilterOperationType.EqualTo:
                    whereBuilder.AppendFormat(" {0} = @{1} ", theFieldName, this.ColumnName, this.Value1);
                    parList.Add(this.ColumnName, this.Value1);
                    break;
                case FilterOperationType.IsGreaterThan:
                    whereBuilder.AppendFormat(" {0} > @{1} ", theFieldName, this.ColumnName, this.Value1);
                    parList.Add(this.ColumnName, this.Value1);
                    break;
                case FilterOperationType.NotEqualTo:
                    whereBuilder.AppendFormat(" {0} <> @{1} ", theFieldName, this.ColumnName, this.Value1);
                    parList.Add(this.ColumnName, this.Value1);
                    break;
                case FilterOperationType.IsNotNull:
                    whereBuilder.AppendFormat(" {0} IS NOT NULL ", this.ColumnName);
                    parList.Add(this.ColumnName, "");
                    break;
                case FilterOperationType.IsNull:
                    whereBuilder.AppendFormat(" {0} IS NULL ", this.ColumnName);
                    parList.Add(this.ColumnName, "");
                    break;
                default:
                    break;
            }
            Paramters = parList;
            return whereBuilder.ToString();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(this.Value1)
                && string.IsNullOrWhiteSpace(this.Value2)
                && this.FilterItems.All(m => m.IsEmpty());
           
        }
    }

    public enum DataType
    {
        String = 0,
        Date = 1,
        DateTime = 2,
        Number = 3
    }
}
