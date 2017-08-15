using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Models
{
    /// <summary>
    /// 商务逻辑操作的类型
    /// </summary>
    public enum  BusinessAction
    {
        /// <summary>
        /// 无操作
        /// </summary>
        NONE=0,
        /// <summary>
        /// 新增操作
        /// </summary>
        ADD=1,
        /// <summary>
        /// 编辑操作
        /// </summary>
        EDIT=2,
        /// <summary>
        /// 复制操作
        /// </summary>
        COPY=4,
        /// <summary>
        /// 删除操作
        /// </summary>
        DELETE=8
    }
}
