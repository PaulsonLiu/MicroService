using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    /// <summary>
    /// 分页的结构
    /// </summary>
    [DataContract]
    public class PagingArgs
    {
        /// <summary>
        /// 是否需要分页
        /// </summary>
        [DataMember]
        public bool RequirePaging { get; set; }
        /// <summary>
        /// 页的大小
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// 当前的页号
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
        /// <summary>
        /// 数据的总数
        /// </summary>
        [DataMember]
        public long TotalCount { get; set; }
        /// <summary>
        /// 是否需要返回总数
        /// </summary>
        [DataMember]
        public bool RequireTotalCount { get; set; }

        /// <summary>
        /// 分页参数定义
        /// </summary>
        public PagingArgs()
        {
            this.RequirePaging = true;
            this.RequireTotalCount = true;
        }
        public static PagingArgs NoPagingArgs
        {
            get
            {
                return new PagingArgs() { RequirePaging = false, RequireTotalCount = false, PageSize = 100000, PageIndex = 0 };
            }
        }
    }
}
