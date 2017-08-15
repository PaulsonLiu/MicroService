using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    /// <summary>
    /// 用于编辑的参数
    /// </summary>
    [DataContract]
    public class BusinessArgs
    {
        /// <summary>
        /// 操作类型
        /// Add Edit Copy etc
        /// </summary>
        [DataMember]
        public BusinessAction Action { get; set; }
        /// <summary>
        /// 查询参数
        /// </summary>
        [DataMember]
        public FilterArgs FilterArgs { get; set; }
        /// <summary>
        /// 默认值的设置
        /// 用于复制
        /// </summary>
        [DataMember]
        public Dictionary<string, string> DefaultValues { get; set; }
    }
}
