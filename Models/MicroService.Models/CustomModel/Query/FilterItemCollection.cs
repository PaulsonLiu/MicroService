using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public class FilterItemCollection : FilterItemBase
    {
        [DataMember]
        public string CollectionName { get; set; }

        [DataMember]
        public List<FilterItemBase> FilterItems { get; set; }

        public FilterItemCollection()
        {
            this.FilterItems = new List<FilterItemBase>();
        }

        public FilterItemCollection And(FilterItemBase item)
        {
            item.LogicOperation = LogicOperationType.And;
            this.FilterItems.Add(item);
            return this;
        }

        public FilterItemCollection Or(FilterItemBase item)
        {
            item.LogicOperation = LogicOperationType.Or;
            this.FilterItems.Add(item);
            return this;
        }

        /// <summary>
        /// 创建一个分组
        /// </summary>
        /// <returns></returns>
        public FilterItemCollection New()
        {
            return new FilterItemCollection();
        }


        public override string ToWhere(out Dictionary<string,string> Paramters)
        {
            throw new NotImplementedException();
        }
    }
}
