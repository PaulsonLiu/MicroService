using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public class GroupField
    {
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public bool ASC { get; set; }
    }

}
