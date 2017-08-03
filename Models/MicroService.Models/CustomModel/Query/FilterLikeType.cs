using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public enum FilterLikeType
    {
        [EnumMember]
        Contains=0,
        [EnumMember]
        Left=1,
        [EnumMember]
        Right=2,
        [EnumMember]
        None=4,
    }
}
