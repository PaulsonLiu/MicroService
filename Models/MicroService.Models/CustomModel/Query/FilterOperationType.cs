using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public enum FilterOperationType
    {
        [EnumMember]
        Like,
        [EnumMember]
        Contains,
        [EnumMember]
        EqualTo,
        [EnumMember]
        NotEqualTo,

        //数据值必须大于 >
        [EnumMember]
        IsGreaterThan,

        //数据值必须大于或等于筛选器值。 >=
        [EnumMember]
        IsGreaterThanOrEqualTo,

         //数据值必须小于筛选器值。 <
        [EnumMember]
        IsLessThan,
        //数据值必须小于或等于筛选器值。 <=
        [EnumMember]
        IsLessThanOrEqualTo,
        /// <summary>
        /// 字段为空
        /// </summary>
        [EnumMember]
        IsNull,
        /// <summary>
        /// 字段不为空
        /// </summary>
        [EnumMember]
        IsNotNull,
        /// <summary>
        /// 全文检索
        /// </summary>
        [EnumMember]
        FullTextSearch,
    }
}
