using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{   

    [DataContract]
    public enum LogicOperationType
    {
        [EnumMember]
        None,
        [EnumMember]
        Or,
        [EnumMember]
        And,

    }
}
