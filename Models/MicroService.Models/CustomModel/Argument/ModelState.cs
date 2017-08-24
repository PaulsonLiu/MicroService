using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Models
{
    /// <summary>
    /// 模型的状态定义(参照EF的实体状态定义)
    /// </summary>
    public enum ModelState
    {
        /// <summary>
        /// 对象存在，但未由对象服务跟踪。在创建实体之后、但将其添加到对象上下文之前，该实体处于此状态。通过调用 System.Data.Objects.ObjectContext.Detach(System.Object)
        /// 方法从上下文中移除实体后，或者使用 System.Data.Objects.MergeOption.NoTrackingSystem.Data.Objects.MergeOption
        /// 加载实体后，该实体也会处于此状态。
        /// </summary>
        Detached = 1,
        /// <summary>
        /// 摘要:
        /// 自对象加载到上下文中后，或自上次调用 System.Data.Objects.ObjectContext.SaveChanges() 方法后，此对象尚未经过修改。
        /// </summary>
        Unchanged = 0,
        /// <summary>
        /// 摘要:
        /// 对象已添加到对象上下文，但尚未调用 System.Data.Objects.ObjectContext.SaveChanges() 方法。对象是通过调用
        /// System.Data.Objects.ObjectContext.AddObject(System.String,System.Object)
        /// 方法添加到对象上下文中的。
        /// </summary>
        Added = 4,
        /// <summary>
        /// 摘要:
        /// 使用 System.Data.Objects.ObjectContext.DeleteObject(System.Object) 方法从对象上下文中删除了对象。
        /// </summary>
        Deleted = 8,
        /// <summary>
        /// 摘要:
        /// 对象已更改，但尚未调用 System.Data.Objects.ObjectContext.SaveChanges() 方法。
        /// </summary>
        Modified = 16,
        /// <summary>
        /// 归档
        /// </summary>
        Archive=32,
    }
}
