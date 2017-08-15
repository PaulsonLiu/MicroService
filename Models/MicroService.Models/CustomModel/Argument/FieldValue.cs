using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Models
{
    /// <summary>
    /// 代表一个字段的值
    /// </summary>
    public class FieldValue
    {
        /// <summary>
        /// 字段的值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 字段的显示值
        /// </summary>
        public string DisplayValue { get; set; }
        ///// <summary>
        ///// 字段的ToolTip值
        ///// </summary>
        //public string ToolTip { get; set; }
        ///// <summary>
        ///// 当前的字段的标签显示值
        ///// </summary>
        //public string Label { get; set; }
    }

}
