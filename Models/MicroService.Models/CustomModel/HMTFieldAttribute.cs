using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Models
{
    /// <summary>
    /// 字段信息配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HMTFieldAttribute : Attribute
    {
        /// <summary>
        /// 映射的编码
        /// </summary>
        public string MappingCode { get; set; }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool PrimaryKey { get; set; }

        /// <summary>
        /// 是否忽略 不映射到数据库
        /// </summary>
        public bool Ingore { get; set; }

        /// <summary>
        /// 自动编号
        /// </summary>
        public bool AutoSeq { get; set; }
    }
}
