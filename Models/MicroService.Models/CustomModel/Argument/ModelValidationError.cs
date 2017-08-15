using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MicroService.Models
{
    [DataContract]
    public class ModelValidationError
    {
        public ModelValidationError()
        {

        }

        public ModelValidationError(string propertyName, string errorMessage)
        {
            this.ErrorMessage = errorMessage;
            this.PropertyName = propertyName;
        }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string PropertyName { get; set; }
    }
}
