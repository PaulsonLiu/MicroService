using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public class OrderField
    {
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public bool ASC { get; set; }
        [DataMember]
        public int Index { get; set; }

        public string ToOrderString()
        {
            if (ASC == false)
            {
                return string.Format(" {0} DESC", FieldName);
            }
            else
            {
                return string.Format("{0} ASC", FieldName);
            }
        }
    }
}
