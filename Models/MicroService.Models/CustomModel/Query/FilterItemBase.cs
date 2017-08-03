using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Common;

namespace MicroService.Models
{
    [KnownType(typeof(FilterItemCollection))]
    [KnownType(typeof(FilterItem))]
    [DataContract]
    public abstract class FilterItemBase
    {
        [DataMember]
        public LogicOperationType LogicOperation { get; set; }

        public FilterItemBase()
        {
            this.LogicOperation = LogicOperationType.And;
        }


        public abstract string ToWhere(out Dictionary<string, string> Paramters);
    }
}
