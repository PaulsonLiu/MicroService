using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Models
{
    /// <summary>
    /// 查询数据的方式
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// 通过配置生成
        /// </summary>
        None=0,
        /// <summary>
        /// 通过key值获取数据
        /// </summary>
        Key=4,
        /// <summary>
        /// 通过sql查询数据
        /// </summary>
        Sql=1,
        /// <summary>
        /// 通过Esql查询数据
        /// </summary>
        ESql=2,
        /// <summary>
        /// 通过where 生成sql
        /// </summary>
        Where=8
    }
}
