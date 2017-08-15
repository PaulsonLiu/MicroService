using System;
using System.Collections.Generic;

namespace MicroService.Models
{
    /// <summary>
    /// 模型的通用接口定义
    /// </summary>
    public interface IModelBase
    {
        /// <summary>
        /// 当前的有效字段名称列表
        /// </summary>
        List<string> Fields { get; set; }
        //string ArchiveField { get; set; }
        //bool IsResource { get; }
        //string Bu { get; set; }
        //string ChangedBy { get; set; }
        //string ChangeOn { get; set; }
        //string ConcurrencyField { get; set; }
        //string CreatedBy { get; set; }
        //string CreatedOn { get; set; }
        bool IsSelected { get; set; }
        //string ObjectName { get; }
        //string ObjectRk1 { get; set; }
        //string ObjectRk2 { get; set; }
        //string ObjectType1 { get; set; }
        //string ObjectType2 { get; set; }
    }
}
