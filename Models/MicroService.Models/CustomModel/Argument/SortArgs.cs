using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public class SortArgs
    {
        [DataMember]
        public List<OrderField> OrderFields { get; set; }

        public SortArgs()
        {
            this.OrderFields = new List<OrderField>();
        }

        public string GetOrderString()
        {
            if (this.OrderFields != null && this.OrderFields.Count > 0)
            {
                return string.Join(",", this.OrderFields.Select(m =>m.ToOrderString()).ToArray());
            }

            return null;
        }
        public static SortArgs NoSortArgs
        {
            get
            {
                return new SortArgs();
            }
        }
    }
}
