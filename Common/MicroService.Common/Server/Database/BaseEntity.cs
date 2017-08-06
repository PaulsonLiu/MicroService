using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MicroService.Common.Server.Database
{
    /// <summary>
    /// DB表基底
    /// </summary>
    [Serializable]
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// DB 資料版號
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public EnumState State { get; set; }
    }

    /// <summary>
    /// 状态
    /// </summary>
    public enum EnumState
    {
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 1,

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
    }
}
